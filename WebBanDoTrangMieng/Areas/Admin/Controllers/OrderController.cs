using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace WebBanDoTrangMieng.Areas.Admin.Controllers
{

    public class OrderController : Controller
    {
        private QLStoreTrangMiengEntities db= new QLStoreTrangMiengEntities();
        // GET: Admin/Order
        public ActionResult Index(int page =1 , string status="",string search="")
        {
            int pageSize = 10;

            var orders=db.Orders.Include(o=>o.User).AsQueryable();

            //loc theo trang thai
            if(!string.IsNullOrEmpty(status))
            orders =orders.Where(o=>o.Status==status);

            //tim kiem theo ten khach hang hoac orderID
            if(!string.IsNullOrEmpty(search))
            {
                orders=orders.Where(o=>o.User.UserName.Contains(search) || 
                            o.OrderId.ToString().Contains(search));
            }

            //sap xep theo ngay moi nhat
            orders = orders.OrderByDescending(o=>o.OrderDate);

            //phan trang
            int totalOrders=orders.Count();
            int totalPages=(int)Math.Ceiling((double)totalOrders/pageSize);

            var orderList=orders.Skip((page-1)*pageSize)
            .Take(pageSize)
            .ToList();
            
            //truyen du lieu cho view
            ViewBag.CurrentPage=page;
            ViewBag.TotalPages=totalPages;
            ViewBag.TotalOrders=totalOrders;
            ViewBag.CurrentStatus=status;
            ViewBag.Search=search;


            return View(orderList);
        }

        // GET: Admin/Order/Details/5
        public ActionResult Details(int id)
        {
            var order = db.Orders
                .Include(o => o.User)
                .Include(o => o.Order_Product.Select(op => op.Product))
                .FirstOrDefault(o => o.OrderId == id);
            
            if (order == null)
            {
                return HttpNotFound();
            }
            
            // Tính tổng tiền đơn hàng
            decimal totalAmount = order.Order_Product.Sum(op => op.Quantity * op.Price);
            ViewBag.TotalAmount = totalAmount;
            
            return View(order);
        }

        // GET: Admin/Order/Edit/5
        public ActionResult Edit(int id)
        {
            var order = db.Orders
                .Include(o => o.User)
                .Include(o => o.Order_Product.Select(op => op.Product))
                .FirstOrDefault(o => o.OrderId == id);
            
            if (order == null)
            {
                return HttpNotFound();
            }

            // Kiểm tra xem có thể edit không - CHỈ KHÔNG CHO SỬA KHI ĐÃ HOÀN TẤT
            if (order.Status == "Delivered" || order.Status == "Cancelled")
            {
                TempData["ErrorMessage"] = "Không thể cập nhật đơn hàng đã " + 
                    (order.Status == "Delivered" ? "giao thành công" : "hủy");
                return RedirectToAction("Details", new { id = id });
            }

            // Tạo dropdown list cho status
            ViewBag.StatusList = GetStatusSelectList(order.Status);
            
            // Tính tổng tiền
            decimal totalAmount = order.Order_Product.Sum(op => op.Quantity * op.Price);
            ViewBag.TotalAmount = totalAmount;
            
            return View(order);
        }

        // POST: Admin/Order/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string status)
        {
            var order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }

            // Validate status transition
            if (!IsValidStatusTransition(order.Status, status))
            {
                TempData["ErrorMessage"] = $"Không thể chuyển từ trạng thái '{order.Status}' sang '{status}'";
                return RedirectToAction("Edit", new { id = id });
            }

            // Update status
            order.Status = status;
            db.SaveChanges();

            // Success message
            TempData["SuccessMessage"] = $"Đã cập nhật trạng thái đơn hàng #{order.OrderId} thành '{GetStatusText(status)}'";
            
            return RedirectToAction("Details", new { id = id });
        }

        private List<SelectListItem> GetStatusSelectList(string currentStatus)
        {
            var statuses = new List<SelectListItem>();
            
            // Logic chuyển đổi trạng thái hợp lệ
            switch (currentStatus)
            {
                case "Pending":
                    statuses.Add(new SelectListItem { Text = "⏳ Chờ xác nhận", Value = "Pending", Selected = true });
                    statuses.Add(new SelectListItem { Text = "✅ Xác nhận đơn hàng", Value = "Confirmed" });
                    statuses.Add(new SelectListItem { Text = "❌ Hủy đơn hàng", Value = "Cancelled" });
                    break;
                    
                case "Confirmed":
                    statuses.Add(new SelectListItem { Text = "✅ Đã xác nhận", Value = "Confirmed", Selected = true });
                    statuses.Add(new SelectListItem { Text = "🔄 Bắt đầu chuẩn bị", Value = "Processing" });
                    statuses.Add(new SelectListItem { Text = "❌ Hủy đơn hàng", Value = "Cancelled" });
                    break;
                    
                case "Processing":
                    statuses.Add(new SelectListItem { Text = "🔄 Đang chuẩn bị", Value = "Processing", Selected = true });
                    statuses.Add(new SelectListItem { Text = "🚛 Giao cho shipper", Value = "Shipped" });
                    break;
                    
                case "Shipped":
                    statuses.Add(new SelectListItem { Text = "🚛 Đang giao hàng", Value = "Shipped", Selected = true });
                    statuses.Add(new SelectListItem { Text = "📦 Giao thành công", Value = "Delivered" });
                    statuses.Add(new SelectListItem { Text = "❌ Giao thất bại", Value = "Cancelled" });
                    break;
            }
            
            return statuses;
        }

        private bool IsValidStatusTransition(string currentStatus, string newStatus)
        {
            // Define valid transitions
            var validTransitions = new Dictionary<string, List<string>>
            {
                ["Pending"] = new List<string> { "Confirmed", "Cancelled" },
                ["Confirmed"] = new List<string> { "Processing", "Cancelled" },
                ["Processing"] = new List<string> { "Shipped" },
                ["Shipped"] = new List<string> { "Delivered", "Cancelled" }
            };

            return validTransitions.ContainsKey(currentStatus) && 
                   validTransitions[currentStatus].Contains(newStatus);
        }

        private string GetStatusText(string status)
        {
            switch (status)
            {
                case "Pending": return "Chờ xác nhận";
                case "Confirmed": return "Đã xác nhận";
                case "Processing": return "Đang chuẩn bị";
                case "Shipped": return "Đang giao";
                case "Delivered": return "Đã giao";
                case "Cancelled": return "Đã hủy";
                default: return status;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}