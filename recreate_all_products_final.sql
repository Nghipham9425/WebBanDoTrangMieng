-- Script tạo lại tất cả sản phẩm với ảnh đúng tên
-- Xóa hết sản phẩm cũ trước
USE QLStoreTrangMieng;

-- Xóa hết dữ liệu cũ
DELETE FROM Products;
DELETE FROM Categories;

-- Reset identity
DBCC CHECKIDENT ('Products', RESEED, 0);
DBCC CHECKIDENT ('Categories', RESEED, 0);

-- Tạo lại categories
INSERT INTO Categories (CategoryName, Description) VALUES
(N'Bánh Tráng Miệng', N'Các loại bánh ngọt, tiramisu, cheesecake, mousse và đồ tráng miệng'),
(N'Nước Uống', N'Các loại nước uống, cà phê, trà và đồ uống đặc biệt');

-- Tạo lại tất cả sản phẩm với ảnh đúng tên

-- BÁNH TRÁNG MIỆNG (CategoryID = 1)

-- 1. Tiramisu Original
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Tiramisu Original', N'Tiramisu truyền thống Ý với hương vị cà phê đậm đà, mascarpone mềm mịn và lớp cacao thơm lừng. Một tác phẩm kinh điển không thể bỏ qua.', 65000, 1, '/Content/Images/Products/OriginalTiramisu.png,/Content/Images/Products/tiramisu_1.jpg,/Content/Images/Products/tiramisu_2.jpg', GETDATE(), 1);

-- 2. Tiramisu Dâu
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Tiramisu Dâu', N'Phiên bản tiramisu tươi mát với dâu tây tự nhiên, kem mascarpone và bánh ladyfinger. Vị ngọt thanh của dâu hòa quyện cùng độ béo ngậy của kem.', 70000, 1, '/Content/Images/Products/strawBerrytiramisu.png', GETDATE(), 1);

-- 3. Tiramisu Dưa Lưới
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Tiramisu Dưa Lưới', N'Tiramisu độc đáo với hương dưa lưới tươi mát, kết hợp hoàn hảo giữa vị ngọt của trái cây và độ béo ngậy của mascarpone.', 68000, 1, '/Content/Images/Products/MelonTiramisu.png', GETDATE(), 1);

-- 4. Misu Thiếc
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Misu Thiếc', N'Dessert đặc biệt lấy cảm hứng từ tiramisu nhưng có hương vị riêng biệt, kết cấu mềm mịn và vị ngọt vừa phải.', 60000, 1, '/Content/Images/Products/misuThiec_1.jpg,/Content/Images/Products/misuThiec_2.jpg,/Content/Images/Products/misuThiec_3.jpg', GETDATE(), 1);

-- 5. Matchamisu
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Matchamisu', N'Sự kết hợp hoàn hảo giữa tiramisu và matcha Nhật Bản. Vị đắng nhẹ của trà xanh hòa quyện với độ ngọt béo của mascarpone.', 72000, 1, '/Content/Images/Products/matchamisu_1.jpg,/Content/Images/Products/matchamisu_2.jpg', GETDATE(), 1);

-- 6. Basque Cheesecake
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Basque Cheesecake', N'Bánh phô mai Basque truyền thống với lớp vỏ nướng cháy nhẹ đặc trưng, bên trong mềm mịn như kem. Hương vị đậm đà, khó quên.', 85000, 1, '/Content/Images/Products/BasqueCheesecake.png', GETDATE(), 1);

-- 7. Mousse Blueberry
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Mousse Blueberry', N'Mousse việt quất tươi với kết cấu nhẹ như mây, vị chua ngọt tự nhiên của blueberry tạo nên một món tráng miệng tuyệt vời.', 58000, 1, '/Content/Images/Products/MousseBlueberry_1.jpg,/Content/Images/Products/MousseBlueberry_2.jpg,/Content/Images/Products/MousseBlueberry_3.jpg,/Content/Images/Products/MousseBlueberry_4.jpg', GETDATE(), 1);

