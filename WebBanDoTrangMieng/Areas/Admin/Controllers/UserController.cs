using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace WebBanDoTrangMieng.Areas.Admin.Controllers
{
    [WebBanDoTrangMieng.AdminAuthorize]
    public class UserController : Controller
    {
        private readonly QLStoreTrangMiengEntities db = new QLStoreTrangMiengEntities();
        // GET: Admin/User
        public ActionResult Index()
        {
            var users = db.Users.ToList();
            return View(users);
        }

        // GET: Admin/User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.IsActive = true;
                db.Users.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Admin/User/Edit/5
        public ActionResult Edit(int id)
        {
            var user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Find(model.UserId);
                if (user == null)
                {
                    return HttpNotFound();
                }
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.Role = model.Role;
                user.Phone = model.Phone;
                user.Address = model.Address;
                user.IsActive = model.IsActive;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // POST: Admin/User/ToggleActive/5
        [HttpPost]
        public ActionResult ToggleActive(int id)
        {
            var user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            // Cho phép đổi trạng thái cho mọi user, kể cả Customer
            user.IsActive = !(user.IsActive ?? true);
            db.SaveChanges();
            return Json(new { success = true, isActive = user.IsActive });
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