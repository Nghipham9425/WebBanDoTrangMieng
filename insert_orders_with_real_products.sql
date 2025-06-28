-- Script tạo đơn hàng mẫu với sản phẩm có hình ảnh thật
USE QLStoreTrangMieng;

-- Đảm bảo có 2 customers test
IF NOT EXISTS (SELECT 1 FROM [User] WHERE Email = 'customer1@gmail.com')
BEGIN
    INSERT INTO [User] (UserName, Email, Password, Role, Phone, Address, CreatedDate, IsActive) 
    VALUES (N'Nguyễn Văn A', N'customer1@gmail.com', N'123456', N'Customer', N'0987654321', N'789 Võ Văn Tần, Q3, TP.HCM', GETDATE(), 1);
END

IF NOT EXISTS (SELECT 1 FROM [User] WHERE Email = 'customer2@gmail.com')
BEGIN
    INSERT INTO [User] (UserName, Email, Password, Role, Phone, Address, CreatedDate, IsActive) 
    VALUES (N'Trần Thị B', N'customer2@gmail.com', N'123456', N'Customer', N'0901234567', N'456 Lê Lợi, Q1, TP.HCM', GETDATE(), 1);
END

-- Lấy UserId
DECLARE @Customer1Id INT = (SELECT UserId FROM [User] WHERE Email = 'customer1@gmail.com');
DECLARE @Customer2Id INT = (SELECT UserId FROM [User] WHERE Email = 'customer2@gmail.com');

-- Lấy các ProductId có hình ảnh thật (từ folder Images/Products/)
DECLARE @TiramisuId INT = (SELECT TOP 1 ProductId FROM Product WHERE Name LIKE N'%Tiramisu%' AND ImageUrl IS NOT NULL);
DECLARE @FlanId INT = (SELECT TOP 1 ProductId FROM Product WHERE Name LIKE N'%Flan%' AND ImageUrl IS NOT NULL);
DECLARE @MatchaId INT = (SELECT TOP 1 ProductId FROM Product WHERE Name LIKE N'%Matcha%' AND ImageUrl IS NOT NULL);
DECLARE @AmericanoId INT = (SELECT TOP 1 ProductId FROM Product WHERE Name LIKE N'%Americano%' AND ImageUrl IS NOT NULL);
DECLARE @CappuccinoId INT = (SELECT TOP 1 ProductId FROM Product WHERE Name LIKE N'%Cappuccino%' AND ImageUrl IS NOT NULL);

-- Nếu không tìm thấy, lấy 5 sản phẩm đầu tiên có hình
IF @TiramisuId IS NULL SET @TiramisuId = (SELECT TOP 1 ProductId FROM Product WHERE ImageUrl IS NOT NULL ORDER BY ProductId);
IF @FlanId IS NULL SET @FlanId = (SELECT ProductId FROM (SELECT ProductId, ROW_NUMBER() OVER (ORDER BY ProductId) as rn FROM Product WHERE ImageUrl IS NOT NULL) t WHERE rn = 2);
IF @MatchaId IS NULL SET @MatchaId = (SELECT ProductId FROM (SELECT ProductId, ROW_NUMBER() OVER (ORDER BY ProductId) as rn FROM Product WHERE ImageUrl IS NOT NULL) t WHERE rn = 3);
IF @AmericanoId IS NULL SET @AmericanoId = (SELECT ProductId FROM (SELECT ProductId, ROW_NUMBER() OVER (ORDER BY ProductId) as rn FROM Product WHERE ImageUrl IS NOT NULL) t WHERE rn = 4);
IF @CappuccinoId IS NULL SET @CappuccinoId = (SELECT ProductId FROM (SELECT ProductId, ROW_NUMBER() OVER (ORDER BY ProductId) as rn FROM Product WHERE ImageUrl IS NOT NULL) t WHERE rn = 5);

-- Xóa đơn hàng cũ nếu có
DELETE FROM Order_Product WHERE OrderId IN (SELECT OrderId FROM [Order] WHERE UserId IN (@Customer1Id, @Customer2Id));
DELETE FROM [Order] WHERE UserId IN (@Customer1Id, @Customer2Id);

-- Đơn hàng 1: Pending (chờ xác nhận)
INSERT INTO [Order] (OrderDate, ShippingAddress, PaymentMethod, Status, UserId) 
VALUES (GETDATE(), N'789 Võ Văn Tần, Q3, TP.HCM', N'Cash', N'Pending', @Customer1Id);

