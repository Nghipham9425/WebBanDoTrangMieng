USE QLStoreTrangMieng;

-- Delete old data
DELETE FROM Product_Promotion;
DELETE FROM Product;
DELETE FROM Category;
DELETE FROM PaymentMethod;
DELETE FROM Promotion;

-- Insert Categories với N prefix
INSERT INTO Category (Name, Description) VALUES 
(N'Bánh Flan', N'Các loại bánh flan truyền thống'),
(N'Tiramisu', N'Tiramisu Ý nguyên chất'), 
(N'Cheesecake', N'Bánh phô mai các loại'),
(N'Pudding', N'Pudding trái cây'),
(N'Bánh Tart', N'Bánh tart mini'),
(N'Trà Sữa', N'Trà sữa các loại'),
(N'Cà Phê', N'Cà phê và espresso'),
(N'Sinh Tố', N'Sinh tố trái cây tươi'),
(N'Nước Ép', N'Nước ép trái cây nguyên chất'),
(N'Soda', N'Nước có gas và mocktail');

-- Insert Payment Methods với N prefix
INSERT INTO PaymentMethod (MethodName, Description) VALUES 
(N'COD', N'Thanh toán khi nhận hàng'),
(N'Bank Transfer', N'Chuyển khoản ngân hàng'),
(N'MoMo', N'Ví điện tử MoMo'),
(N'Credit Card', N'Thẻ tín dụng'),
(N'Debit Card', N'Thẻ ghi nợ');

-- Insert Products - TRÁNG MIỆNG với N prefix
INSERT INTO Product (Name, Description, Price, CategoryId, StockQuantity, ImageUrl) VALUES 
(N'Bánh Flan Caramen', N'Bánh flan truyền thống với caramen', 25000.00, 1, 50, N'/Content/Images/Products/flan-caramen.jpg'),
(N'Bánh Flan Dâu', N'Bánh flan với topping dâu tây', 28000.00, 1, 40, N'/Content/Images/Products/flan-dau.jpg'),
(N'Tiramisu Cà Phê', N'Tiramisu với hương vị cà phê đậm đà', 45000.00, 2, 30, N'/Content/Images/Products/tiramisu-cafe.jpg'),
(N'Tiramisu Matcha', N'Tiramisu vị trà xanh Nhật Bản', 48000.00, 2, 25, N'/Content/Images/Products/tiramisu-matcha.jpg'),
(N'Cheesecake Dâu', N'Bánh phô mai với topping dâu tây tươi', 55000.00, 3, 25, N'/Content/Images/Products/cheesecake-dau.jpg'),
(N'Cheesecake Chocolate', N'Bánh phô mai vị chocolate đậm đà', 58000.00, 3, 20, N'/Content/Images/Products/cheesecake-choco.jpg'),
(N'Pudding Xoài', N'Pudding xoài thơm ngon mát lạnh', 30000.00, 4, 40, N'/Content/Images/Products/pudding-xoai.jpg'),
(N'Pudding Dâu', N'Pudding dâu tây ngọt thanh', 32000.00, 4, 35, N'/Content/Images/Products/pudding-dau.jpg'),
(N'Tart Trái Cây', N'Tart mini với nhiều loại trái cây tươi', 35000.00, 5, 35, N'/Content/Images/Products/tart-traicay.jpg'),
(N'Tart Chocolate', N'Tart chocolate đắng ngọt', 38000.00, 5, 30, N'/Content/Images/Products/tart-choco.jpg'),

-- Insert Products - NƯỚC UỐNG với N prefix
(N'Trà Sữa Trân Châu', N'Trà sữa trân châu đen classic', 35000.00, 6, 60, N'/Content/Images/Products/trasua-tranchau.jpg'),
(N'Trà Sữa Matcha', N'Trà sữa matcha Nhật Bản', 42000.00, 6, 45, N'/Content/Images/Products/trasua-matcha.jpg'),
(N'Trà Sữa Taro', N'Trà sữa khoai môn tím', 38000.00, 6, 50, N'/Content/Images/Products/trasua-taro.jpg'),
(N'Cà Phê Sữa Đá', N'Cà phê sữa đá truyền thống Việt Nam', 25000.00, 7, 70, N'/Content/Images/Products/cafe-suada.jpg'),
(N'Cappuccino', N'Cappuccino Ý đậm đà', 38000.00, 7, 40, N'/Content/Images/Products/cappuccino.jpg'),
(N'Latte', N'Café Latte mềm mại', 42000.00, 7, 35, N'/Content/Images/Products/latte.jpg'),
(N'Sinh Tố Bơ', N'Sinh tố bơ tươi mát lạnh', 40000.00, 8, 30, N'/Content/Images/Products/sinhto-bo.jpg'),
(N'Sinh Tố Xoài', N'Sinh tố xoài cát Hòa Lộc', 42000.00, 8, 25, N'/Content/Images/Products/sinhto-xoai.jpg'),
(N'Nước Ép Cam', N'Nước ép cam tươi 100% nguyên chất', 30000.00, 9, 50, N'/Content/Images/Products/nuocep-cam.jpg'),
(N'Nước Ép Dưa Hấu', N'Nước ép dưa hấu tươi mát', 28000.00, 9, 45, N'/Content/Images/Products/nuocep-duahau.jpg'),
(N'Soda Chanh', N'Soda chanh tươi mát lạnh', 28000.00, 10, 60, N'/Content/Images/Products/soda-chanh.jpg'),
(N'Soda Dâu', N'Soda dâu tây ngọt thanh', 32000.00, 10, 40, N'/Content/Images/Products/soda-dau.jpg');

-- Insert Promotions với N prefix
INSERT INTO Promotion (Code, Description, DiscountPercent, StartDate, EndDate) VALUES 
(N'WELCOME10', N'Giảm 10% cho khách hàng mới', 10.00, GETDATE(), DATEADD(MONTH, 1, GETDATE())),
(N'COMBO15', N'Giảm 15% khi mua combo tráng miệng + nước uống', 15.00, GETDATE(), DATEADD(DAY, 15, GETDATE()));

-- Insert Sample Product-Promotion relationships
INSERT INTO Product_Promotion (ProductId, PromotionId) VALUES 
(1, 1), -- Bánh Flan Caramen có khuyến mãi WELCOME10
(3, 1), -- Tiramisu Cà Phê có khuyến mãi WELCOME10  
(5, 2), -- Cheesecake Dâu có khuyến mãi COMBO15
(11, 2); -- Trà Sữa Trân Châu có khuyến mãi COMBO15

PRINT N'Dữ liệu đã được fix với Unicode đúng!';
PRINT N'Tất cả chuỗi tiếng Việt giờ sẽ hiển thị chính xác!'; 