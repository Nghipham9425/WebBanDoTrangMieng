-- ==========================================
-- XÓA HẾT VÀ TẠO LẠI TẤT CẢ SẢN PHẨM
-- ==========================================

USE QLStoreTrangMieng;

-- Xóa dữ liệu cũ
DELETE FROM Order_Product;
DELETE FROM Product_Promotion;
DELETE FROM Review;
DELETE FROM [Order];
DELETE FROM Product;
DBCC CHECKIDENT ('Product', RESEED, 0);

-- Cập nhật danh mục thành 2 danh mục chính
UPDATE Category SET Name = N'Bánh Tráng Miệng', Description = N'Tất cả các loại bánh ngọt và tráng miệng' WHERE CategoryId = 1;
UPDATE Category SET Name = N'Nước Uống', Description = N'Tất cả các loại đồ uống' WHERE CategoryId = 2;

-- ==========================================
-- BÁNH TRÁNG MIỆNG (CategoryId = 1)
-- ==========================================

INSERT INTO Product (Name, Description, Price, CategoryId, StockQuantity, ImageUrl) VALUES 

-- Tiramisu Collection
(N'Tiramisu Cà Phê Original', 
 N'Tiramisu truyền thống Ý với lớp mascarpone mềm mịn, bánh ladyfinger thấm cà phê espresso đậm đà. Được rắc bột cacao nguyên chất, mang đến hương vị cổ điển khó quên.', 
 65000, 1, 25, 
 N'/Content/Images/Products/tiramisu_1.jpg,/Content/Images/Products/tiramisu_2.jpg,/Content/Images/Products/OriginalTiramisu.png'),

(N'Matchamisu - Tiramisu Matcha', 
 N'Phiên bản độc đáo của tiramisu với bột matcha Nhật Bản cao cấp. Vị đắng nhẹ của trà xanh hòa quyện cùng kem cheese mềm mịn, tạo nên hương vị tinh tế và sang trọng.', 
 72000, 1, 20, 
 N'/Content/Images/Products/matchamisu_1.jpg,/Content/Images/Products/matchamisu_2.jpg,/Content/Images/Products/CROCHI_matcha.png'),

(N'Tiramisu Dâu Tây', 
 N'Tiramisu ngọt ngào với dâu tây tươi Đà Lạt, kết hợp cùng kem mascarpone và bánh bông lan mềm. Vị chua nhẹ của dâu cân bằng hoàn hảo với độ ngọt của kem.', 
 78000, 1, 18, 
 N'/Content/Images/Products/strawBerrytiramisu.png,/Content/Images/Products/CROCHI_Strawberry.png,/Content/Images/Products/merryBerry_1.jpg'),

(N'Tiramisu Dưa Lưới', 
 N'Hương vị mới lạ với dưa lưới Nhật ngọt thơm, kết hợp cùng kem cheese nhẹ nhàng. Món tráng miệng hoàn hảo cho mùa hè với vị ngọt tự nhiên và mát lạnh.', 
 75000, 1, 15, 
 N'/Content/Images/Products/MelonTiramisu.png'),

-- Mousse Collection  
(N'Mousse Blueberry 4 Tầng', 
 N'Bánh mousse việt quất cao cấp với 4 lớp hương vị khác nhau: mousse việt quất, kem vanilla, bánh chocolate và gelato việt quất. Mỗi lớp mang đến trải nghiệm vị giác độc đáo.', 
 85000, 1, 20, 
 N'/Content/Images/Products/MousseBlueberry_1.jpg,/Content/Images/Products/MousseBlueberry_3.jpg,/Content/Images/Products/MousseBlueberry_4.jpg,/Content/Images/Products/MousseBlueberry_w.jpg'),

(N'Mousse Xoài Cát Hòa Lộc', 
 N'Bánh mousse từ xoài cát Hòa Lộc nguyên chất, không pha trộn. Vị ngọt đậm đà tự nhiên của xoài kết hợp với mousse mềm mịn, tan ngay trong miệng.', 
 70000, 1, 25, 
 N'/Content/Images/Products/MousseXoai_1.jpg,/Content/Images/Products/MousseXoai_2.jpg,/Content/Images/Products/crepeOvocado.png'),

