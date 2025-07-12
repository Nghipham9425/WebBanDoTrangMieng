using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanDoTrangMieng.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class DashboardController : Controller
    {
        private readonly QLStoreTrangMiengEntities db = new QLStoreTrangMiengEntities();

        public ActionResult Index()
        {
            try
            {
                // Thống kê cơ bản
                ViewBag.TotalProducts = db.Products.Count();
                ViewBag.TotalOrders = db.Orders.Count();
                ViewBag.TotalCustomers = db.Users.Count(u => u.Role == "Customer");
                
                // Doanh thu
                ViewBag.TotalRevenue = db.Orders
                    .Where(o => o.Status == "Delivered" || o.Status == "Paid")
                    .SelectMany(o => o.Order_Product)
                    .Sum(op => (decimal?)op.Quantity * op.Price) ?? 0;
                
                // Đơn hàng theo trạng thái
                ViewBag.PendingOrders = db.Orders.Count(o => o.Status == "Pending");
                ViewBag.PaidOrders = db.Orders.Count(o => o.Status == "Paid" || o.Status == "Delivered");
                
                // Đơn hàng gần đây - sử dụng tên property đúng
                var recentOrdersList = db.Orders
                    .OrderByDescending(o => o.OrderDate)
                    .Take(3)
                    .ToList();
                
                var recentOrders = recentOrdersList.Select(o => new {
                    OrderId = o.OrderId,
                    OrderDate = o.OrderDate,
                    Status = o.Status,
                    UserName = (o.User != null) ? o.User.UserName : "Khách hàng",
                    Total = db.Order_Product.Where(op => op.OrderId == o.OrderId).Sum(op => (decimal?)op.Quantity * op.Price) ?? 0
                }).ToList();
                
                ViewBag.RecentOrders = recentOrders;
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi: " + ex.Message;
                // Dữ liệu mặc định khi lỗi
                ViewBag.TotalProducts = 0;
                ViewBag.TotalOrders = 0;
                ViewBag.TotalCustomers = 0;
                ViewBag.TotalRevenue = 0;
                ViewBag.PendingOrders = 0;
                ViewBag.PaidOrders = 0;
                ViewBag.RecentOrders = new List<object>();
            }
            
            return View();
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