-- 8. Mousse Xoài
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Mousse Xoài', N'Mousse xoài tươi ngọt với hương vị nhiệt đới đặc trưng, kết cấu mềm mịn tan trong miệng. Thích hợp cho những ngày hè nóng bức.', 55000, 1, '/Content/Images/Products/MousseXoai_1.jpg,/Content/Images/Products/MousseXoai_2.jpg', GETDATE(), 1);

-- 9. Mousse Vải
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Mousse Vải', N'Mousse vải thiều với hương vị ngọt ngào đặc trưng của loại trái cây này, kết cấu mềm mịn và màu sắc hấp dẫn.', 60000, 1, '/Content/Images/Products/mousseVai_1.jpg,/Content/Images/Products/mousseVai_2.jpg,/Content/Images/Products/mousseVai_3.jpg', GETDATE(), 1);

-- 10. Merry Berry
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Merry Berry', N'Tổng hòa của nhiều loại berry tươi ngon, tạo nên một món tráng miệng đầy màu sắc và hương vị phong phú.', 65000, 1, '/Content/Images/Products/merryBerry_1.jpg,/Content/Images/Products/merryBerry_2.jpg,/Content/Images/Products/merryBerry_3.jpg', GETDATE(), 1);

-- 11. Flan Caramel
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Flan Caramel', N'Bánh flan truyền thống với lớp caramel đắng ngọt hài hòa, kết cấu mềm mịn tan chảy. Món tráng miệng kinh điển được yêu thích.', 45000, 1, '/Content/Images/Products/flan_caramel.png', GETDATE(), 1);

-- 12. Butter Scotch Brulee
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Butter Scotch Brulee', N'Crème brûlée với hương butterscotch đặc biệt, lớp đường cháy giòn tan kết hợp với kem mềm mịn bên dưới.', 68000, 1, '/Content/Images/Products/ButterScotchBrulee.png', GETDATE(), 1);

-- 13. Chocolate Cake
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Chocolate Cake', N'Bánh chocolate đậm đà với nhiều lớp kem chocolate, phủ ganache bóng mịn. Thiên đường cho những người yêu chocolate.', 75000, 1, '/Content/Images/Products/choco_cake_1.jpg', GETDATE(), 1);

-- 14. Salted Chocolate
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Salted Chocolate', N'Chocolate mặn ngọt độc đáo với chút muối biển, tạo nên hương vị phức tạp và hấp dẫn khó cưỡng.', 65000, 1, '/Content/Images/Products/SaltedChocolate.png', GETDATE(), 1);

-- 15. Croissant Chocolate
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Croissant Chocolate', N'Bánh sừng bò Pháp giòn rụm với nhân chocolate đậm đà, kết hợp hoàn hảo giữa vỏ bánh bơ và chocolate ngọt ngào.', 40000, 1, '/Content/Images/Products/choco_croissant.jpg', GETDATE(), 1);

-- 16. Croissant Matcha
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Croissant Matcha', N'Bánh sừng bò với nhân matcha thơm lừng, vị đắng nhẹ của trà xanh kết hợp với độ bơ của croissant.', 42000, 1, '/Content/Images/Products/matcha_croissant.jpg', GETDATE(), 1);

-- 17. Crochi Strawberry
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Crochi Strawberry', N'Bánh crochi nhân dâu tây tươi, vỏ bánh giòn tan và nhân dâu ngọt ngào, thích hợp làm món ăn nhẹ.', 35000, 1, '/Content/Images/Products/CROCHI_Strawberry.png', GETDATE(), 1);

-- 18. Crochi Chocolate
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Crochi Chocolate', N'Bánh crochi nhân chocolate đậm đà, kết cấu giòn rụm bên ngoài và mềm mịn bên trong.', 35000, 1, '/Content/Images/Products/CROCHI_choco.png', GETDATE(), 1);

-- 19. Crochi Matcha
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Crochi Matcha', N'Bánh crochi nhân matcha thơm lừng, vị trà xanh đặc trưng của Nhật Bản trong từng miếng bánh.', 35000, 1, '/Content/Images/Products/CROCHI_matcha.png', GETDATE(), 1);