(N'Mousse Vải Thiều', 
 N'Mousse vải thiều Bắc Giang với hương thơm đặc trưng. Lớp mousse mềm mịn kết hợp cùng miếng vải tươi, mang đến vị ngọt thanh mát và hương thơm quyến rũ.', 
 68000, 1, 22, 
 N'/Content/Images/Products/mousseVai_1.jpg,/Content/Images/Products/mousseVai_2.jpg,/Content/Images/Products/mousseVai_3.jpg'),

-- Cheesecake & Chocolate
(N'Basque Cheesecake Nướng', 
 N'Bánh phô mai Basque nướng truyền thống với lớp vỏ ngoài cháy nhẹ đặc trưng. Bên trong mềm mịn như custard, vị béo ngậy của cream cheese hòa quyện cùng vị ngọt dịu nhẹ.', 
 95000, 1, 12, 
 N'/Content/Images/Products/BasqueCheesecake.png'),

(N'Chocolate Cake Premium', 
 N'Bánh chocolate đen Bỉ cao cấp với ganache mềm mịn. Sử dụng chocolate 70% cacao, mang đến vị đắng đậm đà và hậu vị ngọt dài. Được trang trí tinh tế với chocolate chips.', 
 88000, 1, 18, 
 N'/Content/Images/Products/choco_cake_1.jpg,/Content/Images/Products/CROCHI_choco.png,/Content/Images/Products/SaltedChocolate.png'),

(N'Salted Caramel Chocolate', 
 N'Bánh chocolate đặc biệt với nhân caramel muối biển. Vị ngọt của chocolate kết hợp với caramel và chút muối biển tạo nên hương vị phức hợp, độc đáo và nghiện.', 
 92000, 1, 15, 
 N'/Content/Images/Products/SaltedMarbleCoffee.png,/Content/Images/Products/saltedCaCaoAvocado.png'),

-- Flan & Pudding
(N'Bánh Flan Caramel Truyền Thống', 
 N'Bánh flan caramel theo công thức truyền thống với trứng gà tươi và sữa tươi nguyên chất. Lớp caramel đắng nhẹ cân bằng hoàn hảo với độ ngọt của bánh flan mềm mịn.', 
 35000, 1, 40, 
 N'/Content/Images/Products/flan_caramel.png,/Content/Images/Products/ButterScotchBrulee.png'),

(N'Misu Thiệc - Flan Cao Cấp', 
 N'Flan cao cấp với công thức bí mật, sử dụng kem tươi và vanilla Madagascar. Kết cấu mịn màng hơn flan thường, vị béo ngậy quyến rũ và hương thơm vanilla tự nhiên.', 
 52000, 1, 25, 
 N'/Content/Images/Products/misuThiec_1.jpg,/Content/Images/Products/misuThiec_2.jpg,/Content/Images/Products/misuThiec_3.jpg'),

(N'Merry Berry Pudding', 
 N'Pudding trái cây tổng hợp với dâu tây, việt quất và raspberry. Mỗi lớp có màu sắc và hương vị riêng biệt, tạo nên món tráng miệng đầy màu sắc và dinh dưỡng.', 
 48000, 1, 30, 
 N'/Content/Images/Products/merryBerry_1.jpg,/Content/Images/Products/merryBerry_2.jpg,/Content/Images/Products/merryBerry_3.jpg'),

-- Crepes & Pastries
(N'Matcha Crepe Nhật Bản', 
 N'Bánh crepe matcha mỏng mịn với kem tươi và bột matcha Uji cao cấp. Lớp vỏ crepe mềm mại kết hợp cùng kem ngọt và vị đắng nhẹ của matcha tạo nên hương vị cân bằng.', 
 62000, 1, 20, 
 N'/Content/Images/Products/matchaCrepe.png,/Content/Images/Products/CookieMatcha.png'),

