using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanDoTrangMieng.Controllers
{
    public class ProductController : Controller
    {
        private QLStoreTrangMiengEntities db = new QLStoreTrangMiengEntities();

        // GET: Product
        public ActionResult Index(int? categoryId, string categoryType, string sortBy, string search, decimal? minPrice, decimal? maxPrice, int page = 1, int pageSize = 12)
        {
            var products = db.Products.Include("Category").AsQueryable();

            // Filter by category
            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == categoryId.Value);
            }

            // Filter by category type (bánh ngọt hoặc nước uống)
            if (!string.IsNullOrEmpty(categoryType))
            {
                if (categoryType.Equals("banh-ngot", StringComparison.OrdinalIgnoreCase))
                {
                    // CategoryId 1: Bánh Tráng Miệng (các loại bánh ngọt)
                    products = products.Where(p => p.CategoryId == 1);
                }
                else if (categoryType.Equals("nuoc-uong", StringComparison.OrdinalIgnoreCase))
                {
                    // CategoryId 2: Nước Uống
                    products = products.Where(p => p.CategoryId == 2);
                }
            }

            // Search by name
            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.Name.Contains(search));
            }

            // Filter by price range
            if (minPrice.HasValue)
            {
                products = products.Where(p => p.Price >= minPrice.Value);
            }
            if (maxPrice.HasValue)
            {
                products = products.Where(p => p.Price <= maxPrice.Value);
            }

            // Sorting
            switch (sortBy?.ToLower())
            {
                case "name_asc":
                    products = products.OrderBy(p => p.Name);
                    break;
                case "name_desc":
                    products = products.OrderByDescending(p => p.Name);
                    break;
                case "price_asc":
                    products = products.OrderBy(p => p.Price);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(p => p.Price);
                    break;
                case "newest":
                    products = products.OrderByDescending(p => p.CreatedDate);
                    break;
                case "oldest":
                    products = products.OrderBy(p => p.CreatedDate);
                    break;
                case "best_selling":
                    // Tính tổng số lượng sản phẩm đã bán ra (bán chạy nhất)
                    var productSales = db.Order_Product
                        .GroupBy(op => op.ProductId)
                        .Select(group => new {
                            ProductId = group.Key,
                            TotalSold = group.Sum(op => op.Quantity) // Tổng số lượng đã bán
                        })
                        .ToList();

                    // Chuyển danh sách kết quả thành Dictionary để dễ tra cứu
                    var salesDict = new Dictionary<int, int>();
                    foreach (var item in productSales)
                    {
                        salesDict[item.ProductId] = item.TotalSold;
                    }

                    // Lấy danh sách sản phẩm hiện tại
                    var productList = products.ToList();

                    // Sắp xếp sản phẩm theo tổng số lượng đã bán (từ cao xuống thấp)
                    productList = productList.OrderByDescending(p =>
                        salesDict.ContainsKey(p.ProductId) ? salesDict[p.ProductId] : 0
                    ).ToList();

                    // Chuyển kết quả về dạng IQueryable
                    products = productList.AsQueryable();
                    break;
                default:
                    products = products.OrderBy(p => p.Name); // Default sort by name
                    break;
            }

            // Get categories for filter dropdown
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.CurrentCategory = categoryId;
            ViewBag.CurrentCategoryType = categoryType;
            ViewBag.CurrentSort = sortBy;
            ViewBag.CurrentSearch = search;
            ViewBag.CurrentMinPrice = minPrice;
            ViewBag.CurrentMaxPrice = maxPrice;

            // Pagination
            var totalItems = products.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            var pagedProducts = products.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = totalItems;

            return View(pagedProducts);
        }

        // GET: Product/Detail/5
        public ActionResult Detail(int id)
        {
            var product = db.Products.Include("Category").FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return HttpNotFound();
            }

            // Lấy danh sách đánh giá cho sản phẩm này
            var reviews = db.Reviews
                .Where(r => r.Order.Order_Product.Any(op => op.ProductId == id) && r.IsVisible)
                .OrderByDescending(r => r.CreatedDate)
                .ToList();
            ViewBag.Reviews = reviews;

            // Kiểm tra quyền đánh giá - cải tiến logic
            bool canReview = false;
            if (Session["UserId"] != null)
            {
                int userId = (int)Session["UserId"];
                
                // Tìm tất cả đơn hàng hoàn thành chứa sản phẩm này
                var completedOrders = db.Orders
                    .Where(o => o.UserId == userId && o.Status == "Delivered")
                    .Where(o => o.Order_Product.Any(op => op.ProductId == id))
                    .ToList();
                
                // Kiểm tra xem có đơn hàng nào chưa được đánh giá không
                canReview = completedOrders.Any(o => 
                    !db.Reviews.Any(r => r.OrderId == o.OrderId && 
                        r.Order.Order_Product.Any(op => op.ProductId == id)));
            }
            ViewBag.CanReview = canReview;

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddReview(int productId, int rating, string comment)
        {
            if (Session["UserId"] == null)
            {
                TempData["Error"] = "Vui lòng đăng nhập để đánh giá sản phẩm";
                return RedirectToAction("Detail", new { id = productId });
            }
            int userId = (int)Session["UserId"];
            
            // Tìm tất cả đơn hàng hoàn thành chứa sản phẩm này của user
            var completedOrders = db.Orders
                .Where(o => o.UserId == userId && o.Status == "Delivered")
                .Where(o => o.Order_Product.Any(op => op.ProductId == productId))
                .ToList();
            
            if (!completedOrders.Any())
            {
                TempData["Error"] = "Bạn chỉ có thể đánh giá sản phẩm đã mua và đã giao hàng.";
                return RedirectToAction("Detail", new { id = productId });
            }

            // Tìm đơn hàng chưa được đánh giá cho sản phẩm này
            var orderToReview = completedOrders.FirstOrDefault(o => 
                !db.Reviews.Any(r => r.OrderId == o.OrderId && 
                    r.Order.Order_Product.Any(op => op.ProductId == productId)));
            
            if (orderToReview == null)
            {
                TempData["Error"] = "Bạn đã đánh giá sản phẩm này cho tất cả các đơn hàng đã mua. Nếu muốn đánh giá thêm, vui lòng mua sản phẩm này lần nữa.";
                return RedirectToAction("Detail", new { id = productId });
            }

            // Validate
            if (rating < 1 || rating > 5)
            {
                TempData["Error"] = "Vui lòng chọn số sao từ 1 tới 5";
                return RedirectToAction("Detail", new { id = productId });
            }
            if (string.IsNullOrEmpty(comment) || comment.Length < 10)
            {
                TempData["Error"] = "Vui lòng nhập nội dung đánh giá ít nhất 10 ký tự";
                return RedirectToAction("Detail", new { id = productId });
            }

            // Tạo đánh giá mới
            var review = new Review
            {
                OrderId = orderToReview.OrderId,
                Rating = rating,
                Comment = comment,
                CreatedDate = DateTime.Now,
                IsVisible = true
            };
            
            try
            {
                db.Reviews.Add(review);
                db.SaveChanges();
                TempData["Success"] = "Cảm ơn bạn đã đánh giá sản phẩm!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Có lỗi xảy ra khi lưu đánh giá. Vui lòng thử lại.";
            }
            return RedirectToAction("Detail", new { id = productId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
