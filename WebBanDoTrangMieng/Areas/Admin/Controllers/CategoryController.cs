using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDoTrangMieng.Models.ViewModel;
using System.Data.Entity;

namespace WebBanDoTrangMieng.Areas.Admin.Controllers
{
    [WebBanDoTrangMieng.AdminAuthorize]
    public class CategoryController : Controller
    {
        private readonly QLStoreTrangMiengEntities db = new QLStoreTrangMiengEntities();
        // GET: Admin/Category
        public ActionResult Index()
        {
            var categories = db.Categories.ToList();
            return View(categories);
        }

        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category model)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int id)
        {
            var category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                var category = db.Categories.Find(model.CategoryId);
                if (category == null)
                {
                    return HttpNotFound();
                }
                category.Name = model.Name;
                category.Description = model.Description;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var category = db.Categories.Include(c => c.Products.Select(p => p.Order_Product)).FirstOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                return HttpNotFound();
            }
            // Nếu có sản phẩm thuộc danh mục
            if (category.Products.Any())
            {
                // Nếu có sản phẩm đã từng được bán (có Order_Product)
                if (category.Products.Any(p => p.Order_Product.Any()))
                {
                    TempData["Error"] = "Không thể xóa danh mục đã có sản phẩm từng được bán.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Error"] = "Không thể xóa danh mục đã có sản phẩm.";
                    return RedirectToAction("Index");
                }
            }
            db.Categories.Remove(category);
            db.SaveChanges();
            TempData["Success"] = "Xóa danh mục thành công.";
            return RedirectToAction("Index");
        }

        // POST: Admin/Category/UpdateStatus/5
        [HttpPost]
        public ActionResult UpdateStatus(int id)
        {
            using (var db = new QLStoreTrangMiengEntities())
            {
                var category = db.Categories.Find(id);
                if (category == null)
                {
                    return HttpNotFound();
                }
                // Nếu có trường trạng thái thì cập nhật, nếu không thì bỏ qua
                // category.IsActive = isActive;
                db.SaveChanges();
                return Json(new { success = true });
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