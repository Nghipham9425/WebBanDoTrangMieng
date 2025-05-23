using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanDoTrangMieng.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart - Hiển thị giỏ hàng
        public ActionResult Index()
        {
            // Ở đây sau này bạn sẽ lấy dữ liệu giỏ hàng từ session hoặc database
            return View();
        }

        // GET: Cart/Checkout - Trang thanh toán
        public ActionResult Checkout()
        {
            return View();
        }

        // POST: Cart/AddToCart - Thêm sản phẩm vào giỏ hàng (sử dụng AJAX)
        [HttpPost]
        public ActionResult AddToCart(int productId, int quantity = 1)
        {
            // Xử lý thêm sản phẩm vào giỏ hàng
            // Trả về JSON để cập nhật giao diện
            return Json(new { success = true, message = "Đã thêm sản phẩm vào giỏ hàng" });
        }

        // POST: Cart/UpdateCart - Cập nhật số lượng sản phẩm
        [HttpPost]
        public ActionResult UpdateCart(int productId, int quantity)
        {
            // Xử lý cập nhật số lượng sản phẩm
            return Json(new { success = true });
        }

        // POST: Cart/RemoveItem - Xóa sản phẩm khỏi giỏ hàng
        [HttpPost]
        public ActionResult RemoveItem(int productId)
        {
            // Xử lý xóa sản phẩm
            return Json(new { success = true });
        }
    }
}