DECLARE @Order1Id INT = SCOPE_IDENTITY();
INSERT INTO Order_Product (OrderId, ProductId, Quantity, Price) VALUES
(@Order1Id, @TiramisuId, 2, 45000),
(@Order1Id, @FlanId, 1, 35000);

-- Đơn hàng 2: Confirmed (đã xác nhận)  
INSERT INTO [Order] (OrderDate, ShippingAddress, PaymentMethod, Status, UserId) 
VALUES (DATEADD(HOUR, -2, GETDATE()), N'456 Lê Lợi, Q1, TP.HCM', N'Bank Transfer', N'Confirmed', @Customer2Id);

DECLARE @Order2Id INT = SCOPE_IDENTITY();
INSERT INTO Order_Product (OrderId, ProductId, Quantity, Price) VALUES
(@Order2Id, @MatchaId, 1, 55000),
(@Order2Id, @AmericanoId, 2, 30000);

-- Đơn hàng 3: Processing (đang chuẩn bị)
INSERT INTO [Order] (OrderDate, ShippingAddress, PaymentMethod, Status, UserId) 
VALUES (DATEADD(HOUR, -4, GETDATE()), N'789 Võ Văn Tần, Q3, TP.HCM', N'Bank Transfer', N'Processing', @Customer1Id);

DECLARE @Order3Id INT = SCOPE_IDENTITY();
INSERT INTO Order_Product (OrderId, ProductId, Quantity, Price) VALUES
(@Order3Id, @TiramisuId, 3, 45000),
(@Order3Id, @FlanId, 5, 35000);

-- Đơn hàng 4: Shipped (đang giao hàng)
INSERT INTO [Order] (OrderDate, ShippingAddress, PaymentMethod, Status, UserId) 
VALUES (DATEADD(DAY, -1, GETDATE()), N'456 Lê Lợi, Q1, TP.HCM', N'Credit Card', N'Shipped', @Customer2Id);

DECLARE @Order4Id INT = SCOPE_IDENTITY();
INSERT INTO Order_Product (OrderId, ProductId, Quantity, Price) VALUES
(@Order4Id, @CappuccinoId, 2, 40000),
(@Order4Id, @MatchaId, 1, 55000);

-- Đơn hàng 5: Delivered (đã giao thành công)
INSERT INTO [Order] (OrderDate, ShippingAddress, PaymentMethod, Status, UserId) 
VALUES (DATEADD(DAY, -3, GETDATE()), N'789 Võ Văn Tần, Q3, TP.HCM', N'Cash', N'Delivered', @Customer1Id);

DECLARE @Order5Id INT = SCOPE_IDENTITY();
INSERT INTO Order_Product (OrderId, ProductId, Quantity, Price) VALUES
(@Order5Id, @AmericanoId, 1, 30000),
(@Order5Id, @TiramisuId, 1, 45000);

-- Đơn hàng 6: Cancelled (đã hủy)
INSERT INTO [Order] (OrderDate, ShippingAddress, PaymentMethod, Status, UserId) 
VALUES (DATEADD(DAY, -2, GETDATE()), N'456 Lê Lợi, Q1, TP.HCM', N'Bank Transfer', N'Cancelled', @Customer2Id);

DECLARE @Order6Id INT = SCOPE_IDENTITY();
INSERT INTO Order_Product (OrderId, ProductId, Quantity, Price) VALUES
(@Order6Id, @FlanId, 2, 35000),
(@Order6Id, @CappuccinoId, 1, 40000);

-- Hiển thị kết quả
PRINT N'✅ Đã tạo thành công 6 đơn hàng mẫu với sản phẩm có hình ảnh!';
PRINT N'';

-- Hiển thị tổng quan
SELECT 
    o.OrderId,
    o.Status as TrangThai,
    u.UserName as KhachHang,
    COUNT(op.ProductId) as SoSanPham,
    FORMAT(SUM(op.Quantity * op.Price), 'N0') + N' VNĐ' as TongTien,
    o.PaymentMethod as PhuongThucTT
FROM [Order] o
INNER JOIN [User] u ON o.UserId = u.UserId  
INNER JOIN Order_Product op ON o.OrderId = op.OrderId
WHERE u.Email IN ('customer1@gmail.com', 'customer2@gmail.com')
GROUP BY o.OrderId, o.Status, u.UserName, o.PaymentMethod
ORDER BY o.OrderId;

PRINT N'';
PRINT N'🎉 Có thể test Order Management ngay bây giờ!';
PRINT N'📱 Vào /Admin/Order để xem danh sách đơn hàng'; 