(N'Avocado Crepe Healthy', 
 N'Crepe bơ tươi bổ dưỡng với bơ Đắk Lắk nguyên chất. Giàu vitamin và chất béo tốt, kết hợp cùng mật ong tự nhiên và hạt chia, món ăn healthy và ngon miệng.', 
 58000, 1, 18, 
 N'/Content/Images/Products/crepeOvocado.png'),

(N'French Toast Brulée Đặc Biệt', 
 N'Bánh mì nướng kiểu Pháp với lớp caramel cháy giòn bên ngoài. Bên trong mềm ẩm được ngâm trong hỗn hợp trứng và kem tươi, phủ đường caramel và nướng đến vàng ruộm.', 
 68000, 1, 15, 
 N'/Content/Images/Products/SWEET-BRULEED-CITRUS-FRENCH-TOAST-scaled.png,/Content/Images/Products/CreamBruleeBriocheFrenchToasts.png'),

(N'Dalgona French Toast', 
 N'French toast độc đáo với kem Dalgona coffee đặc trưng Hàn Quốc. Lớp kem Dalgona bông mịn kết hợp cùng bánh mì brioche thơm nướng, tạo nên món ăn Instagram-worthy.', 
 72000, 1, 12, 
 N'/Content/Images/Products/SWEET-DALGONA-VELVET-FRENCH-TOAST-scaled.png'),

(N'Buttermilk Pancake Stack', 
 N'Stack 3 lớp pancake bơ sữa mềm mịn kiểu Mỹ. Được phục vụ kèm bơ lạt, siro maple Canada và berry tươi. Món ăn sáng kinh điển với hương vị đậm đà khó quên.', 
 75000, 1, 18, 
 N'/Content/Images/Products/ButtermilkPancakeStack.png'),

(N'Hong Kong Egg Waffles', 
 N'Bánh waffle trứng Hong Kong giòn rụm bên ngoài, mềm bên trong. Được làm từ bột mì cao cấp và trứng gà tươi, tạo nên những viên tròn đặc trưng và hương vị thơm ngon.', 
 45000, 1, 25, 
 N'/Content/Images/Products/HongkongEggWaffles.png'),

(N'Chocolate Croissant Pháp', 
 N'Bánh sừng bò chocolate kiểu Pháp với 64 lớp bơ lạt. Vỏ ngoài giòn tan, bên trong mềm mại với nhân chocolate đen Valrhona. Được nướng tươi mỗi ngày theo công thức truyền thống.', 
 42000, 1, 30, 
 N'/Content/Images/Products/choco_croissant.jpg'),

(N'Matcha Croissant Fusion', 
 N'Croissant fusion Việt-Pháp với bột matcha Nhật Bản. Kết hợp kỹ thuật làm bánh Pháp với hương vị Á Đông, tạo nên món bánh độc đáo với màu xanh đẹp mắt và vị đắng nhẹ quyến rũ.', 
 45000, 1, 25, 
 N'/Content/Images/Products/matcha_croissant.jpg'),

(N'Cloudy Lava Nutella', 
 N'Bánh mây với nhân Nutella chảy nóng hổi. Lớp vỏ mềm như mây kết hợp cùng nhân Nutella bên trong, khi cắn vào sẽ có Nutella chảy ra như dung nham, vừa ngon vừa vui mắt.', 
 55000, 1, 20, 
 N'/Content/Images/Products/Cloudy-Lava-Nutella_220k.png');

-- ==========================================
-- NƯỚC UỐNG (CategoryId = 2)  
-- ==========================================

INSERT INTO Product (Name, Description, Price, CategoryId, StockQuantity, ImageUrl) VALUES

-- Matcha & Tea Collection
(N'Matcha Latte Signature', 
 N'Trà sữa matcha đặc trưng với bột matcha Uji nguyên chất và sữa tươi organic. Được pha chế theo tỷ lệ vàng, tạo nên vị đắng nhẹ hài hòa với độ béo ngậy của sữa và ngọt dịu của đường.', 
 48000, 2, 35, 
 N'/Content/Images/Products/MatchaLatteS.png,/Content/Images/Products/MATCHA-COCO-EARL-GREY-MATCHA-LATTE.png,/Content/Images/Products/StrawMatchaLate.png'),

