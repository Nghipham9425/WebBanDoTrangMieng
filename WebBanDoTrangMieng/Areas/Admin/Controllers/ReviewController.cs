using System.Linq;
using System.Web.Mvc;
using WebBanDoTrangMieng;
using System.Data.Entity;
using System;
using WebBanDoTrangMieng.Models.ViewModel;

namespace WebBanDoTrangMieng.Areas.Admin.Controllers
{
    public class ReviewController : Controller
    {
        private QLStoreTrangMiengEntities db = new QLStoreTrangMiengEntities();

        // GET: Admin/Review - Trang chính hiển thị danh sách sản phẩm có review
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            // Lấy danh sách sản phẩm có đánh giá với thông tin chi tiết
            var productsWithReviews = db.Products
                .Where(p => db.Reviews.Any(r => r.Order.Order_Product.Any(op => op.ProductId == p.ProductId)))
                .Select(p => new ProductReviewVM
                {
                    Product = p,
                    ReviewCount = db.Reviews.Count(r => r.Order.Order_Product.Any(op => op.ProductId == p.ProductId)),
                    VisibleReviewCount = db.Reviews.Count(r => r.Order.Order_Product.Any(op => op.ProductId == p.ProductId) && r.IsVisible),
                    HiddenReviewCount = db.Reviews.Count(r => r.Order.Order_Product.Any(op => op.ProductId == p.ProductId) && !r.IsVisible),
                    AverageRating = db.Reviews
                        .Where(r => r.Order.Order_Product.Any(op => op.ProductId == p.ProductId))
                        .Average(r => (double?)r.Rating) ?? 0,
                    LatestReview = db.Reviews
                        .Where(r => r.Order.Order_Product.Any(op => op.ProductId == p.ProductId))
                        .OrderByDescending(r => r.CreatedDate)
                        .FirstOrDefault().CreatedDate
                })
                .OrderByDescending(x => x.LatestReview)
                .ToList();

            // Phân trang
            int totalProducts = productsWithReviews.Count;
            var pagedProducts = productsWithReviews.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
            ViewBag.TotalProducts = totalProducts;
            ViewBag.PageSize = pageSize;

            return View(pagedProducts);
        }

        // GET: Admin/Review/ProductReviews/5 - Chi tiết đánh giá của sản phẩm
        public ActionResult ProductReviews(int productId, int page = 1, int pageSize = 10)
        {
            var product = db.Products.Include(p => p.Category).FirstOrDefault(p => p.ProductId == productId);
            if (product == null)
            {
                TempData["Error"] = "Không tìm thấy sản phẩm";
                return RedirectToAction("Index");
            }

            // Lấy reviews với thông tin user đầy đủ
            var reviewsQuery = db.Reviews
                .Include(r => r.Order)
                .Include(r => r.Order.User)
                .Where(r => r.Order.Order_Product.Any(op => op.ProductId == productId))
                .OrderByDescending(r => r.CreatedDate);

            int totalReviews = reviewsQuery.Count();
            int totalPages = (int)Math.Ceiling((double)totalReviews / pageSize);

            var reviews = reviewsQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(r => new ReviewVM
                {
                    ReviewId = r.ReviewId,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    CreatedDate = r.CreatedDate,
                    IsVisible = r.IsVisible,
                    UserName = r.Order.User.UserName ?? "Khách hàng",
                    UserEmail = r.Order.User.Email ?? "",
                    OrderId = r.OrderId
                })
                .ToList();

            ViewBag.Product = product;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalReviews = totalReviews;
            ViewBag.TotalPages = totalPages;

            return View(reviews);
        }

        // POST: Admin/Review/Hide/5 - Ẩn review
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Hide(int id)
        {
            try
            {
                var review = db.Reviews.Find(id);
                if (review == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy đánh giá" });
                }

                review.IsVisible = false;
                db.SaveChanges();
                
                return Json(new { success = true, message = "Đã ẩn đánh giá thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

        // POST: Admin/Review/Show/5 - Hiện review
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Show(int id)
        {
            try
            {
                var review = db.Reviews.Find(id);
                if (review == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy đánh giá" });
                }

                review.IsVisible = true;
                db.SaveChanges();
                
                return Json(new { success = true, message = "Đã hiển thị đánh giá thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
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