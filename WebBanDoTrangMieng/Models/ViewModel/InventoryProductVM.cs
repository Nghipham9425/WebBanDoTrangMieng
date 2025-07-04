using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanDoTrangMieng.Models.ViewModel
{
    public class InventoryProductVM
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int? StockQuantity { get; set; }
        public string CategoryName { get; set; }
    }
}