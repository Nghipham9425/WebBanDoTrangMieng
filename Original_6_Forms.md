# 📋 6 BIỂU MẪU Gốc - CHO THIẾT KẾ GIAO DIỆN

## BM01: Biểu mẫu sản phẩm

| Trường                           | Giá trị |
| -------------------------------- | ------- |
| Mã sản phẩm (ProductId)          | .....   |
| Tên sản phẩm (Name)              | .....   |
| Danh mục (CategoryId)            | .....   |
| Giá (Price)                      | .....   |
| Mô tả (Description)              | .....   |
| Thành phần                       | .....   |
| Trọng lượng/Kích thước           | .....   |
| Hình ảnh (ImageUrl)              | .....   |
| Số lượng tồn kho (StockQuantity) | .....   |
| Trạng thái (Còn hàng/Hết hàng)   | .....   |

---

## BM02: Biểu mẫu giỏ hàng

**🛒 Giỏ hàng của bạn**

| Sản phẩm                      | Đơn giá | Số lượng  | Thao tác |
| ----------------------------- | ------- | --------- | -------- |
| [Hình ảnh] **Bánh Mousse...** | 75.000đ | [-] 1 [+] | [🗑️]     |
| [Hình ảnh] **Cupcake Mat...** | 40.000đ | [-] 2 [+] | [🗑️]     |
| .....                         | .....   | .....     | .....    |

**← Tiếp tục mua sắm**

**📋 Tóm tắt đơn hàng:**

- **Mã giảm giá:** [ Nhập mã giảm giá ] [Áp dụng]
- **Tiền hàng:** .....đ
- **Phí vận chuyển:** 20.000đ
- **Tạm tính:** .....đ
- **Tổng cộng:** .....đ

**[THANH TOÁN]**

---

## BM03: Biểu mẫu thanh toán

**💳 Thanh toán**

**🚚 Thông tin giao hàng:**

| Trường             | Giá trị |
| ------------------ | ------- |
| Họ và tên          | .....   |
| Số điện thoại      | .....   |
| Email              | .....   |
| Địa chỉ            | .....   |
| Tỉnh/Thành phố     | .....   |
| Quận/Huyện         | .....   |
| Xã/Phường          | .....   |
| Ghi chú (tùy chọn) | .....   |

**💳 Phương thức thanh toán:**

- ☐ **Thanh toán khi nhận hàng (COD)**  
  _Thanh toán bằng tiền mặt khi nhận hàng_

- ☑️ **Chuyển khoản ngân hàng**  
  _Chuyển khoản qua ngân hàng hoặc ví điện tử_

- ☐ **Ví điện tử MoMo**  
  _Thanh toán qua ví điện tử MoMo_

**📋 Tóm tắt đơn hàng:**

| Sản phẩm           | Số lượng | Giá     |
| ------------------ | -------- | ------- |
| Bánh Mousse Matcha | 1        | 75.000đ |
| Cupcake Matcha     | 2        | 80.000đ |

- **Tạm tính:** 155.000đ
- **Phí vận chuyển:** 20.000đ
- **Tổng cộng:** **175.000đ**

**[🛒 ĐẶT HÀNG]**  
_🔒 Thanh toán an toàn và bảo mật_

---

## BM04: Biểu mẫu đăng ký tài khoản

| Trường                | Giá trị |
| --------------------- | ------- |
| Họ và tên (UserName)  | .....   |
| Email                 | .....   |
| Số điện thoại (Phone) | .....   |
| Mật khẩu (Password)   | .....   |
| Xác nhận mật khẩu     | .....   |
| Địa chỉ (Address)     | .....   |

---

## BM05: Biểu mẫu khuyến mãi

| Trường                           | Giá trị |
| -------------------------------- | ------- |
| Mã khuyến mãi (Code)             | .....   |
| Tên chương trình (Description)   | .....   |
| Thời gian bắt đầu (StartDate)    | .....   |
| Thời gian kết thúc (EndDate)     | .....   |
| Mức giảm giá % (DiscountPercent) | .....   |
| Sản phẩm áp dụng                 | .....   |
| Điều kiện áp dụng                | .....   |
| Số lượng mã giảm giá             | .....   |
| Trạng thái (IsActive)            | .....   |

---

## BM06: Biểu mẫu đánh giá sản phẩm

