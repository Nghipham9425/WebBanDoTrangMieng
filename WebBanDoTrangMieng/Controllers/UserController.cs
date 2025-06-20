using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using WebBanDoTrangMieng.Models;
using WebBanDoTrangMieng.Models.ViewModel;


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
                        Password = model.Password, // Nên mã hóa password trong thực tế
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
                        user = new { 
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
                            user = new { 
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