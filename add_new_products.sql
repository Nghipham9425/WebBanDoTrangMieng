-- Script thêm các sản phẩm mới vào database
-- Sử dụng database QLStoreTrangMieng
USE QLStoreTrangMieng;

-- Thêm các sản phẩm đồ uống mới (CategoryID = 2)

-- 1. Frozen Slush Tea - Frosted Peach
INSERT INTO Products (Name, Description, Price, CategoryId, ImageUrl, StockQuantity, CreatedDate) VALUES
(N'Frozen Slush Tea - Frosted Peach', N'Trà đá xay với hương đào tươi mát, kết cấu mịn như tuyết và vị ngọt thanh của đào. Thức uống hoàn hảo cho mùa hè.', 55000, 2, '/Content/Images/Products/FROZEN-SLUSH-TEA-FROSTED-PEACH-SLUSH-TEA.png', 50, GETDATE());

-- 2. Frozen Slush Tea - Grapefruit
INSERT INTO Products (Name, Description, Price, CategoryId, ImageUrl, StockQuantity, CreatedDate) VALUES
(N'Frozen Slush Tea - Grapefruit', N'Trà đá xay với hương bưởi tươi, vị chua ngọt đặc trưng của bưởi kết hợp với trà thơm mát, giải nhiệt tuyệt vời.', 55000, 2, '/Content/Images/Products/FROZEN-SLUSH-TEA-GRAPEFRUIT-FROZEN-SLUSH-TEA.png', 50, GETDATE());

-- 3. Frozen Slush Tea - Longan
INSERT INTO Products (Name, Description, Price, CategoryId, ImageUrl, StockQuantity, CreatedDate) VALUES
(N'Frozen Slush Tea - Longan', N'Trà đá xay với hương nhãn ngọt ngào, vị ngọt tự nhiên của nhãn hòa quyện với trà tạo nên thức uống độc đáo.', 55000, 2, '/Content/Images/Products/FROZEN-SLUSH-TEA-LONGAN-FROZEN-SLUSH-TEA.png', 50, GETDATE());

-- 4. Frozen Slush Tea - Strawberry Lemonade Crush
INSERT INTO Products (Name, Description, Price, CategoryId, ImageUrl, StockQuantity, CreatedDate) VALUES
(N'Frozen Slush Tea - Strawberry Lemonade Crush', N'Trà đá xay với dâu tây và chanh tươi, sự kết hợp hoàn hảo giữa vị ngọt của dâu và vị chua của chanh.', 58000, 2, '/Content/Images/Products/FROZEN-SLUSH-TEA-STRAWBERRY-LEMONADE-CRUSH.png', 50, GETDATE());

-- 5. Frozen Slush Tea - Tangerine
INSERT INTO Products (Name, Description, Price, CategoryId, ImageUrl, StockQuantity, CreatedDate) VALUES
(N'Frozen Slush Tea - Tangerine', N'Trà đá xay với hương cam quýt tươi mát, vị ngọt chua của cam quýt mang lại cảm giác sảng khoái.', 55000, 2, '/Content/Images/Products/FROZEN-SLUSH-TEA-TANGERINE-FROZEN-SLUSH-TEA.png', 50, GETDATE());

-- 6. Juice - Red Remedy
INSERT INTO Products (Name, Description, Price, CategoryId, ImageUrl, StockQuantity, CreatedDate) VALUES
(N'Juice - Red Remedy', N'Nước ép đặc biệt với các loại trái cây đỏ tươi, giàu vitamin và chất chống oxi hóa, tốt cho sức khỏe.', 48000, 2, '/Content/Images/Products/JUICE-RED-REMEDY.png', 50, GETDATE());

-- Hiển thị thông báo hoàn thành
PRINT N'Đã thêm thành công 6 sản phẩm đồ uống mới!';
PRINT N'- 5 loại Frozen Slush Tea';
PRINT N'- 1 loại Juice'; 