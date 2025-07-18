using System;

namespace WebBanDoTrangMieng.Models.ViewModel
{
    public class RevenueReportVM
    {
        public DateTime Date { get; set; }
        public decimal TotalRevenue { get; set; }
        public int OrderCount { get; set; }
        public int ProductSold { get; set; }
    }
}