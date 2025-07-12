using System;
using System.Linq;
using System.Net.Mail;
using System.Text;
using WebBanDoTrangMieng.Models.ViewModel;

namespace WebBanDoTrangMieng.Helpers
{
    public static class EmailHelper
    {
        public static bool SendOrderConfirmationEmail(int orderId)
        {
            try
            {

                using (var db = new QLStoreTrangMiengEntities())
                {
                    // Lấy thông tin đơn hàng
                    var order = db.Orders.Find(orderId);
                    if (order == null) return false;

                    // Lấy chi tiết sản phẩm trong đơn hàng
                    var orderProducts = db.Order_Product
                        .Where(op => op.OrderId == orderId)
                        .Select(op => new
                        {
                            ProductName = op.Product.Name,
                            Quantity = op.Quantity,
                            Price = op.Price,
                            Total = op.Quantity * op.Price,
                            ImageUrl = op.Product.ImageUrl
                        }).ToList();

                    // Tính tổng tiền
                    decimal subtotal = orderProducts.Sum(p => p.Total);
                    decimal shipping = 20000; // Phí ship cố định
                    decimal total = subtotal + shipping;

                    // Tạo nội dung email HTML
                    var emailBody = CreateOrderConfirmationEmailTemplate(order, orderProducts, subtotal, shipping, total);

                    // Gửi email
                    MailMessage mail = new MailMessage();
                    mail.To.Add(order.User.Email);
                    mail.Subject = $"Xác nhận đơn hàng #{order.OrderId} - Đồ Tráng Miệng";
                    mail.Body = emailBody;
                    mail.IsBodyHtml = true;

                    SmtpClient smtp = new SmtpClient();
                    smtp.Send(mail);

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

                private static string CreateOrderConfirmationEmailTemplate(Order order, object orderProducts, decimal subtotal, decimal shipping, decimal total)
        {
            // Cast orderProducts về IEnumerable để sử dụng được
            var products = orderProducts as System.Collections.IEnumerable;
            
            var productHtml = "";
            foreach (dynamic product in products)
            {
                productHtml += $@"
                <div class='product'>
                    <div>
                        <div class='product-name'>{product.ProductName}</div>
                        <div class='product-details'>Số lượng: {product.Quantity} × {product.Price:N0}đ</div>
                    </div>
                    <div class='product-price'>{product.Total:N0}đ</div>
                </div>";
            }

            return $@"<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1'>
    <style>
        * {{ margin: 0; padding: 0; box-sizing: border-box; }}
        body {{ 
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            padding: 20px;
            min-height: 100vh;
        }}
        .email-card {{
            max-width: 600px;
            margin: 0 auto;
            background: white;
            border-radius: 20px;
            box-shadow: 0 20px 40px rgba(0,0,0,0.1);
            overflow: hidden;
        }}
        .header {{
            background: linear-gradient(45deg, #ff6b6b, #ee5a24);
            color: white;
            padding: 40px 30px;
            text-align: center;
        }}
        .header h1 {{ font-size: 28px; margin-bottom: 10px; }}
        .header p {{ font-size: 16px; opacity: 0.9; }}
        .content {{ padding: 40px 30px; }}
        .greeting {{ font-size: 18px; color: #2c3e50; margin-bottom: 20px; }}
        .info-box {{
            background: #f8f9fa;
            border-left: 4px solid #3498db;
            padding: 20px;
            margin: 25px 0;
            border-radius: 8px;
        }}
        .info-title {{ color: #2c3e50; font-size: 16px; font-weight: bold; margin-bottom: 15px; }}
        .info-row {{ margin: 8px 0; color: #555; }}
        .info-row strong {{ color: #2c3e50; }}
        .status {{ color: #27ae60; font-weight: bold; }}
        .products {{ margin: 30px 0; }}
        .product {{
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding: 15px;
            border-bottom: 1px solid #ecf0f1;
            background: #fdfdfd;
        }}
        .product:last-child {{ border-bottom: none; }}
        .product-name {{ font-weight: bold; color: #2c3e50; }}
        .product-details {{ color: #7f8c8d; font-size: 14px; }}
        .product-price {{ color: #e74c3c; font-weight: bold; }}
        .total-box {{
            background: linear-gradient(45deg, #74b9ff, #0984e3);
            color: white;
            padding: 25px;
            border-radius: 15px;
            margin: 30px 0;
            text-align: center;
        }}
        .total-row {{ 
            display: flex; 
            justify-content: space-between; 
            margin: 8px 0;
            font-size: 16px;
        }}
        .total-final {{
            border-top: 2px solid rgba(255,255,255,0.3);
            padding-top: 15px;
            margin-top: 15px;
            font-size: 20px;
            font-weight: bold;
        }}
        .highlight {{ background: #fff3cd; padding: 15px; border-radius: 8px; margin: 20px 0; }}
        .footer {{
            background: #2c3e50;
            color: white;
            padding: 30px;
            text-align: center;
        }}
        .footer p {{ margin: 10px 0; opacity: 0.8; }}
        @media (max-width: 600px) {{
            .email-card {{ margin: 10px; border-radius: 15px; }}
            .header, .content, .footer {{ padding: 20px; }}
            .product {{ flex-direction: column; align-items: flex-start; }}
            .product-price {{ margin-top: 5px; }}
        }}
    </style>
</head>
<body>
    <div class='email-card'>
        <!-- Header -->
        <div class='header'>
            <h1>🎉 Đơn hàng thành công!</h1>
            <p>Cảm ơn bạn đã tin tưởng Đồ Tráng Miệng</p>
        </div>
        
        <!-- Content -->
        <div class='content'>
            <div class='greeting'>
                Xin chào <strong>{order.User.UserName}</strong>! 👋
            </div>
            <p>Chúng tôi đã nhận được đơn hàng của bạn và đang chuẩn bị những món tráng miệng thơm ngon nhất!</p>
            
            <!-- Order Info -->
            <div class='info-box'>
                <div class='info-title'>📋 Chi tiết đơn hàng</div>
                <div class='info-row'><strong>Mã đơn:</strong> #{order.OrderId}</div>
                <div class='info-row'><strong>Ngày đặt:</strong> {order.OrderDate:dd/MM/yyyy lúc HH:mm}</div>
                <div class='info-row'><strong>Trạng thái:</strong> <span class='status'>{order.Status}</span></div>
                <div class='info-row'><strong>Giao đến:</strong> {order.ShippingAddress}</div>
            </div>
            
            <!-- Products -->
            <div class='info-title'>🛍️ Sản phẩm đã đặt</div>
            <div class='products'>
                {productHtml}
            </div>
            
            <!-- Total -->
            <div class='total-box'>
                <div class='total-row'>
                    <span>Tạm tính:</span>
                    <span>{subtotal:N0}đ</span>
                </div>
                <div class='total-row'>
                    <span>Phí giao hàng:</span>
                    <span>{shipping:N0}đ</span>
                </div>
                <div class='total-final'>
                    <div class='total-row'>
                        <span>Tổng cộng:</span>
                        <span>{total:N0}đ</span>
                    </div>
                </div>
            </div>
            
            <!-- Delivery Info -->
            <div class='highlight'>
                <strong>🚚 Thời gian giao hàng:</strong> 2-3 ngày làm việc<br>
                <strong>📞 Hỗ trợ khách hàng:</strong> 0123-456-789
            </div>
            
            <p style='text-align: center; margin-top: 30px; color: #7f8c8d;'>
                Chúng tôi sẽ gửi thông báo khi đơn hàng được giao nhé! 🧁
            </p>
        </div>
        
        <!-- Footer -->
        <div class='footer'>
            <p><strong>Đồ Tráng Miệng</strong></p>
            <p>Ngọt ngào mỗi ngày ❤️</p>
            <p style='font-size: 12px;'>Email tự động - không cần phản hồi</p>
        </div>
    </div>
</body>
</html>";
        }
        
        public static void SendContactEmail(ContactVM contactInfo, string userEmail)
        {
            try
            {
                using (var mail = new MailMessage())
                {
                    mail.From = new MailAddress("dotrangmieng.contact@gmail.com", "Đồ Tráng Miệng Website");
                    mail.To.Add("admin@dotrangmieng.com"); // Thay bằng email admin thật
                    mail.Subject = $"[Liên hệ] {contactInfo.Subject}";
                    mail.Body = CreateContactEmailTemplate(contactInfo, userEmail);
                    mail.IsBodyHtml = true;
                    mail.BodyEncoding = Encoding.UTF8;
                    mail.SubjectEncoding = Encoding.UTF8;

                    using (var smtp = new SmtpClient())
                    {
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Không thể gửi email liên hệ: " + ex.Message);
            }
        }

        private static string CreateContactEmailTemplate(ContactVM contact, string userEmail)
        {
            return $@"<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1'>
    <style>
        * {{ margin: 0; padding: 0; box-sizing: border-box; }}
        body {{ 
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: #f8f9fa;
            padding: 20px;
        }}
        .email-container {{
            max-width: 600px;
            margin: 0 auto;
            background: white;
            border-radius: 12px;
            box-shadow: 0 4px 12px rgba(0,0,0,0.1);
            overflow: hidden;
        }}
        .header {{
            background: linear-gradient(45deg, #28a745, #20c997);
            color: white;
            padding: 30px;
            text-align: center;
        }}
        .content {{ padding: 30px; }}
        .info-box {{
            background: #f8f9fa;
            border-left: 4px solid #28a745;
            padding: 20px;
            margin: 20px 0;
            border-radius: 8px;
        }}
        .label {{ font-weight: bold; color: #495057; margin-bottom: 5px; }}
        .value {{ color: #212529; margin-bottom: 15px; }}
        .message-box {{
            background: #e3f2fd;
            padding: 20px;
            border-radius: 8px;
            margin-top: 20px;
        }}
        .footer {{
            background: #6c757d;
            color: white;
            padding: 20px;
            text-align: center;
            font-size: 14px;
        }}
        .priority {{ background: #fff3cd; border: 1px solid #ffeaa7; color: #856404; padding: 10px; border-radius: 5px; margin: 15px 0; }}
    </style>
</head>
<body>
    <div class='email-container'>
        <div class='header'>
            <h1>📧 Tin nhắn liên hệ mới</h1>
            <p>Từ website Đồ Tráng Miệng</p>
        </div>
        
        <div class='content'>
            <div class='priority'>
                ⚡ <strong>Tin nhắn cần phản hồi từ khách hàng</strong>
            </div>
            
            <p>Bạn có một tin nhắn liên hệ mới từ khách hàng đã đăng ký:</p>
            
            <div class='info-box'>
                <div class='label'>👤 Họ và tên:</div>
                <div class='value'><strong>{contact.Name}</strong></div>
                
                <div class='label'>📧 Email liên hệ:</div>
                <div class='value'><a href='mailto:{contact.Email}'>{contact.Email}</a></div>
                
                <div class='label'>📱 Số điện thoại:</div>
                <div class='value'>{(string.IsNullOrEmpty(contact.Phone) ? "Không cung cấp" : $"<a href='tel:{contact.Phone}'>{contact.Phone}</a>")}</div>
                
                <div class='label'>🏷️ Tiêu đề:</div>
                <div class='value'><strong>{contact.Subject}</strong></div>
                
                <div class='label'>🔐 Tài khoản website:</div>
                <div class='value'>{userEmail}</div>
                
                <div class='label'>⏰ Thời gian gửi:</div>
                <div class='value'>{DateTime.Now:dd/MM/yyyy HH:mm:ss}</div>
            </div>
            
            <div class='message-box'>
                <div class='label'>💬 Nội dung tin nhắn:</div>
                <p style='margin-top: 10px; line-height: 1.6; white-space: pre-wrap; background: white; padding: 15px; border-radius: 5px;'>{contact.Message}</p>
            </div>
            
            <div style='background: #d4edda; border: 1px solid #c3e6cb; color: #155724; padding: 15px; border-radius: 8px; margin-top: 20px;'>
                <strong>📋 Hành động cần làm:</strong><br>
                1. Phản hồi khách hàng qua email: <a href='mailto:{contact.Email}?subject=Re: {contact.Subject}' style='color: #155724;'><strong>{contact.Email}</strong></a><br>
                2. Hoặc gọi điện: {(string.IsNullOrEmpty(contact.Phone) ? "Không có SĐT" : contact.Phone)}<br>
                3. Cập nhật trạng thái trong hệ thống quản lý
            </div>
        </div>
        
        <div class='footer'>
            <p><strong>© 2024 Đồ Tráng Miệng</strong></p>
            <p>Hệ thống thông báo tự động - Vui lòng phản hồi khách hàng sớm nhất</p>
        </div>
    </div>
</body>
</html>";
        }
        
        public static void SendPromotionEmail(User customer, Promotion promotion, string emailSubject, string emailMessage)
        {
            try
            {
                using (var mail = new MailMessage())
                {
                    mail.From = new MailAddress("dotrangmieng.promo@gmail.com", "Đồ Tráng Miệng");
                    mail.To.Add(customer.Email);
                    mail.Subject = emailSubject;
                    mail.Body = CreatePromotionEmailTemplate(customer, promotion, emailSubject, emailMessage);
                    mail.IsBodyHtml = true;
                    mail.BodyEncoding = Encoding.UTF8;
                    mail.SubjectEncoding = Encoding.UTF8;

                    using (var smtp = new SmtpClient())
                    {
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Không thể gửi email khuyến mãi cho {customer.Email}: " + ex.Message);
            }
        }

        private static string CreatePromotionEmailTemplate(User customer, Promotion promotion, string emailSubject, string emailMessage)
        {
            return $@"<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1'>
    <style>
        * {{ margin: 0; padding: 0; box-sizing: border-box; }}
        body {{ 
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            padding: 20px;
        }}
        .email-container {{
            max-width: 600px;
            margin: 0 auto;
            background: white;
            border-radius: 20px;
            box-shadow: 0 10px 30px rgba(0,0,0,0.2);
            overflow: hidden;
        }}
        .header {{
            background: linear-gradient(45deg, #ff6b6b, #ff8e53);
            color: white;
            padding: 40px 30px;
            text-align: center;
            position: relative;
        }}
        .header::after {{
            content: '';
            position: absolute;
            bottom: -10px;
            left: 0;
            right: 0;
            height: 20px;
            background: white;
            border-radius: 20px 20px 0 0;
        }}
        .content {{ padding: 40px 30px; }}
        .promo-card {{
            background: linear-gradient(45deg, #28a745, #20c997);
            color: white;
            padding: 30px;
            border-radius: 15px;
            text-align: center;
            margin: 25px 0;
            position: relative;
            overflow: hidden;
        }}
        .promo-card::before {{
            content: '🎉';
            position: absolute;
            top: -10px;
            right: -10px;
            font-size: 50px;
            opacity: 0.3;
        }}
        .promo-code {{
            background: rgba(255,255,255,0.2);
            padding: 15px;
            border-radius: 10px;
            margin: 15px 0;
            border: 2px dashed rgba(255,255,255,0.5);
        }}
        .cta-button {{
            display: inline-block;
            background: linear-gradient(45deg, #ff6b6b, #ff8e53);
            color: white;
            padding: 15px 30px;
            text-decoration: none;
            border-radius: 50px;
            font-weight: bold;
            margin-top: 20px;
            box-shadow: 0 5px 15px rgba(255,107,107,0.3);
        }}
        .message-box {{
            background: #f8f9fa;
            padding: 25px;
            border-radius: 15px;
            margin: 20px 0;
            border-left: 5px solid #28a745;
        }}
        .footer {{
            background: #2c3e50;
            color: white;
            padding: 30px;
            text-align: center;
        }}
    </style>
</head>
<body>
    <div class='email-container'>
        <div class='header'>
            <h1>🎉 {emailSubject}</h1>
            <p>Ưu đãi đặc biệt dành riêng cho bạn!</p>
        </div>
        
        <div class='content'>
            <div style='text-align: center; margin-bottom: 30px;'>
                <h2>Xin chào <span style='color: #28a745;'>{customer.UserName}</span>! 👋</h2>
            </div>
            
            <div class='message-box'>
                <p style='line-height: 1.8; white-space: pre-wrap;'>{emailMessage}</p>
            </div>
            
            <div class='promo-card'>
                <h3 style='margin-bottom: 15px;'>🏷️ Mã giảm giá của bạn</h3>
                <div class='promo-code'>
                    <h1 style='font-size: 32px; font-weight: bold; margin: 10px 0;'>{promotion.Code}</h1>
                    <p style='font-size: 18px; opacity: 0.9;'>Giảm {promotion.DiscountPercent}% đơn hàng</p>
                </div>
                <p style='font-size: 14px; opacity: 0.8; margin-top: 15px;'>
                    ⏰ Có hiệu lực từ {promotion.StartDate:dd/MM/yyyy} đến {promotion.EndDate:dd/MM/yyyy}
                </p>
                <a href='#' class='cta-button'>🛍️ Mua ngay</a>
            </div>
            
            <div style='background: #fff3cd; border: 1px solid #ffeaa7; color: #856404; padding: 20px; border-radius: 10px; margin-top: 25px;'>
                <p><strong>📝 Cách sử dụng:</strong></p>
                <p>1. Chọn sản phẩm yêu thích</p>
                <p>2. Nhập mã <strong>{promotion.Code}</strong> khi thanh toán</p>
                <p>3. Tận hưởng ưu đãi {promotion.DiscountPercent}%!</p>
            </div>
            
            <p style='text-align: center; margin-top: 30px; color: #7f8c8d; font-style: italic;'>
                Đừng bỏ lỡ cơ hội vàng này! 🧁✨
            </p>
        </div>
        
        <div class='footer'>
            <p><strong>🧁 Đồ Tráng Miệng</strong></p>
            <p>Ngọt ngào mỗi ngày ❤️</p>
            <p style='font-size: 12px; opacity: 0.7; margin-top: 10px;'>
                Bạn nhận được email này vì đăng ký nhận thông tin khuyến mãi.<br>
                <a href='#' style='color: #ecf0f1;'>Hủy đăng ký</a>
            </p>
        </div>
    </div>
</body>
</html>";
        }
    }
} 