-- ==========================================
-- THÊM TẤT CẢ SẢN PHẨM DỰA TRÊN HÌNH ẢNH CÓ SẴN
-- ==========================================

USE QLStoreTrangMieng;

-- Xóa dữ liệu cũ và reset identity
DELETE FROM Order_Product;
DELETE FROM Product_Promotion;
DELETE FROM [Order];
DELETE FROM Review;
DELETE FROM Product;
DBCC CHECKIDENT ('Product', RESEED, 0);

-- ==========================================
-- BÁNH TRÁNG MIỆNG
-- ==========================================

-- Tiramisu
INSERT INTO Product (Name, Description, Price, CategoryId, StockQuantity, ImageUrl) VALUES 
(N'Tiramisu Cà Phê Original', N'Tiramisu truyền thống với hương vị cà phê đậm đà và mascarpone mềm mịn', 65000, 2, 20, 
 N'/Content/Images/Products/tiramisu_1.jpg,/Content/Images/Products/tiramisu_2.jpg,/Content/Images/Products/OriginalTiramisu.png'),

(N'Matchamisu - Tiramisu Matcha', N'Tiramisu với hương vị trà xanh Nhật Bản độc đáo', 68000, 2, 15, 
 N'/Content/Images/Products/matchamisu_1.jpg,/Content/Images/Products/matchamisu_2.jpg,/Content/Images/Products/CROCHI_matcha.png'),

(N'Tiramisu Dâu Tây', N'Tiramisu ngọt ngào với dâu tây tươi', 70000, 2, 18, 
 N'/Content/Images/Products/strawBerrytiramisu.png,/Content/Images/Products/CROCHI_Strawberry.png,/Content/Images/Products/merryBerry_1.jpg'),

(N'Tiramisu Dưa Lưới', N'Tiramisu với hương vị dưa lưới tươi mát', 72000, 2, 12, 
 N'/Content/Images/Products/MelonTiramisu.png'),

-- Mousse Cakes
(N'Mousse Blueberry', N'Bánh mousse việt quất với 4 lớp hương vị khác nhau', 75000, 4, 25, 
 N'/Content/Images/Products/MousseBlueberry_1.jpg,/Content/Images/Products/MousseBlueberry_3.jpg,/Content/Images/Products/MousseBlueberry_4.jpg,/Content/Images/Products/MousseBlueberry_w.jpg'),

(N'Mousse Xoài', N'Bánh mousse xoài cát Hòa Lộc thơm ngon', 68000, 4, 20, 
 N'/Content/Images/Products/MousseXoai_1.jpg,/Content/Images/Products/MousseXoai_2.jpg,/Content/Images/Products/crepeOvocado.png'),

(N'Mousse Vải', N'Bánh mousse vải tươi mát lạnh', 70000, 4, 18, 
 N'/Content/Images/Products/mousseVai_1.jpg,/Content/Images/Products/mousseVai_2.jpg,/Content/Images/Products/mousseVai_3.jpg'),

-- Cheesecake & Chocolate
(N'Basque Cheesecake', N'Bánh phô mai Basque nướng truyền thống', 85000, 3, 15, 
 N'/Content/Images/Products/BasqueCheesecake.png'),

(N'Chocolate Cake', N'Bánh chocolate đậm đà với ganache', 78000, 3, 20, 
 N'/Content/Images/Products/choco_cake_1.jpg,/Content/Images/Products/CROCHI_choco.png,/Content/Images/Products/SaltedChocolate.png'),

(N'Salted Caramel Chocolate', N'Chocolate với caramel muối biển', 82000, 3, 12, 
 N'/Content/Images/Products/SaltedMarbleCoffee.png,/Content/Images/Products/saltedCaCaoAvocado.png'),

-- Flan & Pudding
(N'Bánh Flan Caramel', N'Bánh flan truyền thống với caramel đậm đà', 35000, 1, 30, 
 N'/Content/Images/Products/flan_caramel.png,/Content/Images/Products/ButterScotchBrulee.png'),

(N'Misu Thiệc - Flan Đặc Biệt', N'Flan cao cấp với hương vị độc đáo', 45000, 1, 25, 
 N'/Content/Images/Products/misuThiec_1.jpg,/Content/Images/Products/misuThiec_2.jpg,/Content/Images/Products/misuThiec_3.jpg'),

(N'Merry Berry Pudding', N'Pudding trái cây nhiều màu sắc', 42000, 4, 20, 
 N'/Content/Images/Products/merryBerry_1.jpg,/Content/Images/Products/merryBerry_2.jpg,/Content/Images/Products/merryBerry_3.jpg'),

-- Crepes & Pancakes
(N'Matcha Crepe', N'Bánh crepe matcha mềm mịn', 58000, 5, 18, 
 N'/Content/Images/Products/matchaCrepe.png,/Content/Images/Products/CookieMatcha.png'),

(N'Avocado Crepe', N'Crepe bơ thơm ngon bổ dưỡng', 55000, 5, 15, 
 N'/Content/Images/Products/crepeOvocado.png'),

(N'French Toast Brulée', N'Bánh mì nướng kiểu Pháp với lớp caramel', 65000, 5, 12, 
 N'/Content/Images/Products/SWEET-BRULEED-CITRUS-FRENCH-TOAST-scaled.png,/Content/Images/Products/CreamBruleeBriocheFrenchToasts.png'),

(N'Dalgona French Toast', N'French toast với kem Dalgona', 68000, 5, 10, 
 N'/Content/Images/Products/SWEET-DALGONA-VELVET-FRENCH-TOAST-scaled.png'),

