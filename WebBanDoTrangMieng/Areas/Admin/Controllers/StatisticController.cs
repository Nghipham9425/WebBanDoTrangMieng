using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Web.Mvc;
using System.Data.Entity;
using WebBanDoTrangMieng.Models.ViewModel;

namespace WebBanDoTrangMieng.Areas.Admin.Controllers
{
    [WebBanDoTrangMieng.AdminAuthorize]
    public class StatisticController : Controller
    {
        private QLStoreTrangMiengEntities db = new QLStoreTrangMiengEntities();

        // Method chung để lấy dữ liệu doanh thu
        private List<RevenueReportVM> GetRevenueData(DateTime? from, DateTime? to)
        {
            // Nếu không có filter, lấy 30 ngày gần nhất
            if (!from.HasValue && !to.HasValue)
            {
                to = DateTime.Now.Date;
                from = to.Value.AddDays(-30);
            }

            // Query dữ liệu doanh thu
            var query = db.Orders.Where(o => o.Status == "Delivered" || o.Status == "Paid").AsQueryable();
            
            if (from.HasValue)
                query = query.Where(o => o.OrderDate >= from.Value);
            if (to.HasValue)
                query = query.Where(o => o.OrderDate <= to.Value);

            // Sử dụng cách đơn giản: GroupBy Year, Month, Day rồi AsEnumerable()
            return query
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
                .AsEnumerable()
                .Select(x => new RevenueReportVM
                {
                    Date = new DateTime(x.Year, x.Month, x.Day),
                    TotalRevenue = x.TotalRevenue,
                    OrderCount = x.OrderCount,
                    ProductSold = x.ProductSold
                })
                .OrderBy(r => r.Date)
                .ToList();
        }

        // GET: Admin/Statistic
        public ActionResult Index(DateTime? from, DateTime? to)
        {
            var report = GetRevenueData(from, to);

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

        // GET: Admin/Statistic/ExportRevenueReport
        public ActionResult ExportRevenueReport(DateTime? from, DateTime? to, string format = "pdf")
        {
            try
            {
                var revenueData = GetRevenueData(from, to);
                return GeneratePDFReport(revenueData, from, to);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi xuất báo cáo: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        // Method tạo PDF report
        private ActionResult GeneratePDFReport(List<RevenueReportVM> revenueData, DateTime? from, DateTime? to)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Tạo document
                Document document = new Document(PageSize.A4, 40, 40, 40, 40);
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();

                // Font Unicode hỗ trợ tiếng Việt
                BaseFont baseFont;
                try
                {
                    baseFont = BaseFont.CreateFont(Server.MapPath("~/Content/arial-unicode-ms.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                }
                catch
                {
                    baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                }
                
                Font titleFont = new Font(baseFont, 22, Font.BOLD, BaseColor.BLACK);
                Font headerFont = new Font(baseFont, 13, Font.BOLD, BaseColor.WHITE);
                Font normalFont = new Font(baseFont, 11, Font.NORMAL, BaseColor.BLACK);
                Font totalFont = new Font(baseFont, 13, Font.BOLD, BaseColor.BLACK);

                // Tiêu đề
                Paragraph title = new Paragraph("BÁO CÁO DOANH THU", titleFont);
                title.Alignment = Element.ALIGN_CENTER;
                title.SpacingAfter = 10f;
                document.Add(title);

                // Thông tin thời gian
                string timeRange = $"Từ ngày: {(from?.ToString("dd/MM/yyyy") ?? "Tất cả")} - Đến ngày: {(to?.ToString("dd/MM/yyyy") ?? "Tất cả")}";
                Paragraph timeInfo = new Paragraph(timeRange, normalFont);
                timeInfo.Alignment = Element.ALIGN_CENTER;
                timeInfo.SpacingAfter = 15f;
                document.Add(timeInfo);

                // Tạo bảng
                PdfPTable table = new PdfPTable(4);
                table.WidthPercentage = 100;
                table.SetWidths(new float[] { 2.2f, 2.5f, 1.5f, 1.8f });
                table.SpacingBefore = 10f;
                table.SpacingAfter = 10f;

                // Header bảng
                BaseColor headerBg = new BaseColor(60, 60, 60);
                string[] headers = { "Ngày", "Doanh thu (VNĐ)", "Số đơn hàng", "Số sản phẩm" };
                foreach (var h in headers)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(h, headerFont))
                    {
                        BackgroundColor = headerBg,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        Padding = 7f
                    };
                    table.AddCell(cell);
                }

                // Dữ liệu
                foreach (var item in revenueData)
                {
                    PdfPCell cell1 = new PdfPCell(new Phrase(item.Date.ToString("dd/MM/yyyy"), normalFont));
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell1.Padding = 5f;
                    table.AddCell(cell1);

                    PdfPCell cell2 = new PdfPCell(new Phrase(item.TotalRevenue.ToString("N0"), normalFont));
                    cell2.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell2.Padding = 5f;
                    table.AddCell(cell2);

                    PdfPCell cell3 = new PdfPCell(new Phrase(item.OrderCount.ToString(), normalFont));
                    cell3.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell3.Padding = 5f;
                    table.AddCell(cell3);

                    PdfPCell cell4 = new PdfPCell(new Phrase(item.ProductSold.ToString(), normalFont));
                    cell4.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell4.Padding = 5f;
                    table.AddCell(cell4);
                }

                // Tổng cộng
                PdfPCell totalLabel = new PdfPCell(new Phrase("TỔNG CỘNG", totalFont))
                {
                    BackgroundColor = new BaseColor(220, 220, 220),
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Padding = 7f
                };
                table.AddCell(totalLabel);

                PdfPCell totalRevenue = new PdfPCell(new Phrase(revenueData.Sum(x => x.TotalRevenue).ToString("N0"), totalFont))
                {
                    BackgroundColor = new BaseColor(220, 220, 220),
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    Padding = 7f
                };
                table.AddCell(totalRevenue);

                PdfPCell totalOrder = new PdfPCell(new Phrase(revenueData.Sum(x => x.OrderCount).ToString(), totalFont))
                {
                    BackgroundColor = new BaseColor(220, 220, 220),
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Padding = 7f
                };
                table.AddCell(totalOrder);

                PdfPCell totalProduct = new PdfPCell(new Phrase(revenueData.Sum(x => x.ProductSold).ToString(), totalFont))
                {
                    BackgroundColor = new BaseColor(220, 220, 220),
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Padding = 7f
                };
                table.AddCell(totalProduct);

                document.Add(table);
                document.Close();

                // Trả về file
                byte[] bytes = ms.ToArray();
                string fileName = $"BaoCaoDoanhThu_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                return File(bytes, "application/pdf", fileName);
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