-- 20. Matcha Crepe
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Matcha Crepe', N'Bánh crepe matcha mỏng mịn với kem matcha thơm ngon, món tráng miệng Nhật Bản tinh tế.', 48000, 1, '/Content/Images/Products/matchaCrepe.png', GETDATE(), 1);

-- 21. Crepe Avocado
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Crepe Avocado', N'Bánh crepe độc đáo với nhân bơ tươi, vị béo ngậy tự nhiên của bơ kết hợp với bánh crepe mềm mịn.', 50000, 1, '/Content/Images/Products/crepeOvocado.png', GETDATE(), 1);

-- 22. Cookie Matcha
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Cookie Matcha', N'Bánh quy matcha giòn tan với hương vị trà xanh đậm đà, thích hợp làm snack hoặc tráng miệng nhẹ.', 30000, 1, '/Content/Images/Products/CookieMatcha.png', GETDATE(), 1);

-- 23. Hong Kong Egg Waffles
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Hong Kong Egg Waffles', N'Bánh waffle trứng Hong Kong đặc trưng với hình dạng tổ ong độc đáo, giòn bên ngoài mềm bên trong.', 45000, 1, '/Content/Images/Products/HongkongEggWaffles.png', GETDATE(), 1);

-- 24. Buttermilk Pancake Stack
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Buttermilk Pancake Stack', N'Chồng bánh pancake buttermilk mềm mịn với syrup và bơ, món ăn sáng kiểu Mỹ đầy hấp dẫn.', 55000, 1, '/Content/Images/Products/ButtermilkPancakeStack.png', GETDATE(), 1);

-- 25. Cream Brulee Brioche French Toast
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Cream Brulee Brioche French Toast', N'Bánh mì nướng kiểu Pháp brioche với lớp kem brulee đặc biệt, kết hợp giữa bánh mì và tráng miệng.', 65000, 1, '/Content/Images/Products/CreamBruleeBriocheFrenchToasts.png', GETDATE(), 1);

-- 26. Sweet Bruleed Citrus French Toast
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Sweet Bruleed Citrus French Toast', N'Bánh mì nướng kiểu Pháp với hương cam chanh tươi mát và lớp đường cháy giòn tan.', 62000, 1, '/Content/Images/Products/SWEET-BRULEED-CITRUS-FRENCH-TOAST-scaled.png', GETDATE(), 1);

-- 27. Sweet Dalgona Velvet French Toast
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Sweet Dalgona Velvet French Toast', N'Bánh mì nướng với kem dalgona mềm mịn như nhung, phiên bản hiện đại của món bánh mì nướng truyền thống.', 68000, 1, '/Content/Images/Products/SWEET-DALGONA-VELVET-FRENCH-TOAST-scaled.png', GETDATE(), 1);

-- 28. Cloudy Lava Nutella
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Cloudy Lava Nutella', N'Bánh lava Nutella với nhân chocolate nóng chảy bên trong, bề mặt như đám mây trắng mềm mịn.', 58000, 1, '/Content/Images/Products/Cloudy-Lava-Nutella_220k.png', GETDATE(), 1);

-- 29. Vegan Granola
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Vegan Granola', N'Granola thuần chay với hạt và trái cây khô, lành mạnh và dinh dưỡng, thích hợp cho người ăn chay.', 35000, 1, '/Content/Images/Products/VeganGranola.png', GETDATE(), 1);

-- NƯỚC UỐNG (CategoryID = 2)

-- 30. Espresso
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Espresso', N'Cà phê espresso đậm đà nguyên chất, chiết xuất từ hạt cà phê chất lượng cao, hương vị mạnh mẽ và thơm lừng.', 35000, 2, '/Content/Images/Products/Espresso.png', GETDATE(), 1);

-- 31. Americano
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Americano', N'Cà phê americano nhẹ nhàng, pha loãng từ espresso với nước nóng, vị đắng nhẹ và thơm mát.', 40000, 2, '/Content/Images/Products/Americano.png', GETDATE(), 1);

