using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using WebBanDoTrangMieng.Models.ViewModel;

namespace WebBanDoTrangMieng.Areas.Admin.Controllers
{
    [WebBanDoTrangMieng.AdminAuthorize]
    public class InventoryController : Controller
    {
        private readonly QLStoreTrangMiengEntities db = new QLStoreTrangMiengEntities();
        // GET: Admin/Inventory
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var allProducts = db.Products
                .Select(p => new InventoryProductVM
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    StockQuantity = p.StockQuantity,
                    CategoryName = p.Category.Name
                });
            int totalProducts = allProducts.Count();
            var products = allProducts
                .OrderBy(p => p.ProductId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.TotalProducts = totalProducts;
            return View(products);
        }
        // GET: Admin/Inventory/Edit/5
        public ActionResult Edit(int id)
        {
            var product = db.Products.Find(id);
            if (product == null)
                return HttpNotFound();
            return View(product);
        }

        // POST: Admin/Inventory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product model)
        {
            var product = db.Products.Find(model.ProductId);
            if (product == null)
                return HttpNotFound();

            product.StockQuantity = model.StockQuantity;
            db.SaveChanges();
            TempData["Success"] = "Cập nhật tồn kho thành công!";
            return RedirectToAction("Index");
        }
    }
}