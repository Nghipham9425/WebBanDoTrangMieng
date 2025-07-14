using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDoTrangMieng.Models.ViewModel;

namespace WebBanDoTrangMieng.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            var model = new ContactVM();
            
            // Populate thông tin từ Session nếu user đã đăng nhập
            if (Session["Email"] != null)
            {
                model.Name = Session["UserName"]?.ToString() ?? "";
                model.Email = Session["Email"]?.ToString() ?? "";
                model.Phone = Session["Phone"]?.ToString() ?? "";
            }
            
            return View(model);
        }
    }
}