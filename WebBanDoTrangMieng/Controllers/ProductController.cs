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
        public ActionResult Index(int? categoryId, string sortBy, string search, decimal? minPrice, decimal? maxPrice, int page = 1, int pageSize = 12)
        {
            var products = db.Products.Include("Category").AsQueryable();
            
            // Filter by category
            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == categoryId.Value);
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
                default:
                    products = products.OrderBy(p => p.Name); // Default sort by name
                    break;
            }
            
            // Get categories for filter dropdown
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.CurrentCategory = categoryId;
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
            
            return View(product);
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
