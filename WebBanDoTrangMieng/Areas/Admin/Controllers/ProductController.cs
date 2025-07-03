using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanDoTrangMieng.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private QLStoreTrangMiengEntities db = new QLStoreTrangMiengEntities();

        // GET: Admin/Product
        public ActionResult Index(int page = 1, string search = "")
        {
            int pageSize = 12; // Số sản phẩm trên mỗi trang
            
            var products = db.Products.Include(p => p.Category).AsQueryable();
            
            // Tìm kiếm nếu có
            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.Name.Contains(search) || p.Category.Name.Contains(search));
                ViewBag.Search = search;
            }
            
            // Sắp xếp theo ngày tạo mới nhất
            products = products.OrderByDescending(p => p.CreatedDate);
            
            // Tính toán phân trang
            int totalProducts = products.Count();
            int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
            
            var productList = products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            
            // Truyền thông tin phân trang qua ViewBag
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.TotalProducts = totalProducts;
            ViewBag.PageSize = pageSize;
            ViewBag.HasPreviousPage = page > 1;
            ViewBag.HasNextPage = page < totalPages;
            
            return View(productList);
        }

        // GET: Admin/Product/Details/5
        public ActionResult Details(int id)
        {
            Product product = db.Products.Include(p => p.Category).FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Product/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return View();
        }

        // POST: Admin/Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Price,ImageUrl,Description,StockQuantity,CategoryId")] Product product, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                // Handle image upload
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(imageFile.FileName);
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                    string path = Path.Combine(Server.MapPath("~/Content/Images/Products/"), uniqueFileName);
                    
                    // Create directory if it doesn't exist
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                    
                    imageFile.SaveAs(path);
                    product.ImageUrl = "/Content/Images/Products/" + uniqueFileName;
                }

                product.CreatedDate = DateTime.Now;
                db.Products.Add(product);
                db.SaveChanges();
                
                TempData["SuccessMessage"] = "Sản phẩm đã được thêm thành công!";
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Admin/Product/Edit/5
        public ActionResult Edit(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Admin/Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,Name,Price,ImageUrl,Description,StockQuantity,CategoryId,CreatedDate")] Product product, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                // Handle image upload
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    // Delete old image if exists
                    if (!string.IsNullOrEmpty(product.ImageUrl))
                    {
                        string oldImagePath = Server.MapPath("~" + product.ImageUrl);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    string fileName = Path.GetFileName(imageFile.FileName);
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                    string path = Path.Combine(Server.MapPath("~/Content/Images/Products/"), uniqueFileName);
                    
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                    
                    imageFile.SaveAs(path);
                    product.ImageUrl = "/Content/Images/Products/" + uniqueFileName;
                }

                db.Entry(product).State = System.Data.EntityState.Modified;
                db.SaveChanges();
                
                TempData["SuccessMessage"] = "Sản phẩm đã được cập nhật thành công!";
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Admin/Product/Delete/5
        public ActionResult Delete(int id)
        {
            Product product = db.Products.Include(p => p.Category).FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            
            // Delete image file if exists
            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                string imagePath = Server.MapPath("~" + product.ImageUrl);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            
            db.Products.Remove(product);
            db.SaveChanges();
            
            TempData["SuccessMessage"] = "Sản phẩm đã được xóa thành công!";
            return RedirectToAction("Index");
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