-- 32. Cappuccino
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Cappuccino', N'Cappuccino truyền thống Ý với espresso, sữa nóng và lớp foam dày, tạo nên hương vị hài hòa.', 50000, 2, '/Content/Images/Products/Capuccino.png', GETDATE(), 1);

-- 33. Latte Flat White
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Latte Flat White', N'Flat white kiểu Úc với espresso đậm đà và sữa microfoam mềm mịn, vị cà phê nổi bật hơn latte thông thường.', 52000, 2, '/Content/Images/Products/LatteFlatWhite.png', GETDATE(), 1);

-- 34. Caramel Macchiato
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Caramel Macchiato', N'Macchiato caramel ngọt ngào với espresso, sữa nóng và syrup caramel thơm lừng, đầy hấp dẫn.', 55000, 2, '/Content/Images/Products/CaremelMachiato.png', GETDATE(), 1);

-- 35. Mocha
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Mocha', N'Mocha chocolate đậm đà kết hợp giữa espresso và chocolate, tạo nên hương vị ngọt đắng hài hòa.', 58000, 2, '/Content/Images/Products/Mocha.png', GETDATE(), 1);

-- 36. Vietnamese Milk Coffee
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Vietnamese Milk Coffee', N'Cà phê sữa Việt Nam truyền thống với cà phê phin và sữa đặc ngọt, hương vị đặc trưng của quê hương.', 45000, 2, '/Content/Images/Products/VietnameseMilkCoffee.png', GETDATE(), 1);

-- 37. Iced Chocolate
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Iced Chocolate', N'Chocolate đá mát lạnh với kem tươi, thích hợp cho những ngày nóng bức, vị ngọt ngào quyến rũ.', 48000, 2, '/Content/Images/Products/IcedChocolate.png', GETDATE(), 1);

-- 38. Matcha Latte
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Matcha Latte', N'Latte matcha Nhật Bản với bột trà xanh cao cấp và sữa nóng, vị đắng nhẹ đặc trưng của matcha.', 55000, 2, '/Content/Images/Products/MatchaLatteS.png', GETDATE(), 1);

-- 39. Strawberry Matcha Latte
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Strawberry Matcha Latte', N'Matcha latte kết hợp với hương dâu tây tươi mát, sự pha trộn độc đáo giữa Á và Âu.', 60000, 2, '/Content/Images/Products/StrawMatchaLate.png', GETDATE(), 1);

-- 40. Espresso Matcha Latte
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Espresso Matcha Latte', N'Sự kết hợp táo bạo giữa espresso và matcha, tạo nên hương vị đắng đặc biệt và độc đáo.', 62000, 2, '/Content/Images/Products/EspressoMatchaLatte.png', GETDATE(), 1);

-- 41. Tiramisu Latte
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Tiramisu Latte', N'Latte với hương vị tiramisu đặc trưng, kết hợp cà phê, mascarpone và cacao, như uống tiramisu lỏng.', 65000, 2, '/Content/Images/Products/TiramissuLatte.png', GETDATE(), 1);

-- 42. Tiramisu Matcha Caramel
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Tiramisu Matcha Caramel', N'Đồ uống fusion độc đáo kết hợp tiramisu, matcha và caramel, một trải nghiệm hương vị hoàn toàn mới.', 68000, 2, '/Content/Images/Products/TiramissuMatchaCaremel.png', GETDATE(), 1);

-- 43. Soko Mores Latte
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Soko Mores Latte', N'Latte với hương vị s''mores (marshmallow, chocolate, graham cracker), mang đến cảm giác như đang cắm trại.', 58000, 2, '/Content/Images/Products/Soko''MoresLatte.png', GETDATE(), 1);

-- 44. Signature Mocha Brown Sugar Latte
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Signature Mocha Brown Sugar Latte', N'Latte đặc trưng của quán với mocha và đường nâu, hương vị phong phú và đậm đà.', 65000, 2, '/Content/Images/Products/SIGNATURE-MOCHA-BROWN-SUGAR-LATTE.png', GETDATE(), 1);

