using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBanDoTrangMieng.Models
{
    public class CartItem
    {
      public int ProductId {get; set;}
      public string ProductName {get; set;}
      public decimal Price {get; set;}
      public int Quantity {get; set;}
      public decimal TotalPrice {get; set;}
      public string ImageUrl {get; set;}
     
     public decimal Total => Price * Quantity;
    }

    public class Cart
    {
      public List<CartItem> Items {get; set;} = new List<CartItem>();

      public decimal TotalAmount => Items.Sum(item => item.Total);
      public int TotalItems => Items.Sum(item => item.Quantity);
    }
}