(N'Buttermilk Pancake Stack', N'Bánh pancake xếp tầng với bơ sữa', 72000, 5, 15, 
 N'/Content/Images/Products/ButtermilkPancakeStack.png'),

(N'Hong Kong Egg Waffles', N'Bánh waffle trứng Hong Kong', 48000, 5, 20, 
 N'/Content/Images/Products/HongkongEggWaffles.png'),

-- Pastries & Croissants
(N'Chocolate Croissant', N'Bánh sừng bò chocolate Pháp', 35000, 5, 25, 
 N'/Content/Images/Products/choco_croissant.jpg'),

(N'Matcha Croissant', N'Bánh sừng bò matcha', 38000, 5, 20, 
 N'/Content/Images/Products/matcha_croissant.jpg'),

(N'Cloudy Lava Nutella', N'Bánh mây với nhân Nutella chảy', 52000, 5, 18, 
 N'/Content/Images/Products/Cloudy-Lava-Nutella_220k.png'),

-- ==========================================
-- NƯỚC UỐNG - TRÀ SỮA & MATCHA
-- ==========================================

(N'Matcha Latte Signature', N'Trà sữa matcha đặc trưng của quán', 45000, 6, 30, 
 N'/Content/Images/Products/MatchaLatteS.png,/Content/Images/Products/MATCHA-COCO-EARL-GREY-MATCHA-LATTE.png,/Content/Images/Products/StrawMatchaLate.png'),

(N'Matcha Cloud Smoothie', N'Smoothie matcha với foam mây', 48000, 6, 25, 
 N'/Content/Images/Products/MATCHA-MATCHA-CLOUD-SMOOTHIE-scaled.png'),

(N'Matcha Vanilla Coconut', N'Matcha pha với vanilla và dừa', 46000, 6, 20, 
 N'/Content/Images/Products/MATCHA-VANILLA-COCO-MATCHA-COLD-FOAM.png'),

(N'Trà Sữa Trân Châu Đen', N'Trà sữa trân châu đen truyền thống', 38000, 6, 40, 
 N'/Content/Images/Products/EarlGreyWithBoba.png'),

(N'Gentle Hojicha', N'Trà hojicha nhẹ nhàng', 42000, 6, 25, 
 N'/Content/Images/Products/gentleHojicha.png'),

(N'Rich Hojicha', N'Trà hojicha đậm đà', 44000, 6, 20, 
 N'/Content/Images/Products/richHojicha.png'),

(N'Matcha Oat Side', N'Matcha với sữa yến mạch', 47000, 6, 18, 
 N'/Content/Images/Products/matchaOatSide.png'),

-- ==========================================
-- CÀ PHÊ
-- ==========================================

(N'Espresso', N'Cà phê espresso nguyên chất', 25000, 7, 50, 
 N'/Content/Images/Products/Espresso.png,/Content/Images/Products/EspressoMatchaLatte.png'),

(N'Americano', N'Cà phê Americano đen', 28000, 7, 45, 
 N'/Content/Images/Products/Americano.png'),

(N'Cappuccino', N'Cappuccino Ý với foam sữa dày', 35000, 7, 35, 
 N'/Content/Images/Products/Capuccino.png'),

(N'Latte', N'Café Latte mềm mại', 38000, 7, 30, 
 N'/Content/Images/Products/LatteFlatWhite.png,/Content/Images/Products/TiramissuLatte.png'),

(N'Mocha', N'Cà phê mocha với chocolate', 42000, 7, 25, 
 N'/Content/Images/Products/Mocha.png,/Content/Images/Products/SIGNATURE-MOCHA-BROWN-SUGAR-LATTE.png'),

(N'Caramel Macchiato', N'Macchiato với caramel thơm ngon', 45000, 7, 20, 
 N'/Content/Images/Products/CaremelMachiato.png,/Content/Images/Products/TiramissuMatchaCaremel.png'),

(N'Vietnamese Milk Coffee', N'Cà phê sữa đá Việt Nam', 32000, 7, 40, 
 N'/Content/Images/Products/VietnameseMilkCoffee.png'),

(N'S''mores Latte', N'Latte với hương vị bánh quy S''mores', 48000, 7, 15, 
 N'/Content/Images/Products/Soko''MoresLatte.png'),

-- ==========================================
-- NƯỚC UỐNG KHÁC
-- ==========================================

(N'Iced Chocolate', N'Chocolate đá mát lạnh', 35000, 8, 30, 
 N'/Content/Images/Products/IcedChocolate.png'),

(N'Vegan Granola Smoothie', N'Smoothie granola thuần chay', 45000, 8, 20, 
 N'/Content/Images/Products/VeganGranola.png'),

(N'Oolong Đào Hồng', N'Trà oolong với đào hồng', 40000, 9, 25, 
 N'/Content/Images/Products/OlongDaoHong.png'),

(N'Nước Lụa Đào', N'Trà đào mềm mại như lụa', 38000, 9, 22, 
 N'/Content/Images/Products/NuocLuaDao.png');

PRINT N'Đã thêm tất cả sản phẩm dựa trên hình ảnh có sẵn!';
PRINT N'Tổng cộng: ' + CAST(@@ROWCOUNT AS NVARCHAR(10)) + N' sản phẩm';

-- Kiểm tra kết quả
SELECT COUNT(*) as TotalProducts FROM Product;
SELECT CategoryId, COUNT(*) as ProductCount 
FROM Product 
GROUP BY CategoryId 
ORDER BY CategoryId; 