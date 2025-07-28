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

### **2. Cấu hình Database**

1. **Cài đặt SQL Server**

   - Tải và cài đặt SQL Server Express từ Microsoft
   - Hoặc sử dụng SQL Server Management Studio (SSMS)

2. **Tạo Database**

   - Mở SQL Server Management Studio
   - Kết nối đến SQL Server instance
   - Tạo database mới tên `QLStoreTrangMieng`

3. **Import dữ liệu**
   - Mở file `db_Script.txt` trong project
   - Copy toàn bộ nội dung script
   - Trong SSMS, chọn database `QLStoreTrangMieng`
   - Mở New Query, paste script và Execute

### **3. Cấu hình Connection String**

1. **Mở file Web.config** trong project
2. **Tìm section connectionStrings**
3. **Cập nhật thông tin kết nối:**

```xml
<connectionStrings>
  <add name="QLStoreTrangMiengEntities"
       connectionString="data source=YOUR_SERVER_NAME;initial catalog=QLStoreTrangMieng;integrated security=True;MultipleActiveResultSets=True"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

**Lưu ý:** Thay `YOUR_SERVER_NAME` bằng tên SQL Server instance của bạn (thường là `localhost` hoặc `.\SQLEXPRESS`)

### **4. Build và chạy ứng dụng**

1. **Mở project trong Visual Studio**

   - Mở file `WebBanDoTrangMieng.sln`
   - Đợi Visual Studio restore packages

2. **Build Solution**

   - Nhấn `Ctrl + Shift + B` hoặc
   - Menu Build → Build Solution

3. **Chạy ứng dụng**
   - Nhấn `F5` để debug hoặc `Ctrl + F5` để chạy không debug
   - Trình duyệt sẽ mở tự động với địa chỉ `http://localhost:port`

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

## 🙏 Acknowledgments

- Bootstrap team for the amazing CSS framework
- Font Awesome for icons
- ASP.NET MVC community
- Entity Framework documentation

**Made with ❤️ for learning purposes**
