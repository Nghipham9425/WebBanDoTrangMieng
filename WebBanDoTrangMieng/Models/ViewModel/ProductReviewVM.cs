using System;
using WebBanDoTrangMieng;

namespace WebBanDoTrangMieng.Models.ViewModel
{
    public class ProductReviewVM
    {
        public Product Product { get; set; }
        public int ReviewCount { get; set; }
        public int VisibleReviewCount { get; set; }
        public int HiddenReviewCount { get; set; }
        public double AverageRating { get; set; }
        public DateTime? LatestReview { get; set; }
    }
}