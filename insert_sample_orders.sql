-- Script thêm đơn hàng mẫu để test Order Management
USE QLStoreTrangMieng;

-- Đảm bảo có ít nhất 2 users (admin và customer)
IF NOT EXISTS (SELECT 1 FROM [User] WHERE Email = 'admin@trangmieng.com')
BEGIN
    INSERT INTO [User] (UserName, Email, Password, Role, Phone, Address, CreatedDate, IsActive) 
    VALUES (N'Admin', N'admin@trangmieng.com', N'admin123', N'Admin', N'0123456789', N'Hà Nội', GETDATE(), 1);
END

IF NOT EXISTS (SELECT 1 FROM [User] WHERE Email = 'customer1@gmail.com')
BEGIN
    INSERT INTO [User] (UserName, Email, Password, Role, Phone, Address, CreatedDate, IsActive) 
    VALUES (N'Nguyễn Văn A', N'customer1@gmail.com', N'123456', N'Customer', N'0987654321', N'123 Nguyễn Huệ, Q1, TP.HCM', GETDATE(), 1);
END

IF NOT EXISTS (SELECT 1 FROM [User] WHERE Email = 'customer2@gmail.com')
BEGIN
    INSERT INTO [User] (UserName, Email, Password, Role, Phone, Address, CreatedDate, IsActive) 
    VALUES (N'Trần Thị B', N'customer2@gmail.com', N'123456', N'Customer', N'0909123456', N'456 Lê Lợi, Q3, TP.HCM', GETDATE(), 1);
END

-- Lấy UserId của các customers
DECLARE @Customer1Id INT = (SELECT UserId FROM [User] WHERE Email = 'customer1@gmail.com');
DECLARE @Customer2Id INT = (SELECT UserId FROM [User] WHERE Email = 'customer2@gmail.com');

-- Thêm đơn hàng mẫu
-- Đơn hàng 1: Pending - mới đặt
INSERT INTO [Order] (OrderDate, ShippingAddress, PaymentMethod, Status, UserId) 
VALUES (GETDATE(), N'123 Nguyễn Huệ, Q1, TP.HCM', N'COD', N'Pending', @Customer1Id);

DECLARE @Order1Id INT = SCOPE_IDENTITY();

-- Chi tiết đơn hàng 1 (2 sản phẩm)
INSERT INTO Order_Product (OrderId, ProductId, Quantity, Price) VALUES
(@Order1Id, 1, 2, 45000),  -- Tiramisu Cà Phê x2
(@Order1Id, 11, 1, 35000); -- Trà Sữa Trân Châu x1

-- Đơn hàng 2: Confirmed - đã xác nhận
INSERT INTO [Order] (OrderDate, ShippingAddress, PaymentMethod, Status, UserId) 
VALUES (DATEADD(HOUR, -2, GETDATE()), N'456 Lê Lợi, Q3, TP.HCM', N'MoMo', N'Confirmed', @Customer2Id);

DECLARE @Order2Id INT = SCOPE_IDENTITY();

-- Chi tiết đơn hàng 2 (3 sản phẩm)
INSERT INTO Order_Product (OrderId, ProductId, Quantity, Price) VALUES
(@Order2Id, 5, 1, 55000),  -- Cheesecake Dâu x1
(@Order2Id, 14, 2, 25000), -- Cà Phê Sữa Đá x2
(@Order2Id, 7, 1, 30000);  -- Pudding Xoài x1

-- Đơn hàng 3: Processing - đang chuẩn bị
INSERT INTO [Order] (OrderDate, ShippingAddress, PaymentMethod, Status, UserId) 
VALUES (DATEADD(HOUR, -4, GETDATE()), N'789 Võ Văn Tần, Q3, TP.HCM', N'Bank Transfer', N'Processing', @Customer1Id);

DECLARE @Order3Id INT = SCOPE_IDENTITY();

