using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using WebBanDoTrangMieng.Models;
using WebBanDoTrangMieng.Models.ViewModel;
using System.Net.Mail;

namespace WebBanDoTrangMieng.Controllers
{
    public class UserController : Controller
    {
        private QLStoreTrangMiengEntities db = new QLStoreTrangMiengEntities();

        // GET: User/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: User/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra email đã tồn tại chưa
                var existingUser = db.Users.SingleOrDefault(u => u.Email == model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "Email này đã được sử dụng!");
                    return View(model);
                }

                // Tạo user mới
                var user = new User()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Password = model.Password, // Nên mã hóa password
                    Phone = model.Phone,
                    Address = model.Address,
                    Role = "Customer",
                    CreatedDate = DateTime.Now,
                    IsActive = true
                };

                db.Users.Add(user);
                db.SaveChanges();

                // Lưu session
                Session["UserId"] = user.UserId;
                Session["UserName"] = user.UserName;
                Session["UserRole"] = user.Role;
                Session["Email"] = user.Email;

                // Tạo cookie xác thực
                FormsAuthentication.SetAuthCookie(user.Email, false);

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        // GET: User/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: User/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra user có tồn tại không
                var user = db.Users.SingleOrDefault(u => u.Email == model.Email && u.Password == model.Password && u.IsActive == true);

                if (user != null)
                {
                    // Lưu thông tin đăng nhập vào session
                    Session["UserId"] = user.UserId;
                    Session["UserName"] = user.UserName;
                    Session["UserRole"] = user.Role;
                    Session["Email"] = user.Email;

                    // Tạo cookie xác thực
                    FormsAuthentication.SetAuthCookie(user.Email, model.RememberMe);

                    // Kiểm tra vai trò người dùng và chuyển hướng
                    if (user.Role == "Admin")
                    {
                        return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                    }
                    else if (user.Role == "Customer")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email hoặc mật khẩu không đúng.");
                }
            }
            return View(model);
        }

        // AJAX: Register từ modal
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult RegisterAjax(RegisterVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Kiểm tra email đã tồn tại chưa
                    var existingUser = db.Users.SingleOrDefault(u => u.Email == model.Email);
                    if (existingUser != null)
                    {
                        return Json(new { success = false, message = "Email này đã được sử dụng!" });
                    }

                    // Kiểm tra username đã tồn tại chưa
                    var existingUserName = db.Users.SingleOrDefault(u => u.UserName == model.UserName);
                    if (existingUserName != null)
                    {
                        return Json(new { success = false, message = "Tên đăng nhập này đã được sử dụng!" });
                    }

                    // Tạo user mới
                    var user = new User()
                    {
                        UserName = model.UserName,
                        Email = model.Email,
                        Password = model.Password,
                        Phone = model.Phone,
                        Address = model.Address,
                        Role = "Customer",
                        CreatedDate = DateTime.Now,
                        IsActive = true
                    };

                    db.Users.Add(user);
                    db.SaveChanges();

                    // Lưu session
                    Session["UserId"] = user.UserId;
                    Session["UserName"] = user.UserName;
                    Session["UserRole"] = user.Role;
                    Session["Email"] = user.Email;

                    // Tạo cookie xác thực
                    FormsAuthentication.SetAuthCookie(user.Email, false);

                    return Json(new
                    {
                        success = true,
                        message = "Đăng ký thành công!",
                        user = new
                        {
                            UserName = user.UserName,
                            Email = user.Email
                        }
                    });
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    return Json(new { success = false, message = string.Join(", ", errors) });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

        // AJAX: Login từ modal
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult LoginAjax(LoginVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Kiểm tra user có tồn tại không
                    var user = db.Users.SingleOrDefault(u => u.Email == model.Email && u.Password == model.Password && u.IsActive == true);

                    if (user != null)
                    {
                        // Lưu thông tin đăng nhập vào session
                        Session["UserId"] = user.UserId;
                        Session["UserName"] = user.UserName;
                        Session["UserRole"] = user.Role;
                        Session["Email"] = user.Email;

                        // Tạo cookie xác thực
                        FormsAuthentication.SetAuthCookie(user.Email, model.RememberMe);

                        return Json(new
                        {
                            success = true,
                            message = "Đăng nhập thành công!",
                            user = new
                            {
                                UserName = user.UserName,
                                Email = user.Email,
                                Role = user.Role
                            }
                        });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Email hoặc mật khẩu không đúng!" });
                    }
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    return Json(new { success = false, message = string.Join(", ", errors) });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

        // GET: User/Logout
        public ActionResult Logout()
        {
            // Xóa session
            Session.Clear();

            // Xóa cookie xác thực
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SendResetPasswordEmail(string email)
        {
    try
    {
        // Kiểm tra xem email có tồn tại trong hệ thống
        var user = db.Users.SingleOrDefault(u => u.Email == email && u.IsActive == true);
        if (user == null)
        {
            return Json(new { success = false, message = "Email không tồn tại trong hệ thống!" });
        }

        // Tạo token reset đơn giản
        string resetToken = Guid.NewGuid().ToString();

        // Tạo link reset 
        string resetLink = Url.Action("ResetPassword", "User", 
            new { token = resetToken, email = email }, Request.Url.Scheme);

        // Tạo nội dung email
        string emailBody = $@"
            <h2>Reset mật khẩu - Đồ Tráng Miệng</h2>
            <p>Xin chào <strong>{user.UserName}</strong>,</p>
            <p>Bạn đã yêu cầu reset mật khẩu. Click vào link bên dưới để đặt lại mật khẩu:</p>
            <p><a href='{resetLink}' style='background: #28a745; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>Reset Mật Khẩu</a></p>
            <p>Link này có hiệu lực trong 24 giờ.</p>
            <p>Nếu bạn không yêu cầu reset mật khẩu, vui lòng bỏ qua email này.</p>
            <hr>
            <p><small>Email này được gửi tự động, vui lòng không reply.</small></p>
        ";

            // Gửi email
            MailMessage mail = new MailMessage();
            mail.To.Add(email);
            mail.Subject = "Reset mật khẩu - Đồ Tráng Miệng";
            mail.Body = emailBody;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Send(mail);

            return Json(new { success = true, message = "Link reset mật khẩu đã được gửi về email của bạn!" });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = "Có lỗi xảy ra khi gửi mail: " + ex.Message });
        }
    }
    public ActionResult ResetPassword(string token,string email)
    {
       //Kiem tra token va email co hop le khong
       if(string.IsNullOrEmpty(token) || string.IsNullOrEmpty(email))
       {
         ViewBag.Error="Link reset mat khau khong hop le";
         return View();
       }
       //Kiem tra email co ton tai khong
       var user=db.Users.SingleOrDefault(u=>u.Email==email && u.IsActive==true);
       if(user==null)
       {
        ViewBag.Error="Email khong ton tai";
        return View();
       }
       ViewBag.Token=token;
       ViewBag.Email=email;
       ViewBag.UserName=user.UserName;
       return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
     public ActionResult ResetPassword(string token,string email,string newPassword,string confirmPassword)
    {
      try{
            //validate
            if(string.IsNullOrEmpty(token) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(newPassword) 
            || string.IsNullOrEmpty(confirmPassword))
            {
                ViewBag.Error="Vui lòng nhập đầy đủ thông tin";
                ViewBag.Token=token;
                ViewBag.Email=email;
                return View();
            }
            if(newPassword!=confirmPassword)
            {
                ViewBag.Error="Mật khẩu không trùng khớp!";
                ViewBag.Token=token;
                ViewBag.Email=email;
                return View();
            }
            if(newPassword.Length<6)
            {
                ViewBag.Error="Mật khẩu phải có ít nhất 6 ký tự!";
                ViewBag.Token=token;
                ViewBag.Email=email;
                return View();
            }
            //Tim Username và update pass
            var user=db.Users.SingleOrDefault(u=>u.Email==email && u.IsActive==true);
            if(user==null)
            {
                ViewBag.Error="Email khong ton tai trong he thong !";
                return View();
            }
            //Cap nhat mat khau moi
            user.Password=newPassword;
            db.SaveChanges();

            ViewBag.Success="Update mat khau thanh cong !";
            return View();
      }
      catch(Exception ex)
      {
        ViewBag.Error="Có lỗi xảy ra: "+ex.Message;
        ViewBag.Token=token;
        ViewBag.Email=email;
        return View();
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