-- 45. Salted Marble Coffee
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Salted Marble Coffee', N'Cà phê marble mặn ngọt với hiệu ứng màu sắc đẹp mắt, vị mặn nhẹ tạo điểm nhấn thú vị.', 55000, 2, '/Content/Images/Products/SaltedMarbleCoffee.png', GETDATE(), 1);

-- 46. Salted Cacao Avocado
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Salted Cacao Avocado', N'Đồ uống độc đáo với bơ, cacao và muối biển, mang lại hương vị béo ngậy và lạ miệng.', 58000, 2, '/Content/Images/Products/saltedCaCaoAvocado.png', GETDATE(), 1);

-- 47. Matcha Oat
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Matcha Oat', N'Matcha với sữa yến mạch, lành mạnh và thơm ngon, thích hợp cho người ăn chay hoặc không dung nạp lactose.', 52000, 2, '/Content/Images/Products/matchaOatSide.png', GETDATE(), 1);

-- 48. Gentle Hojicha
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Gentle Hojicha', N'Trà hojicha nhẹ nhàng với hương vị nướng đặc trưng, ít caffeine và dễ uống.', 48000, 2, '/Content/Images/Products/gentleHojicha.png', GETDATE(), 1);

-- 49. Rich Hojicha
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Rich Hojicha', N'Hojicha đậm đà với hương vị nướng mạnh mẽ hơn, dành cho những ai yêu thích vị đặc biệt của trà rang.', 50000, 2, '/Content/Images/Products/richHojicha.png', GETDATE(), 1);

-- 50. Oolong Đào Hồng
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Oolong Đào Hồng', N'Trà oolong kết hợp với hương đào hồng tự nhiên, vị ngọt thanh và thơm mát của trái cây.', 45000, 2, '/Content/Images/Products/OlongDaoHong.png', GETDATE(), 1);

-- 51. Nước Lựu Đào
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Nước Lựu Đào', N'Nước ép lựu đào tươi mát với nhiều vitamin, vị chua ngọt tự nhiên và màu sắc hấp dẫn.', 42000, 2, '/Content/Images/Products/NuocLuaDao.png', GETDATE(), 1);

-- 52. Earl Grey With Boba
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Earl Grey With Boba', N'Trà Earl Grey thơm bergamot kết hợp với trân châu boba dai ngon, phong cách trà sữa hiện đại.', 48000, 2, '/Content/Images/Products/EarlGreyWithBoba.png', GETDATE(), 1);

-- 53. Matcha Coco Earl Grey Matcha Latte
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Matcha Coco Earl Grey Matcha Latte', N'Latte fusion phức tạp với matcha, dừa và Earl Grey, tạo nên hương vị đa tầng độc đáo.', 68000, 2, '/Content/Images/Products/MATCHA-COCO-EARL-GREY-MATCHA-LATTE.png', GETDATE(), 1);

-- 54. Matcha Vanilla Coco Matcha Cold Foam
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Matcha Vanilla Coco Cold Foam', N'Đồ uống matcha với vanilla, dừa và cold foam mịn màng, mát lạnh và thơm ngon.', 65000, 2, '/Content/Images/Products/MATCHA-VANILLA-COCO-MATCHA-COLD-FOAM.png', GETDATE(), 1);

-- 55. Matcha Cloud Smoothie
INSERT INTO Products (ProductName, Description, Price, CategoryID, ImageUrl, CreatedDate, IsActive) VALUES
(N'Matcha Cloud Smoothie', N'Smoothie matcha mịn như mây với kết cấu nhẹ nhàng, vị matcha đậm đà và mát lạnh.', 58000, 2, '/Content/Images/Products/MATCHA-MATCHA-CLOUD-SMOOTHIE-scaled.png', GETDATE(), 1);

PRINT 'Đã tạo lại tất cả sản phẩm thành công!';
PRINT 'Tổng cộng: 55 sản phẩm';
PRINT '- Bánh tráng miệng: 29 sản phẩm';
PRINT '- Nước uống: 26 sản phẩm'; 