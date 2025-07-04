using System;

namespace WebBanDoTrangMieng.Models.ViewModel
{
    public class ReviewVM
    {
        public int ReviewId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsVisible { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public int OrderId { get; set; }
    }
} 