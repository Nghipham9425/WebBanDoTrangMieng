# 🍰 Website Bán Bánh Tráng Miệng

## 📖 Giới thiệu

Website thương mại điện tử chuyên bán các loại bánh tráng miệng và đồ uống cao cấp. Được phát triển bằng ASP.NET MVC với Entity Framework Database First.

## ✨ Tính năng chính

### 🛍️ **Khách hàng**

- **Trang chủ:** Giới thiệu sản phẩm nổi bật với video banner
- **Danh sách sản phẩm:** Lọc theo danh mục, giá, tìm kiếm, sắp xếp
- **Chi tiết sản phẩm:** Xem ảnh, mô tả, đánh giá, chọn số lượng
- **Giỏ hàng:** Thêm/xóa/cập nhật sản phẩm real-time (Session-based)
- **Đăng ký/Đăng nhập:** Quản lý tài khoản khách hàng
- **Thanh toán:** Quy trình checkout hoàn chỉnh

### 👨‍💼 **Quản trị viên**

- **Dashboard:** Thống kê tổng quan (đang phát triển)
- **Quản lý sản phẩm:** CRUD sản phẩm với upload ảnh
- **Quản lý đơn hàng:** Xem chi tiết, cập nhật trạng thái đơn hàng
- **Quản lý khách hàng:** Xem danh sách và thông tin khách hàng

## 🛠️ Công nghệ sử dụng

### **Backend**

- **Framework:** ASP.NET MVC 5
- **Database:** SQL Server với Entity Framework 6 (Database First)
- **Authentication:** ASP.NET Identity
- **Session:** Quản lý giỏ hàng

### **Frontend**

- **UI Framework:** Bootstrap 5
- **JavaScript:** jQuery, AJAX
- **Icons:** Font Awesome
- **Responsive:** Mobile-first design

### **Database**

- **RDBMS:** SQL Server
- **ORM:** Entity Framework 6 (Database First)
- **Tables:** Product, Category, Order, User, Review, Payment...

## 📁 Cấu trúc dự án

```
WebBanDoTrangMieng/
├── Controllers/           # Controllers chính
│   ├── HomeController.cs
│   ├── ProductController.cs
│   ├── CartController.cs
│   └── UserController.cs
├── Areas/
│   └── Admin/            # Khu vực quản trị
│       ├── Controllers/
│       └── Views/
├── Models/               # Models tùy chỉnh
│   ├── CartItem.cs
│   └── ViewModel/
├── Views/                # Razor Views
│   ├── Home/
│   ├── Product/
│   ├── Cart/
│   └── Shared/
├── Content/              # CSS, Images
│   ├── Images/Products/
│   └── *.css
├── Scripts/              # JavaScript files
└── *.cs                  # Entity Framework Models
```

## 🚀 Hướng dẫn cài đặt

### **1. Yêu cầu hệ thống**

- Visual Studio 2019+ hoặc Visual Studio Code
- .NET Framework 4.7.2+
- SQL Server 2017+ hoặc SQL Server Express
- IIS Express (có sẵn trong Visual Studio)

### **2. Clone repository**

```bash
git clone https://github.com/your-username/WebBanDoTrangMieng.git
cd WebBanDoTrangMieng
```

### **3. Cấu hình Database**

1. Mở SQL Server Management Studio
2. Tạo database mới tên `QLStoreTrangMieng`
3. Chạy script trong file `db_Script.txt`
4. Cập nhật connection string trong `Web.config`:

```xml
<connectionStrings>
  <add name="QLStoreTrangMiengEntities"
       connectionString="data source=YOUR_SERVER;initial catalog=QLStoreTrangMieng;integrated security=True"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

### **4. Chạy ứng dụng**

1. Mở solution trong Visual Studio
2. Build solution (Ctrl + Shift + B)
3. Chạy project (F5 hoặc Ctrl + F5)

### **5. Tài khoản mặc định**

- **Admin:** admin@example.com / password123
- **User:** user@example.com / password123

## 📊 Database Schema

### **Bảng chính:**

- **Product:** Sản phẩm (ID, Name, Price, ImageUrl, CategoryId...)
- **Category:** Danh mục sản phẩm
- **User:** Người dùng và quản trị viên
- **Order:** Đơn hàng
- **Order_Product:** Chi tiết đơn hàng
- **Review:** Đánh giá sản phẩm
- **Payment:** Thông tin thanh toán

## 🎯 Tính năng đã hoàn thành

### ✅ **Core Features**

- [x] Hiển thị danh sách sản phẩm với phân trang
- [x] Lọc sản phẩm theo danh mục và giá
- [x] Tìm kiếm sản phẩm
- [x] Chi tiết sản phẩm với gallery ảnh
- [x] Giỏ hàng Session-based hoàn chỉnh
- [x] Thêm/xóa/cập nhật giỏ hàng real-time
- [x] Đăng ký/đăng nhập khách hàng
- [x] Admin quản lý sản phẩm (CRUD)
- [x] Admin quản lý đơn hàng

### 🚧 **Đang phát triển**

- [ ] Tích hợp thanh toán MoMo
- [ ] Hệ thống đánh giá sản phẩm thực tế
- [ ] Dashboard admin với thống kê
- [ ] Email notifications
- [ ] Wishlist functionality

## 📱 Screenshots

### **Trang chủ**

- Hero banner với video background
- Sản phẩm nổi bật
- Responsive design

### **Danh sách sản phẩm**

- Grid/List view toggle
- Sidebar filters
- Sort options

### **Giỏ hàng**

- Real-time quantity updates
- Price calculations
- Checkout flow

### **Admin Panel**

- Product management
- Order tracking
- Clean interface

## 🔧 API Endpoints

### **Cart API**

- `POST /Cart/AddToCart` - Thêm sản phẩm
- `POST /Cart/UpdateCart` - Cập nhật số lượng
- `POST /Cart/RemoveItem` - Xóa sản phẩm
- `GET /Cart/GetCartCount` - Lấy số lượng items

### **Product API**

- `GET /Product` - Danh sách sản phẩm
- `GET /Product/Detail/{id}` - Chi tiết sản phẩm

## 🤝 Đóng góp

1. Fork repository
2. Tạo feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to branch (`git push origin feature/AmazingFeature`)
5. Tạo Pull Request

## 📝 License

Distributed under the MIT License. See `LICENSE` for more information.

## 👥 Tác giả

- **Tên:** [Tên của bạn]
- **Email:** [Email của bạn]
- **GitHub:** [GitHub profile]

## 🙏 Acknowledgments

- Bootstrap team for the amazing CSS framework
- Font Awesome for icons
- ASP.NET MVC community
- Entity Framework documentation

---

## 📞 Liên hệ

Nếu có bất kỳ câu hỏi nào, vui lòng liên hệ qua:

- **Email:** your.email@example.com
- **GitHub Issues:** [Project Issues](https://github.com/your-username/WebBanDoTrangMieng/issues)

**Made with ❤️ for learning purposes**
