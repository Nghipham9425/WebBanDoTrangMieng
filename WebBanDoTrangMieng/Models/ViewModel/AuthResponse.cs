namespace WebBanDoTrangMieng.Models.ViewModel
{
    public class AuthResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public WebBanDoTrangMieng.Models.User User { get; set; }
        public string RedirectUrl { get; set; }
    }
} 