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
    public class StatisticController : Controller
    {

        private QLStoreTrangMiengEntities db = new QLStoreTrangMiengEntities();

        // GET: Admin/Statistic
        public ActionResult Index(DateTime? from, DateTime? to)
        {
            // Nếu không có filter, lấy 30 ngày gần nhất
            if (!from.HasValue && !to.HasValue)
            {
                to = DateTime.Now.Date;
                from = to.Value.AddDays(-30);
            }

            // Lấy dữ liệu doanh thu
            var query = db.Orders.Where(o => o.Status == "Delivered" || o.Status == "Paid").AsQueryable();

            if (from.HasValue)
                query = query.Where(o => o.OrderDate >= from.Value);
            if (to.HasValue)
                query = query.Where(o => o.OrderDate <= to.Value);


            var report = query
            .Include(o => o.Order_Product)
            .GroupBy(o => new {
                Year = o.OrderDate.Value.Year,
                Month = o.OrderDate.Value.Month,
                Day = o.OrderDate.Value.Day
            })
            .Select(g => new {
                g.Key.Year,
                g.Key.Month,
                g.Key.Day,
                TotalRevenue = g.SelectMany(x => x.Order_Product).Sum(p => p.Price * p.Quantity),
                OrderCount = g.Count(),
                ProductSold = g.SelectMany(x => x.Order_Product).Sum(p => p.Quantity)
            })
            .AsEnumerable() // Chuyển sang LINQ to Objects
            .Select(x => new RevenueReportVM
            {
                Date = new DateTime(x.Year, x.Month, x.Day),
                TotalRevenue = x.TotalRevenue,
                OrderCount = x.OrderCount,
                ProductSold = x.ProductSold
            })
            .OrderBy(r => r.Date)
            .ToList();



            // Thống kê tổng quan
            ViewBag.TotalRevenue = report.Sum(r => r.TotalRevenue);
            ViewBag.TotalOrders = report.Sum(r => r.OrderCount);
            ViewBag.TotalProducts = report.Sum(r => r.ProductSold);
            ViewBag.AvgRevenuePerOrder = ViewBag.TotalOrders > 0 ? ViewBag.TotalRevenue / ViewBag.TotalOrders : 0;

            // Dữ liệu cho biểu đồ
            ViewBag.ChartLabels = string.Join(",", report.Select(r => "\"" + r.Date.ToString("dd/MM") + "\""));
            ViewBag.ChartData = string.Join(",", report.Select(r => r.TotalRevenue));

            // Filter values
            ViewBag.FromDate = from?.ToString("yyyy-MM-dd") ?? "";
            ViewBag.ToDate = to?.ToString("yyyy-MM-dd") ?? "";

            return View(report);
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