-- Chi tiết đơn hàng 3 (1 sản phẩm số lượng lớn)
INSERT INTO Order_Product (OrderId, ProductId, Quantity, Price) VALUES
(@Order3Id, 11, 5, 35000), -- Trà Sữa Trân Châu x5
(@Order3Id, 1, 3, 45000);  -- Tiramisu Cà Phê x3

-- Đơn hàng 4: Shipped - đã giao shipper
INSERT INTO [Order] (OrderDate, ShippingAddress, PaymentMethod, Status, UserId) 
VALUES (DATEADD(DAY, -1, GETDATE()), N'321 Nguyễn Thị Minh Khai, Q1, TP.HCM', N'Credit Card', N'Shipped', @Customer2Id);

DECLARE @Order4Id INT = SCOPE_IDENTITY();

-- Chi tiết đơn hàng 4
INSERT INTO Order_Product (OrderId, ProductId, Quantity, Price) VALUES
(@Order4Id, 3, 2, 48000),  -- Tiramisu Matcha x2
(@Order4Id, 15, 1, 38000), -- Cappuccino x1
(@Order4Id, 17, 1, 40000); -- Sinh Tố Bơ x1

-- Đơn hàng 5: Delivered - đã giao thành công
INSERT INTO [Order] (OrderDate, ShippingAddress, PaymentMethod, Status, UserId) 
VALUES (DATEADD(DAY, -3, GETDATE()), N'654 Điện Biên Phủ, Q10, TP.HCM', N'COD', N'Delivered', @Customer1Id);

DECLARE @Order5Id INT = SCOPE_IDENTITY();

-- Chi tiết đơn hàng 5
INSERT INTO Order_Product (OrderId, ProductId, Quantity, Price) VALUES
(@Order5Id, 6, 1, 58000),  -- Cheesecake Chocolate x1
(@Order5Id, 19, 2, 30000); -- Nước Ép Cam x2

-- Đơn hàng 6: Cancelled - đã hủy
INSERT INTO [Order] (OrderDate, ShippingAddress, PaymentMethod, Status, UserId) 
VALUES (DATEADD(DAY, -2, GETDATE()), N'987 Cách Mạng Tháng 8, Q10, TP.HCM', N'MoMo', N'Cancelled', @Customer2Id);

DECLARE @Order6Id INT = SCOPE_IDENTITY();

-- Chi tiết đơn hàng 6 (đã hủy)
INSERT INTO Order_Product (OrderId, ProductId, Quantity, Price) VALUES
(@Order6Id, 9, 2, 35000),  -- Tart Trái Cây x2
(@Order6Id, 12, 1, 42000); -- Trà Sữa Matcha x1

-- Hiển thị thông báo
PRINT N'Đã thêm thành công 6 đơn hàng mẫu:';
PRINT N'- 1 đơn Pending (chờ xác nhận)';
PRINT N'- 1 đơn Confirmed (đã xác nhận)';
PRINT N'- 1 đơn Processing (đang chuẩn bị)';
PRINT N'- 1 đơn Shipped (đã giao shipper)';
PRINT N'- 1 đơn Delivered (đã giao thành công)';
PRINT N'- 1 đơn Cancelled (đã hủy)';
PRINT N'';
PRINT N'Có thể test Order Management ngay bây giờ!';

-- Hiển thị tổng quan đơn hàng
SELECT 
    Status,
    COUNT(*) as SoLuong,
    FORMAT(SUM(op.Quantity * op.Price), 'N0') + N' VNĐ' as TongGiaTri
FROM [Order] o
INNER JOIN Order_Product op ON o.OrderId = op.OrderId
GROUP BY Status
ORDER BY 
    CASE Status 
        WHEN 'Pending' THEN 1
        WHEN 'Confirmed' THEN 2  
        WHEN 'Processing' THEN 3
        WHEN 'Shipped' THEN 4
        WHEN 'Delivered' THEN 5
        WHEN 'Cancelled' THEN 6
    END; 