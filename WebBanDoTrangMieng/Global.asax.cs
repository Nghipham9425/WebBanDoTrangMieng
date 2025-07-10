using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebBanDoTrangMieng
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }

    // Admin Authorization Attribute
    public class AdminAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = filterContext.HttpContext.Session;
            
            // Kiểm tra đã đăng nhập và là Admin chưa
            if (session["UserId"] == null || session["UserRole"]?.ToString() != "Admin")
            {
                // Redirect về trang chủ với thông báo
                filterContext.Controller.TempData["ErrorMessage"] = "Bạn cần đăng nhập Account Admin";
                filterContext.Result = new RedirectResult("/");
                return;
            }
            
            base.OnActionExecuting(filterContext);
        }
    }
}