| Trường                           | Giá trị                          |
| -------------------------------- | -------------------------------- |
| Mã đơn hàng (OrderId)            | DH001                            |
| Mã sản phẩm (ProductId)          | SP001                            |
| Tên khách hàng (UserId)          | Nguyễn Văn A                     |
| Điểm đánh giá (1-5 sao) (Rating) | ⭐⭐⭐⭐⭐                       |
| Nội dung đánh giá (Comment)      | Bánh rất ngon, đóng gói cẩn thận |
| Ngày đánh giá (CreatedDate)      | 15/12/2024                       |

---

## BM07: Biểu mẫu thống kê doanh thu

| Tháng    | Tên món | Số lượng bán | Đơn giá (VNĐ) | Doanh thu (VNĐ) |
| -------- | ------- | ------------ | ------------- | --------------- |
| 1        | .....   | .....        | .....         | .....           |
| 2        | .....   | .....        | .....         | .....           |
| 3        | .....   | .....        | .....         | .....           |
| 4        | .....   | .....        | .....         | .....           |
| 5        | .....   | .....        | .....         | .....           |
| 6        | .....   | .....        | .....         | .....           |
| 7        | .....   | .....        | .....         | .....           |
| 8        | .....   | .....        | .....         | .....           |
| 9        | .....   | .....        | .....         | .....           |
| 10       | .....   | .....        | .....         | .....           |
| 11       | .....   | .....        | .....         | .....           |
| 12       | .....   | .....        | .....         | .....           |
| **Tổng** |         |              |               | **......**      |

---

## BM08: Biểu mẫu hóa đơn

**HÓA ĐƠN BÁN HÀNG**

**Công ty:** Web Bán Đồ Tráng Miệng  
**Địa chỉ:** .....  
**Điện thoại:** .....  
**Email:** .....

---

**Số hóa đơn:** .....  
**Ngày lập:** .....  
**Nhân viên:** .....

**THÔNG TIN KHÁCH HÀNG:**

- **Họ tên:** .....
- **Điện thoại:** .....
- **Địa chỉ:** .....
- **Email:** .....

---

**CHI TIẾT SẢN PHẨM:**

| STT | Tên sản phẩm | ĐVT   | Số lượng | Đơn giá (VNĐ) | Thành tiền (VNĐ) |
| --- | ------------ | ----- | -------- | ------------- | ---------------- |
| 1   | .....        | ..... | .....    | .....         | .....            |
| 2   | .....        | ..... | .....    | .....         | .....            |
| 3   | .....        | ..... | .....    | .....         | .....            |
| 4   | .....        | ..... | .....    | .....         | .....            |
| 5   | .....        | ..... | .....    | .....         | .....            |

---

**TỔNG KẾT:**

- **Tổng tiền hàng:** ..... VNĐ
- **Phí vận chuyển:** ..... VNĐ
- **Giảm giá:** ..... VNĐ
- **Thuế VAT (10%):** ..... VNĐ
- **TỔNG THANH TOÁN:** ..... VNĐ

**Phương thức thanh toán:** .....  
**Trạng thái:** .....  
**Ghi chú:** .....

---

**Chữ ký khách hàng** **Chữ ký nhân viên**

---

## 📊 **MAPPING VỚI DATABASE:**

### **BM01 → Product Table:**

- ProductId, Name, CategoryId, Price, Description, ImageUrl, StockQuantity

### **BM02 → Cart + Cart_Product Tables:**

- Cart: CartId, UserId, CreatedDate, UpdatedDate
- Cart_Product: CartId, ProductId, Quantity, AddedDate
- Product: Name, Price, ImageUrl

### **BM03 → Order + Order_Product Tables:**

- Order: OrderId, OrderDate, UserId, ShippingAddress, PaymentMethod, Status
- Order_Product: ProductId, Quantity, UnitPrice
- User: UserName, Phone, Email, Address

### **BM04 → User Table:**

- UserName, Email, Phone, Password, Address

### **BM05 → Promotion Table:**

- Code, Description, StartDate, EndDate, DiscountPercent, IsActive

### **BM06 → Review Table:**

- OrderId, ProductId, UserId, Rating, Comment, CreatedDate

### **BM07 → Báo cáo (tính toán từ Order_Product):**

- Tính từ SUM(Quantity), AVG(UnitPrice), SUM(Quantity × UnitPrice)

### **BM08 → Order + Order_Product + User Tables:**

- Order: OrderId, OrderDate, UserId, ShippingAddress, PaymentMethod, Status
- Order_Product: ProductId, Quantity, UnitPrice
- User: UserName, Phone, Email, Address
- Product: Name (tên sản phẩm)