(N'Matcha Cloud Smoothie', 
 N'Smoothie matcha với lớp foam mây đặc biệt. Sử dụng matcha ceremonial grade và kỹ thuật tạo foam độc quyền, mang đến trải nghiệm thị giác và vị giác tuyệt vời.', 
 52000, 2, 25, 
 N'/Content/Images/Products/MATCHA-MATCHA-CLOUD-SMOOTHIE-scaled.png'),

(N'Matcha Vanilla Coconut', 
 N'Matcha kết hợp với vanilla Madagascar và nước cốt dừa tươi. Hương vị nhiệt đới độc đáo với sự pha trộn giữa đắng nhẹ của matcha, ngọt dịu của vanilla và béo ngậy của dừa.', 
 50000, 2, 20, 
 N'/Content/Images/Products/MATCHA-VANILLA-COCO-MATCHA-COLD-FOAM.png'),

(N'Trà Sữa Trân Châu Đen Premium', 
 N'Trà sữa trân châu đen theo công thức Taiwan với trà đen Ceylon cao cấp. Trân châu được nấu tươi mỗi ngày, dai ngon và thấm vị. Sữa tươi nguyên kem tạo độ béo ngậy hoàn hảo.', 
 42000, 2, 40, 
 N'/Content/Images/Products/EarlGreyWithBoba.png'),

(N'Gentle Hojicha', 
 N'Trà hojicha rang nhẹ với hương thơm nướng đặc trưng. Vị ngọt tự nhiên từ quá trình rang, không đắng chát, phù hợp cho mọi lứa tuổi. Thức uống thư giãn hoàn hảo.', 
 38000, 2, 30, 
 N'/Content/Images/Products/gentleHojicha.png'),

(N'Rich Hojicha Đậm Đà', 
 N'Hojicha rang đậm với hương vị nồng nàn hơn. Được rang ở nhiệt độ cao tạo hương thơm caramel tự nhiên. Thích hợp cho những ai yêu thích vị đậm đà và hương thơm đặc trưng.', 
 42000, 2, 25, 
 N'/Content/Images/Products/richHojicha.png'),

(N'Matcha Oat Milk Healthy', 
 N'Matcha với sữa yến mạch organic, hoàn toàn thuần chay. Giàu chất xơ và protein thực vật, ít calo nhưng vẫn đậm đà. Món uống healthy trend cho lối sống hiện đại.', 
 55000, 2, 18, 
 N'/Content/Images/Products/matchaOatSide.png'),

-- Coffee Collection
(N'Espresso Single Origin', 
 N'Cà phê espresso từ hạt arabica đơn nguồn gốc Ethiopia. Rang vừa tối để giữ trọn hương thơm floral và vị chua nhẹ đặc trưng. Được pha bằng máy espresso chuyên nghiệp.', 
 28000, 2, 50, 
 N'/Content/Images/Products/Espresso.png,/Content/Images/Products/EspressoMatchaLatte.png'),

(N'Americano Classic', 
 N'Cà phê Americano đen nguyên chất pha từ espresso và nước nóng. Giữ trọn hương vị cà phê nguyên bản, đắng nhẹ và có hậu vị ngọt tự nhiên. Không đường, không sữa.', 
 32000, 2, 45, 
 N'/Content/Images/Products/Americano.png'),

(N'Cappuccino Ý Chính Hiệu', 
 N'Cappuccino truyền thống Ý với tỷ lệ 1:1:1 espresso, sữa nóng và foam sữa. Lớp foam dày mịn được tạo bằng kỹ thuật steaming chuyên nghiệp, mang đến trải nghiệm cà phê Ý đích thực.', 
 38000, 2, 35, 
 N'/Content/Images/Products/Capuccino.png'),

