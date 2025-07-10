using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanDoTrangMieng.Areas.Admin.Controllers
{
    [WebBanDoTrangMieng.AdminAuthorize]
    public class PromotionController : Controller
    {
        private readonly QLStoreTrangMiengEntities db = new QLStoreTrangMiengEntities();
        // GET: Admin/Promotion
        public ActionResult Index(int page=1,int pageSize=10,string status="")
        {
            var promotions = db.Promotions.AsQueryable();

            // locj theo status
            if(!string.IsNullOrEmpty(status))
            {
                if(status=="active")
                promotions=promotions.Where(p=>p.IsActive==true && p.EndDate>=DateTime.Now);
                else if(status=="expired")
                promotions=promotions.Where(p=>p.EndDate<DateTime.Now);
                else if(status=="inactive")
                promotions=promotions.Where(p=>p.IsActive==false);
            }
            //phan trang
            int totalPromotions=promotions.Count();
            var promotionList=promotions.
            OrderByDescending(p=>p.StartDate).
            Skip((page-1)*pageSize).
            Take(pageSize).
            ToList();
            
            ViewBag.CurrentPage=page;
            ViewBag.TotalPages=Math.Ceiling((double)totalPromotions/pageSize);
            ViewBag.pageSize=pageSize;
            ViewBag.TotalPromotions=totalPromotions;
            ViewBag.CurrentStatus=status;

            return View(promotionList);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Promotion model)
        {
            if(ModelState.IsValid)
            {
                // Kiểm tra mã code đã tồn tại chưa
                var existingPromo = db.Promotions.FirstOrDefault(p => p.Code == model.Code);
                if(existingPromo!=null)
                {
                    ModelState.AddModelError("Code","Mã code đã tồn tại");
                    return View(model);
                }
                // Kiểm tra ngày kết thúc phải lớn hơn ngày bắt đầu
                if(model.EndDate<=model.StartDate)
                {
                    ModelState.AddModelError("EndDate","Ngày kết thúc phải sau ngày bắt đầu");
                    return View(model);
                }
                model.IsActive=true;
                db.Promotions.Add(model);
                db.SaveChanges();

                TempData["Success"]="Tạo mã giảm giá thành công";
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var promotion=db.Promotions.Find(id);
            if(promotion==null)
            return HttpNotFound();

            return View(promotion);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Promotion model)
        {
            if (ModelState.IsValid)
            {
                var existingPromo = db.Promotions.FirstOrDefault(p => p.Code == model.Code && p.PromotionId != model.PromotionId);

                if (existingPromo != null)
                {
                    ModelState.AddModelError("Code", "Mã code đã tồn tại");
                    return View(model);
                }

                // Validate dates
                if (model.StartDate > model.EndDate)
                {
                    ModelState.AddModelError("EndDate", "Ngày kết thúc phải lớn hơn ngày bắt đầu");
                    return View(model);
                }

                db.Entry(model).State = System.Data.EntityState.Modified;
                db.SaveChanges();

                TempData["Success"] = "Cập nhật mã giảm giá thành công!";
                return RedirectToAction("Index");
            }
            return View(model);
        }
         // POST: Admin/Promotion/Delete/5 (Soft Delete)
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var promotion = db.Promotions.Find(id);
            if (promotion == null)
                return Json(new { success = false, message = "Không tìm thấy mã giảm giá!" });

            // Soft delete
            promotion.IsActive = false;
            db.SaveChanges();
            
            return Json(new { success = true, message = "Đã vô hiệu hóa mã giảm giá thành công!" });
        }

        // POST: Admin/Promotion/Restore/5
        [HttpPost]
        public ActionResult Restore(int id)
        {
            var promotion = db.Promotions.Find(id);
            if (promotion == null)
                return Json(new { success = false, message = "Không tìm thấy mã giảm giá!" });

            promotion.IsActive = true;
            db.SaveChanges();
            
            return Json(new { success = true, message = "Đã kích hoạt lại mã giảm giá thành công!" });
        }

        // GET: Admin/Promotion/Details/5
        public ActionResult Details(int id)
        {
            var promotion = db.Promotions.Find(id);
            if (promotion == null)
                return HttpNotFound();

            return View(promotion);
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