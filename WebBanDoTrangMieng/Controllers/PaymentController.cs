using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDoTrangMieng;
using WebBanDoTrangMieng.Helpers;
using System.Text;
using WebBanDoTrangMieng.Models.ViewModel;

namespace WebBanDoTrangMieng.Controllers
{
    public class PaymentController : Controller
    {
        private QLStoreTrangMiengEntities db = new QLStoreTrangMiengEntities();
        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }

        // Action tạo URL thanh toán VNPAY sử dụng PayLib
        public ActionResult VnpayCheckout(int orderId, decimal discountAmount = 0)
        {
            var order = db.Orders.Find(orderId);
            if (order == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy đơn hàng!";
                return RedirectToAction("Index", "Home");
            }
            decimal amount = db.Order_Product.Where(x => x.OrderId == orderId)
                .Sum(x => x.Quantity * x.Price);
            
            // Áp dụng discount nếu có
            if (discountAmount > 0)
            {
                amount = amount - discountAmount;
            }
            
            // Thêm phí ship
            amount += 20000;
            string vnp_Returnurl = System.Configuration.ConfigurationManager.AppSettings["vnp_Returnurl"];
            string vnp_Url = System.Configuration.ConfigurationManager.AppSettings["vnp_Url"];
            string vnp_TmnCode = System.Configuration.ConfigurationManager.AppSettings["vnp_TmnCode"];
            string vnp_HashSecret = System.Configuration.ConfigurationManager.AppSettings["vnp_HashSecret"];

            // Tạo bản ghi Payment (Pending)
            var payment = new WebBanDoTrangMieng.Payment
            {
                OrderId = order.OrderId,
                PaymentMethodId = 7, // VNPAY
                Amount = amount,
                Status = "Pending",
                PaymentDate = DateTime.Now
            };
            db.Payments.Add(payment);
            db.SaveChanges();

            // Build URL VNPAY bằng VnPayLibrary
            var vnpay = new WebBanDoTrangMieng.Helpers.VnPayLibrary();
            vnpay.AddRequestData("vnp_Version", WebBanDoTrangMieng.Helpers.VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", ((long)amount * 100).ToString());
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Request.UserHostAddress);
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", $"Thanh toan don hang: {order.OrderId}");
            vnpay.AddRequestData("vnp_OrderType", "other");
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", payment.PaymentId.ToString());
            vnpay.AddRequestData("vnp_ExpireDate", DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss"));

            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            return Redirect(paymentUrl);
        }

        // Action xử lý ReturnUrl từ VNPAY (hiển thị kết quả cho khách)
        public ActionResult VnpayReturn()
        {
            string vnp_HashSecret = System.Configuration.ConfigurationManager.AppSettings["vnp_HashSecret"];
            var vnpayData = Request.QueryString;
            var vnpay = new WebBanDoTrangMieng.Helpers.VnPayLibrary();
            foreach (string s in vnpayData)
            {
                if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                {
                    vnpay.AddResponseData(s, vnpayData[s]);
                }
            }
            string vnp_SecureHash = vnpayData["vnp_SecureHash"];
            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);

            string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
            string orderId = vnpay.GetResponseData("vnp_TxnRef");
            string vnpayTranId = vnpay.GetResponseData("vnp_TransactionNo");
            string amount = vnpay.GetResponseData("vnp_Amount");
            string bankCode = vnpay.GetResponseData("vnp_BankCode");

            string status = "Thất bại";
            string message = "Thanh toán thất bại. Vui lòng thử lại hoặc liên hệ hỗ trợ.";
            string orderCode = orderId;

            if (checkSignature && (!string.IsNullOrEmpty(orderId)))
            {
                int paymentId;
                if (int.TryParse(orderId, out paymentId))
                {
                    var payment = db.Payments.Find(paymentId);
                    if (payment != null)
                    {
                        if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                        {
                            payment.Status = "Success";
                            var order = db.Orders.Find(payment.OrderId);
                            if (order != null)
                            {
                                order.Status = "Paid";
                                status = "Thành công";
                                message = "Thanh toán thành công cho đơn hàng!";
                                orderCode = order.OrderId.ToString();
                            }
                        }
                        else
                        {
                            payment.Status = "Failed";
                            var order = db.Orders.Find(payment.OrderId);
                            if (order != null)
                            {
                                order.Status = "Cancelled";
                                orderCode = order.OrderId.ToString();
                            }
                        }
                        db.SaveChanges();
                    }
                }
            }
            else
            {
                message = "Dữ liệu thanh toán không hợp lệ.";
            }

            var vm = new OrderStatusVM
            {
                OrderCode = orderCode,
                Status = status,
                Message = message
            };
            return View("OrderStatus", vm);
        }

        // Action xử lý IPN từ VNPAY (cập nhật trạng thái thanh toán vào DB)
        [HttpGet]
        public ActionResult VnpayIpn()
        {
            string vnp_HashSecret = System.Configuration.ConfigurationManager.AppSettings["vnp_HashSecret"];
            var vnpayData = Request.QueryString;
            var vnpay = new WebBanDoTrangMieng.Helpers.VnPayLibrary();
            foreach (string s in vnpayData)
            {
                if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                {
                    vnpay.AddResponseData(s, vnpayData[s]);
                }
            }
            string vnp_SecureHash = vnpayData["vnp_SecureHash"];
            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);

            string rspCode = "99";
            string message = "Unknown error";

            if (checkSignature)
            {
                int paymentId = int.Parse(vnpay.GetResponseData("vnp_TxnRef"));
                decimal vnp_Amount = decimal.Parse(vnpay.GetResponseData("vnp_Amount")) / 100;
                var db = new QLStoreTrangMiengEntities();
                var payment = db.Payments.Find(paymentId);
                if (payment != null)
                {
                    var order = db.Orders.Find(payment.OrderId);
                    if (payment.Status == "Pending")
                    {
                        if (payment.Amount == vnp_Amount &&
                            vnpay.GetResponseData("vnp_ResponseCode") == "00" &&
                            vnpay.GetResponseData("vnp_TransactionStatus") == "00")
                        {
                            payment.Status = "Success";
                            if (order != null) order.Status = "Paid";
                        }
                        else
                        {
                            payment.Status = "Failed";
                            if (order != null) order.Status = "Cancelled";
                        }
                        db.SaveChanges();
                        rspCode = "00";
                        message = "Confirm Success";
                    }
                    else
                    {
                        rspCode = "02";
                        message = "Order already confirmed";
                    }
                }
                else
                {
                    rspCode = "01";
                    message = "Order not found";
                }
            }
            else
            {
                rspCode = "97";
                message = "Invalid signature";
            }

            return Content($"{{\"RspCode\":\"{rspCode}\",\"Message\":\"{message}\"}}", "application/json");
        }

        // Hiển thị trạng thái đơn hàng sau khi thanh toán
        public ActionResult OrderStatus(string orderCode, string status, string message)
        {
            var vm = new OrderStatusVM
            {
                OrderCode = orderCode,
                Status = status,
                Message = message
            };
            return View(vm);
        }
    }
}