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

        // POST: Cart/ApplyPromotion - Áp dụng mã giảm giá
        [HttpPost]
        public ActionResult ApplyPromotion(string promoCode)
        {
            try
            {
                if (string.IsNullOrEmpty(promoCode))
                {
                    return Json(new { success = false, message = "Vui lòng nhập mã giảm giá" });
                }

                // Tìm mã giảm giá trong database
                var promotion = db.Promotions.FirstOrDefault(p => 
                    p.Code.ToUpper() == promoCode.ToUpper() && 
                    p.IsActive == true &&
                    p.StartDate <= DateTime.Now &&
                    p.EndDate >= DateTime.Now);

                if (promotion == null)
                {
                    return Json(new { success = false, message = "Mã giảm giá không hợp lệ hoặc đã hết hạn" });
                }

                // Debug log
                System.Diagnostics.Debug.WriteLine($"Found promotion: {promotion.Code}, Discount: {promotion.DiscountPercent}");

                var cart = GetCart();
                if (!cart.Items.Any())
                {
                    return Json(new { success = false, message = "Giỏ hàng trống" });
                }

                // Tính discount
                decimal discountAmount = (cart.TotalAmount * promotion.DiscountPercent) / 100;
                
                // Lưu thông tin promotion vào session
                Session["AppliedPromotion"] = new Models.ViewModel.AppliedPromotionVM {
                    Code = promotion.Code,
                    DiscountPercent = promotion.DiscountPercent,
                    DiscountAmount = discountAmount,
                    Description = promotion.Description
                };

                return Json(new { 
                    success = true, 
                    message = "Áp dụng mã giảm giá thành công",
                    promoCode = promotion.Code,
                    discountPercent = promotion.DiscountPercent,
                    discountAmount = discountAmount.ToString("N0"),
                    originalTotal = cart.TotalAmount.ToString("N0"),
                    newTotal = (cart.TotalAmount - discountAmount + 20000).ToString("N0") // +20k shipping
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

        // POST: Cart/RemovePromotion - Xóa mã giảm giá
        [HttpPost]
        public ActionResult RemovePromotion()
        {
            try
            {
                Session["AppliedPromotion"] = null;
                var cart = GetCart();
                
                return Json(new { 
                    success = true, 
                    message = "Đã hủy mã giảm giá",
                    originalTotal = cart.TotalAmount.ToString("N0"),
                    newTotal = (cart.TotalAmount + 20000).ToString("N0") // +20k shipping
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }
        // GET: Cart/Checkout - Trang thanh toán
        public ActionResult Checkout()
        {
            // Kiểm tra đăng nhập
            if (Session["UserId"] == null)
            {
                TempData["ErrorMessage"] = "Bạn cần đăng nhập để đặt!";
                return RedirectToAction("Index", "Home");
            }

            // Kiểm tra vai trò
            if (Session["UserRole"] == null || Session["UserRole"].ToString() != "Customer")
            {
                TempData["ErrorMessage"] = "Chỉ khách hàng mới được đặt!";
                return RedirectToAction("Index", "Home");
            }

            // Lấy thông tin user để fill vào form
            int userId = (int)Session["UserId"];
            var user = db.Users.Find(userId);
            if (user != null)
            {
                ViewBag.UserName = user.UserName;
                ViewBag.Email = user.Email;
                ViewBag.Phone = user.Phone;
                ViewBag.Address = user.Address;
            }

            var cart = GetCart();
            if (!cart.Items.Any())
            {
                TempData["ErrorMessage"] = "Giỏ trống!";
                return RedirectToAction("Index");
            }
            return View(cart);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(string fullName, string phone, string email, string address, string provinceName, string districtName, string wardName, string payment)
        {
            if (Session["UserId"] == null)
            {
                TempData["ErrorMessage"] = "Bạn cần đăng nhập để đặt!";
                return RedirectToAction("Index", "Home");
            }
            if (Session["UserRole"] == null || Session["UserRole"].ToString() != "Customer")
            {
                TempData["ErrorMessage"] = "Chỉ khách hàng mới được đặt!";
                return RedirectToAction("Index", "Home");
            }

            var cart = GetCart();
            if (!cart.Items.Any())
            {
                TempData["ErrorMessage"] = "Giỏ hàng trống!";
                return RedirectToAction("Index");
            }
            string shippingAddress = $"{address}, {wardName}, {districtName}, {provinceName}";


            int paymentId;
            if (!int.TryParse(payment, out paymentId))
            {
                TempData["ErrorMessage"] = "Phương thức thanh toán không hợp lệ!";
                return RedirectToAction("Checkout");
            }
            var paymentMethodName = db.PaymentMethods
                .Where(m => m.PaymentMethodId == paymentId)
                .Select(m => m.MethodName)
                .FirstOrDefault();

            // Lấy discount info trước khi xóa session
            decimal discountAmount = 0;
            string discountCode = "";
            var appliedPromotion = Session["AppliedPromotion"] as Models.ViewModel.AppliedPromotionVM;
            if (appliedPromotion != null)
            {
                discountAmount = appliedPromotion.DiscountAmount;
                discountCode = appliedPromotion.Code;
            }

            // Hack: Lưu discount vào PaymentMethod field
            string paymentMethodWithDiscount = paymentMethodName;
            if (discountAmount > 0)
            {
                paymentMethodWithDiscount = $"{paymentMethodName}|DISCOUNT:{discountAmount}|CODE:{discountCode}";
            }

            var order = new Order
            {
                UserId = (int)Session["UserId"],
                OrderDate = DateTime.Now,
                ShippingAddress = shippingAddress,
                PaymentMethod = paymentMethodWithDiscount, // Lưu với discount info
                Status = "Pending"
            };
            db.Orders.Add(order);
            db.SaveChanges();

            foreach (var item in cart.Items)
            {
                var orderProduct = new Order_Product
                {
                    OrderId = order.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                };
                db.Order_Product.Add(orderProduct);

                // Trừ tồn kho sản phẩm
                var product = db.Products.Find(item.ProductId);
                if (product != null && product.StockQuantity.HasValue)
                {
                    product.StockQuantity -= item.Quantity;
                    if (product.StockQuantity < 0) product.StockQuantity = 0; // Không cho âm
                }
            }
            db.SaveChanges();

            if (payment == "7") // VNPAY
            {
                // Xóa giỏ hàng và promotion trước khi redirect (sử dụng discountAmount đã có)
                Session["Cart"] = null;
                Session["AppliedPromotion"] = null;
                return RedirectToAction("VnpayCheckout", "Payment", new { orderId = order.OrderId, discountAmount = discountAmount });
            }
            else // COD
            {
                // Xóa giỏ hàng và promotion
                Session["Cart"] = null;
                Session["AppliedPromotion"] = null;
                // Hiển thị trang trạng thái đơn hàng thay vì chuyển thẳng về lịch sử
                return RedirectToAction(
                    "OrderStatus",
                    "Payment",
                    new {
                        orderCode = order.OrderId.ToString(),
                        status = "Thành công",
                        message = "Đặt hàng thành công!"
                    }
                );
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