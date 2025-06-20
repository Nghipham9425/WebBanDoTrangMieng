using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanDoTrangMieng.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(int? categoryId)
        {
            // Trang danh sách sản phẩm
            // Sau này sẽ lấy sản phẩm theo danh mục từ database
            return View();
        }

        // GET: Product/Detail/5
        public ActionResult Detail(int id)
        {
            // Sau này sẽ lấy chi tiết sản phẩm từ database theo id
            // Hiện tại để đơn giản, ta cứ return view
            ViewBag.ProductId = id;
            return View();
        }
    }
}
