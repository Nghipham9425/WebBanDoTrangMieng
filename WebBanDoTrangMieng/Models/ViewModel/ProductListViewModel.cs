using System.Collections.Generic;

namespace WebBanDoTrangMieng.Models.ViewModel
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalProducts { get; set; }
        public int PageSize { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
        
        // Filter properties
        public string SearchTerm { get; set; }
        public int? CategoryId { get; set; }
        public string SortBy { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        
        // Helper collections
        public IEnumerable<Category> Categories { get; set; }
    }
} 