(N'Latte Art Signature', 
 N'Café Latte với nghệ thuật vẽ trên foam sữa. Mỗi ly latte đều được barista tạo ra những họa tiết độc đáo. Vị mềm mại của sữa tươi hòa quyện hoàn hảo với espresso đậm đà.', 
 42000, 2, 30, 
 N'/Content/Images/Products/LatteFlatWhite.png,/Content/Images/Products/TiramissuLatte.png'),

(N'Mocha Chocolate Premium', 
 N'Cà phê mocha với chocolate Valrhona cao cấp. Sự kết hợp hoàn hảo giữa đắng của cà phê và ngọt của chocolate, tạo nên món uống đậm đà và hấp dẫn cho cả người uống cà phê và chocolate.', 
 45000, 2, 28, 
 N'/Content/Images/Products/Mocha.png,/Content/Images/Products/SIGNATURE-MOCHA-BROWN-SUGAR-LATTE.png'),

(N'Caramel Macchiato Đặc Biệt', 
 N'Macchiato với caramel tự làm và vanilla syrup. Lớp caramel được đun đến độ đắng nhẹ, kết hợp với espresso và sữa tươi tạo nên vị ngọt đắng hài hòa và quyến rũ.', 
 48000, 2, 22, 
 N'/Content/Images/Products/CaremelMachiato.png,/Content/Images/Products/TiramissuMatchaCaremel.png'),

(N'Vietnamese Drip Coffee', 
 N'Cà phê sữa đá Việt Nam truyền thống với phin nhôm và cà phê robusta rang đậm. Được pha theo phương pháp phin chậm, tạo ra ly cà phê đậm đà với lớp sữa đặc ngọt bên dưới.', 
 35000, 2, 40, 
 N'/Content/Images/Products/VietnameseMilkCoffee.png'),

(N'S''mores Latte Độc Đáo', 
 N'Latte với hương vị bánh quy S''mores Mỹ. Kết hợp marshmallow nướng, chocolate graham và espresso, tạo nên món uống độc đáo như đang cắm trại bên đống lửa.', 
 52000, 2, 15, 
 N'/Content/Images/Products/Soko''MoresLatte.png'),

-- Other Beverages
(N'Iced Chocolate Deluxe', 
 N'Chocolate đá cao cấp với chocolate đen Bỉ và kem tươi. Được pha chế đậm đà với chocolate nguyên chất, tạo độ béo ngậy và hương vị chocolate đích thực. Thêm whipped cream và chocolate chips.', 
 38000, 2, 32, 
 N'/Content/Images/Products/IcedChocolate.png'),

(N'Vegan Granola Smoothie', 
 N'Smoothie granola thuần chay với yến mạch, hạnh nhân và hoa quả tươi. Giàu protein thực vật và chất xơ, không chứa sản phẩm từ động vật. Món uống healthy và bổ dưỡng.', 
 48000, 2, 20, 
 N'/Content/Images/Products/VeganGranola.png'),

(N'Oolong Đào Hồng Thơm', 
 N'Trà oolong Taiwan cao cấp với đào hồng tươi. Hương thơm tự nhiên của đào kết hợp với vị thanh đậm đà của oolong, tạo nên thức uống thơm mát và thanh tịnh.', 
 44000, 2, 25, 
 N'/Content/Images/Products/OlongDaoHong.png'),

(N'Nước Lụa Đào Mát Lạnh', 
 N'Trà đào mềm mại như lụa với đào tươi Nhật Bản. Được pha chế với trà xanh nhẹ và lát đào tươi, tạo vị ngọt tự nhiên và mát lạnh. Thức uống mùa hè hoàn hảo.', 
 40000, 2, 28, 
 N'/Content/Images/Products/NuocLuaDao.png');

PRINT N'Đã tạo lại tất cả sản phẩm thành công!';

-- Kiểm tra kết quả
SELECT CategoryId, COUNT(*) as ProductCount, 
       CASE CategoryId 
           WHEN 1 THEN N'Bánh Tráng Miệng'
           WHEN 2 THEN N'Nước Uống'
           ELSE N'Khác'
       END as CategoryName
FROM Product 
GROUP BY CategoryId 
ORDER BY CategoryId;

SELECT COUNT(*) as TotalProducts FROM Product; 