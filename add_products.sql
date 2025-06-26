USE QLStoreTrangMieng;

-- Insert Products - TRÁNG MIỆNG với CategoryId đúng
INSERT INTO Product (Name, Description, Price, CategoryId, StockQuantity, ImageUrl) VALUES 
(N'Bánh Flan Caramen', N'Bánh flan truyền thống với caramen', 25000.00, 11, 50, N'/Content/Images/Products/flan-caramen.jpg'),
(N'Bánh Flan Dâu', N'Bánh flan với topping dâu tây', 28000.00, 11, 40, N'/Content/Images/Products/flan-dau.jpg'),
(N'Tiramisu Cà Phê', N'Tiramisu với hương vị cà phê đậm đà', 45000.00, 12, 30, N'/Content/Images/Products/tiramisu-cafe.jpg'),
(N'Tiramisu Matcha', N'Tiramisu vị trà xanh Nhật Bản', 48000.00, 12, 25, N'/Content/Images/Products/tiramisu-matcha.jpg'),
(N'Cheesecake Dâu', N'Bánh phô mai với topping dâu tây tươi', 55000.00, 13, 25, N'/Content/Images/Products/cheesecake-dau.jpg'),
(N'Cheesecake Chocolate', N'Bánh phô mai vị chocolate đậm đà', 58000.00, 13, 20, N'/Content/Images/Products/cheesecake-choco.jpg'),
(N'Pudding Xoài', N'Pudding xoài thơm ngon mát lạnh', 30000.00, 14, 40, N'/Content/Images/Products/pudding-xoai.jpg'),
(N'Pudding Dâu', N'Pudding dâu tây ngọt thanh', 32000.00, 14, 35, N'/Content/Images/Products/pudding-dau.jpg'),
(N'Tart Trái Cây', N'Tart mini với nhiều loại trái cây tươi', 35000.00, 15, 35, N'/Content/Images/Products/tart-traicay.jpg'),
(N'Tart Chocolate', N'Tart chocolate đắng ngọt', 38000.00, 15, 30, N'/Content/Images/Products/tart-choco.jpg'),

-- Insert Products - NƯỚC UỐNG với CategoryId đúng
(N'Trà Sữa Trân Châu', N'Trà sữa trân châu đen classic', 35000.00, 16, 60, N'/Content/Images/Products/trasua-tranchau.jpg'),
(N'Trà Sữa Matcha', N'Trà sữa matcha Nhật Bản', 42000.00, 16, 45, N'/Content/Images/Products/trasua-matcha.jpg'),
(N'Trà Sữa Taro', N'Trà sữa khoai môn tím', 38000.00, 16, 50, N'/Content/Images/Products/trasua-taro.jpg'),
(N'Cà Phê Sữa Đá', N'Cà phê sữa đá truyền thống Việt Nam', 25000.00, 17, 70, N'/Content/Images/Products/cafe-suada.jpg'),
(N'Cappuccino', N'Cappuccino Ý đậm đà', 38000.00, 17, 40, N'/Content/Images/Products/cappuccino.jpg'),
(N'Latte', N'Café Latte mềm mại', 42000.00, 17, 35, N'/Content/Images/Products/latte.jpg'),
(N'Sinh Tố Bơ', N'Sinh tố bơ tươi mát lạnh', 40000.00, 18, 30, N'/Content/Images/Products/sinhto-bo.jpg'),
(N'Sinh Tố Xoài', N'Sinh tố xoài cát Hòa Lộc', 42000.00, 18, 25, N'/Content/Images/Products/sinhto-xoai.jpg'),
(N'Nước Ép Cam', N'Nước ép cam tươi 100% nguyên chất', 30000.00, 19, 50, N'/Content/Images/Products/nuocep-cam.jpg'),
(N'Nước Ép Dưa Hấu', N'Nước ép dưa hấu tươi mát', 28000.00, 19, 45, N'/Content/Images/Products/nuocep-duahau.jpg'),
(N'Soda Chanh', N'Soda chanh tươi mát lạnh', 28000.00, 20, 60, N'/Content/Images/Products/soda-chanh.jpg'),
(N'Soda Dâu', N'Soda dâu tây ngọt thanh', 32000.00, 20, 40, N'/Content/Images/Products/soda-dau.jpg');

PRINT N'Đã thêm 22 sản phẩm với Unicode và CategoryId đúng!'; 