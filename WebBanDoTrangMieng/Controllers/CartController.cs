using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDoTrangMieng.Models;

namespace WebBanDoTrangMieng.Controllers
{
    public class CartController : Controller
    {
        private QLStoreTrangMiengEntities db = new QLStoreTrangMiengEntities();

        private Cart GetCart()
        {
            var cart= Session["Cart"] as Cart;
            if(cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        public ActionResult Index()
        {
           var cart=GetCart();
           return View(cart);
        }
                // POST: Cart/AddToCart - Thêm sản phẩm vào giỏ hàng
        [HttpPost]
        public ActionResult AddToCart(int productId, int quantity = 1)
        {
            try
            {
                var product = db.Products.Find(productId);
                if (product == null)
                {
                    return Json(new { success = false, message = "Sản phẩm không tồn tại" });
                }

                var cart = GetCart();
                var existingItem = cart.Items.FirstOrDefault(x => x.ProductId == productId);

                if (existingItem != null)
                {
                    // Nếu sản phẩm đã có trong giỏ, tăng số lượng
                    existingItem.Quantity += quantity;
                }
                else
                {
                    // Thêm sản phẩm mới vào giỏ
                    cart.Items.Add(new CartItem
                    {
                        ProductId = productId,
                        ProductName = product.Name,
                        Price = product.Price,
                        Quantity = quantity,
                        ImageUrl = product.ImageUrl
                    });
                }

                Session["Cart"] = cart;

                return Json(new { 
                    success = true, 
                    message = "Đã thêm sản phẩm vào giỏ hàng",
                    cartCount = cart.TotalItems,
                    cartTotal = cart.TotalAmount.ToString("N0")
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

        [HttpPost]
        public ActionResult UpdateCart(int productId, int quantity)
        {
            try
            {
                var cart=GetCart();
                var item=cart.Items.FirstOrDefault(x=>x.ProductId == productId);
                if(item!=null)
                {
                    if(quantity <= 0)
                    {
                        cart.Items.Remove(item);
                    }
                    else
                    {
                        item.Quantity = quantity;
                    }
                }
                Session["Cart"] = cart;
                return Json (new {
                    success = true,
                    cartCount = cart.TotalItems,
                    cartTotal = cart.TotalAmount.ToString("N0"),
                    itemTotal= (item?.TotalPrice ?? 0).ToString("N0")
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }
        // POST: Cart/RemoveItem - Xóa sản phẩm khỏi giỏ hàng
        [HttpPost]
        public ActionResult RemoveItem(int productId)
        {
            try
            {
                var cart = GetCart();
                var item = cart.Items.FirstOrDefault(x => x.ProductId == productId);

                if (item != null)
                {
                    cart.Items.Remove(item);
                    Session["Cart"] = cart;
                }

                return Json(new { 
                    success = true,
                    message = "Đã xóa sản phẩm khỏi giỏ hàng",
                    cartCount = cart.TotalItems,
                    cartTotal = cart.TotalAmount.ToString("N0")
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }
        //get Cart/GetCartCount -- lay so luong san pham trong gio hang
        public ActionResult GetCartCount()
        {
            var cart=GetCart();
            return Json(new {count = cart.TotalItems}, JsonRequestBehavior.AllowGet);
        }
        // GET: Cart/Checkout - Trang thanh toán
        public ActionResult Checkout()
        {
            var cart = GetCart();
            if (!cart.Items.Any())
            {
                TempData["ErrorMessage"] = "Giỏ hàng trống!";
                return RedirectToAction("Index");
            }
            return View(cart);
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