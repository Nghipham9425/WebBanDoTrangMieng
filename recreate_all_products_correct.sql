-- Script tạo lại tất cả sản phẩm với tên bảng và cột đúng
-- Database: QLStoreTrangMieng
USE QLStoreTrangMieng;

-- Xóa hết dữ liệu cũ
DELETE FROM Product;
DELETE FROM Category;

-- Reset identity
DBCC CHECKIDENT ('Product', RESEED, 0);
DBCC CHECKIDENT ('Category', RESEED, 0);

-- Tạo lại categories với tên cột đúng: Name (không phải CategoryName)
INSERT INTO Category (Name, Description) VALUES
(N'Bánh Tráng Miệng', N'Các loại bánh ngọt, tiramisu, cheesecake, mousse và đồ tráng miệng'),
(N'Nước Uống', N'Các loại nước uống, cà phê, trà và đồ uống đặc biệt');

-- Tạo lại tất cả sản phẩm với tên cột đúng: Name (không phải ProductName)

-- =============================================
-- BÁNH TRÁNG MIỆNG (CategoryId = 1)
-- =============================================

-- 1. Tiramisu Original
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Tiramisu Original', N'Tiramisu truyền thống Ý với hương vị cà phê đậm đà, mascarpone mềm mịn và lớp cacao thơm lừng. Một tác phẩm kinh điển không thể bỏ qua.', 65000, 1, '/Content/Images/Products/OriginalTiramisu.png,/Content/Images/Products/tiramisu_1.jpg,/Content/Images/Products/tiramisu_2.jpg', GETDATE());

-- 2. Tiramisu Dâu
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Tiramisu Dâu', N'Phiên bản tiramisu tươi mát với dâu tây tự nhiên, kem mascarpone và bánh ladyfinger. Vị ngọt thanh của dâu hòa quyện cùng độ béo ngậy của kem.', 70000, 1, '/Content/Images/Products/strawBerrytiramisu.png', GETDATE());

-- 3. Tiramisu Dưa Lưới
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Tiramisu Dưa Lưới', N'Tiramisu độc đáo với hương dưa lưới tươi mát, kết hợp hoàn hảo giữa vị ngọt của trái cây và độ béo ngậy của mascarpone.', 68000, 1, '/Content/Images/Products/MelonTiramisu.png', GETDATE());

-- 4. Misu Thiếc
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Misu Thiếc', N'Dessert đặc biệt lấy cảm hứng từ tiramisu nhưng có hương vị riêng biệt, kết cấu mềm mịn và vị ngọt vừa phải.', 60000, 1, '/Content/Images/Products/misuThiec_1.jpg,/Content/Images/Products/misuThiec_2.jpg,/Content/Images/Products/misuThiec_3.jpg', GETDATE());

-- 5. Matchamisu
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Matchamisu', N'Sự kết hợp hoàn hảo giữa tiramisu và matcha Nhật Bản. Vị đắng nhẹ của trà xanh hòa quyện với độ ngọt béo của mascarpone.', 72000, 1, '/Content/Images/Products/matchamisu_1.jpg,/Content/Images/Products/matchamisu_2.jpg', GETDATE());

-- 6. Basque Cheesecake
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Basque Cheesecake', N'Bánh phô mai Basque truyền thống với lớp vỏ nướng cháy nhẹ đặc trưng, bên trong mềm mịn như kem. Hương vị đậm đà, khó quên.', 85000, 1, '/Content/Images/Products/BasqueCheesecake.png', GETDATE());

-- 7. Mousse Blueberry
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Mousse Blueberry', N'Mousse việt quất tươi với kết cấu nhẹ như mây, vị chua ngọt tự nhiên của blueberry tạo nên một món tráng miệng tuyệt vời.', 58000, 1, '/Content/Images/Products/MousseBlueberry_1.jpg,/Content/Images/Products/MousseBlueberry_2.jpg,/Content/Images/Products/MousseBlueberry_3.jpg,/Content/Images/Products/MousseBlueberry_4.jpg', GETDATE());

-- 8. Mousse Xoài
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Mousse Xoài', N'Mousse xoài tươi ngọt với hương vị nhiệt đới đặc trưng, kết cấu mềm mịn tan trong miệng. Thích hợp cho những ngày hè nóng bức.', 55000, 1, '/Content/Images/Products/MousseXoai_1.jpg,/Content/Images/Products/MousseXoai_2.jpg', GETDATE());

-- 9. Mousse Vải
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Mousse Vải', N'Mousse vải thiều với hương vị ngọt ngào đặc trưng của loại trái cây này, kết cấu mềm mịn và màu sắc hấp dẫn.', 60000, 1, '/Content/Images/Products/mousseVai_1.jpg,/Content/Images/Products/mousseVai_2.jpg,/Content/Images/Products/mousseVai_3.jpg', GETDATE());

-- 10. Merry Berry
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Merry Berry', N'Tổng hòa của nhiều loại berry tươi ngon, tạo nên một món tráng miệng đầy màu sắc và hương vị phong phú.', 65000, 1, '/Content/Images/Products/merryBerry_1.jpg,/Content/Images/Products/merryBerry_2.jpg,/Content/Images/Products/merryBerry_3.jpg', GETDATE());

-- 11. Flan Caramel
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Flan Caramel', N'Bánh flan truyền thống với lớp caramel đắng ngọt hài hòa, kết cấu mềm mịn tan chảy. Món tráng miệng kinh điển được yêu thích.', 45000, 1, '/Content/Images/Products/flan_caramel.png', GETDATE());

-- 12. Butter Scotch Brulee
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Butter Scotch Brulee', N'Crème brûlée với hương butterscotch đặc biệt, lớp đường cháy giòn tan kết hợp với kem mềm mịn bên dưới.', 68000, 1, '/Content/Images/Products/ButterScotchBrulee.png', GETDATE());

-- 13. Chocolate Cake
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Chocolate Cake', N'Bánh chocolate đậm đà với nhiều lớp kem chocolate, phủ ganache bóng mịn. Thiên đường cho những người yêu chocolate.', 75000, 1, '/Content/Images/Products/choco_cake_1.jpg', GETDATE());

-- 14. Salted Chocolate
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Salted Chocolate', N'Chocolate mặn ngọt độc đáo với chút muối biển, tạo nên hương vị phức tạp và hấp dẫn khó cưỡng.', 65000, 1, '/Content/Images/Products/SaltedChocolate.png', GETDATE());

-- 15. Croissant Chocolate
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Croissant Chocolate', N'Bánh sừng bò Pháp giòn rụm với nhân chocolate đậm đà, kết hợp hoàn hảo giữa vỏ bánh bơ và chocolate ngọt ngào.', 40000, 1, '/Content/Images/Products/choco_croissant.jpg', GETDATE());

-- 16. Croissant Matcha
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Croissant Matcha', N'Bánh sừng bò với nhân matcha thơm lừng, vị đắng nhẹ của trà xanh kết hợp với độ bơ của croissant.', 42000, 1, '/Content/Images/Products/matcha_croissant.jpg', GETDATE());

-- 17. Crochi Strawberry
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Crochi Strawberry', N'Bánh crochi nhân dâu tây tươi, vỏ bánh giòn tan và nhân dâu ngọt ngào, thích hợp làm món ăn nhẹ.', 35000, 1, '/Content/Images/Products/CROCHI_Strawberry.png', GETDATE());

-- 18. Crochi Chocolate
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Crochi Chocolate', N'Bánh crochi nhân chocolate đậm đà, kết cấu giòn rụm bên ngoài và mềm mịn bên trong.', 35000, 1, '/Content/Images/Products/CROCHI_choco.png', GETDATE());

-- 19. Crochi Matcha
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Crochi Matcha', N'Bánh crochi nhân matcha thơm lừng, vị trà xanh đặc trưng của Nhật Bản trong từng miếng bánh.', 35000, 1, '/Content/Images/Products/CROCHI_matcha.png', GETDATE());

-- 20. Matcha Crepe
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Matcha Crepe', N'Bánh crepe matcha mỏng mịn với kem matcha thơm ngon, món tráng miệng Nhật Bản tinh tế.', 48000, 1, '/Content/Images/Products/matchaCrepe.png', GETDATE());

-- 21. Crepe Avocado
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Crepe Avocado', N'Bánh crepe độc đáo với nhân bơ tươi, vị béo ngậy tự nhiên của bơ kết hợp với bánh crepe mềm mịn.', 50000, 1, '/Content/Images/Products/crepeOvocado.png', GETDATE());

-- 22. Cookie Matcha
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Cookie Matcha', N'Bánh quy matcha giòn tan với hương vị trà xanh đậm đà, thích hợp làm snack hoặc tráng miệng nhẹ.', 30000, 1, '/Content/Images/Products/CookieMatcha.png', GETDATE());

-- 23. Hong Kong Egg Waffles
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Hong Kong Egg Waffles', N'Bánh waffle trứng Hong Kong đặc trưng với hình dạng tổ ong độc đáo, giòn bên ngoài mềm bên trong.', 45000, 1, '/Content/Images/Products/HongkongEggWaffles.png', GETDATE());

-- 24. Buttermilk Pancake Stack
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Buttermilk Pancake Stack', N'Chồng bánh pancake buttermilk mềm mịn với syrup và bơ, món ăn sáng kiểu Mỹ đầy hấp dẫn.', 55000, 1, '/Content/Images/Products/ButtermilkPancakeStack.png', GETDATE());

-- 25. Cream Brulee Brioche French Toast
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Cream Brulee Brioche French Toast', N'Bánh mì nướng kiểu Pháp brioche với lớp kem brulee đặc biệt, kết hợp giữa bánh mì và tráng miệng.', 65000, 1, '/Content/Images/Products/CreamBruleeBriocheFrenchToasts.png', GETDATE());

-- 26. Sweet Bruleed Citrus French Toast
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Sweet Bruleed Citrus French Toast', N'Bánh mì nướng kiểu Pháp với hương cam chanh tươi mát và lớp đường cháy giòn tan.', 62000, 1, '/Content/Images/Products/SWEET-BRULEED-CITRUS-FRENCH-TOAST-scaled.png', GETDATE());

-- 27. Sweet Dalgona Velvet French Toast
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Sweet Dalgona Velvet French Toast', N'Bánh mì nướng với kem dalgona mềm mịn như nhung, phiên bản hiện đại của món bánh mì nướng truyền thống.', 68000, 1, '/Content/Images/Products/SWEET-DALGONA-VELVET-FRENCH-TOAST-scaled.png', GETDATE());

-- 28. Cloudy Lava Nutella
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Cloudy Lava Nutella', N'Bánh lava Nutella với nhân chocolate nóng chảy bên trong, bề mặt như đám mây trắng mềm mịn.', 58000, 1, '/Content/Images/Products/Cloudy-Lava-Nutella_220k.png', GETDATE());

-- 29. Vegan Granola
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Vegan Granola', N'Granola thuần chay với hạt và trái cây khô, lành mạnh và dinh dưỡng, thích hợp cho người ăn chay.', 35000, 1, '/Content/Images/Products/VeganGranola.png', GETDATE());

-- =============================================
-- NƯỚC UỐNG (CategoryId = 2)
-- =============================================

-- 30. Espresso
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Espresso', N'Cà phê espresso đậm đà nguyên chất, chiết xuất từ hạt cà phê chất lượng cao, hương vị mạnh mẽ và thơm lừng.', 35000, 2, '/Content/Images/Products/Espresso.png', GETDATE());

-- 31. Americano
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Americano', N'Cà phê americano nhẹ nhàng, pha loãng từ espresso với nước nóng, vị đắng nhẹ và thơm mát.', 40000, 2, '/Content/Images/Products/Americano.png', GETDATE());

-- 32. Cappuccino
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Cappuccino', N'Cappuccino truyền thống Ý với espresso, sữa nóng và lớp foam dày, tạo nên hương vị hài hòa.', 50000, 2, '/Content/Images/Products/Capuccino.png', GETDATE());

-- 33. Latte Flat White
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Latte Flat White', N'Flat white kiểu Úc với espresso đậm đà và sữa microfoam mềm mịn, vị cà phê nổi bật hơn latte thông thường.', 52000, 2, '/Content/Images/Products/LatteFlatWhite.png', GETDATE());

-- 34. Caramel Macchiato
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Caramel Macchiato', N'Macchiato caramel ngọt ngào với espresso, sữa nóng và syrup caramel thơm lừng, đầy hấp dẫn.', 55000, 2, '/Content/Images/Products/CaremelMachiato.png', GETDATE());

-- 35. Mocha
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Mocha', N'Mocha chocolate đậm đà kết hợp giữa espresso và chocolate, tạo nên hương vị ngọt đắng hài hòa.', 58000, 2, '/Content/Images/Products/Mocha.png', GETDATE());

-- 36. Vietnamese Milk Coffee
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Vietnamese Milk Coffee', N'Cà phê sữa Việt Nam truyền thống với cà phê phin và sữa đặc ngọt, hương vị đặc trưng của quê hương.', 45000, 2, '/Content/Images/Products/VietnameseMilkCoffee.png', GETDATE());

-- 37. Iced Chocolate
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Iced Chocolate', N'Chocolate đá mát lạnh với kem tươi, thích hợp cho những ngày nóng bức, vị ngọt ngào quyến rũ.', 48000, 2, '/Content/Images/Products/IcedChocolate.png', GETDATE());

-- 38. Matcha Latte
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Matcha Latte', N'Latte matcha Nhật Bản với bột trà xanh cao cấp và sữa nóng, vị đắng nhẹ đặc trưng của matcha.', 55000, 2, '/Content/Images/Products/MatchaLatteS.png', GETDATE());

-- 39. Strawberry Matcha Latte
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Strawberry Matcha Latte', N'Matcha latte kết hợp với hương dâu tây tươi mát, sự pha trộn độc đáo giữa Á và Âu.', 60000, 2, '/Content/Images/Products/StrawMatchaLate.png', GETDATE());

-- 40. Espresso Matcha Latte
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Espresso Matcha Latte', N'Sự kết hợp táo bạo giữa espresso và matcha, tạo nên hương vị đắng đặc biệt và độc đáo.', 62000, 2, '/Content/Images/Products/EspressoMatchaLatte.png', GETDATE());

-- 41. Tiramisu Latte
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Tiramisu Latte', N'Latte với hương vị tiramisu đặc trưng, kết hợp cà phê, mascarpone và cacao, như uống tiramisu lỏng.', 65000, 2, '/Content/Images/Products/TiramissuLatte.png', GETDATE());

-- 42. Tiramisu Matcha Caramel
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Tiramisu Matcha Caramel', N'Đồ uống fusion độc đáo kết hợp tiramisu, matcha và caramel, một trải nghiệm hương vị hoàn toàn mới.', 68000, 2, '/Content/Images/Products/TiramissuMatchaCaremel.png', GETDATE());

-- 43. Soko Mores Latte  
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Soko Mores Latte', N'Latte với hương vị s''mores (marshmallow, chocolate, graham cracker), mang đến cảm giác như đang cắm trại.', 58000, 2, '/Content/Images/Products/Soko''MoresLatte.png', GETDATE());

-- 44. Signature Mocha Brown Sugar Latte
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Signature Mocha Brown Sugar Latte', N'Latte đặc trưng của quán với mocha và đường nâu, hương vị phong phú và đậm đà.', 65000, 2, '/Content/Images/Products/SIGNATURE-MOCHA-BROWN-SUGAR-LATTE.png', GETDATE());

-- 45. Salted Marble Coffee
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Salted Marble Coffee', N'Cà phê marble mặn ngọt với hiệu ứng màu sắc đẹp mắt, vị mặn nhẹ tạo điểm nhấn thú vị.', 55000, 2, '/Content/Images/Products/SaltedMarbleCoffee.png', GETDATE());

-- 46. Salted Cacao Avocado
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Salted Cacao Avocado', N'Đồ uống độc đáo với bơ, cacao và muối biển, mang lại hương vị béo ngậy và lạ miệng.', 58000, 2, '/Content/Images/Products/saltedCaCaoAvocado.png', GETDATE());

-- 47. Matcha Oat
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Matcha Oat', N'Matcha với sữa yến mạch, lành mạnh và thơm ngon, thích hợp cho người ăn chay hoặc không dung nạp lactose.', 52000, 2, '/Content/Images/Products/matchaOatSide.png', GETDATE());

-- 48. Gentle Hojicha
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Gentle Hojicha', N'Trà hojicha nhẹ nhàng với hương vị nướng đặc trưng, ít caffeine và dễ uống.', 48000, 2, '/Content/Images/Products/gentleHojicha.png', GETDATE());

-- 49. Rich Hojicha
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Rich Hojicha', N'Hojicha đậm đà với hương vị nướng mạnh mẽ hơn, dành cho những ai yêu thích vị đặc biệt của trà rang.', 50000, 2, '/Content/Images/Products/richHojicha.png', GETDATE());

-- 50. Oolong Đào Hồng
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Oolong Đào Hồng', N'Trà oolong kết hợp với hương đào hồng tự nhiên, vị ngọt thanh và thơm mát của trái cây.', 45000, 2, '/Content/Images/Products/OlongDaoHong.png', GETDATE());

-- 51. Nước Lựu Đào
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Nước Lựu Đào', N'Nước ép lựu đào tươi mát với nhiều vitamin, vị chua ngọt tự nhiên và màu sắc hấp dẫn.', 42000, 2, '/Content/Images/Products/NuocLuaDao.png', GETDATE());

-- 52. Earl Grey With Boba
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Earl Grey With Boba', N'Trà Earl Grey thơm bergamot kết hợp với trân châu boba dai ngon, phong cách trà sữa hiện đại.', 48000, 2, '/Content/Images/Products/EarlGreyWithBoba.png', GETDATE());

-- 53. Matcha Coco Earl Grey Matcha Latte
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Matcha Coco Earl Grey Matcha Latte', N'Latte fusion phức tạp với matcha, dừa và Earl Grey, tạo nên hương vị đa tầng độc đáo.', 68000, 2, '/Content/Images/Products/MATCHA-COCO-EARL-GREY-MATCHA-LATTE.png', GETDATE());

-- 54. Matcha Vanilla Coco Cold Foam
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Matcha Vanilla Coco Cold Foam', N'Đồ uống matcha với vanilla, dừa và cold foam mịn màng, mát lạnh và thơm ngon.', 65000, 2, '/Content/Images/Products/MATCHA-VANILLA-COCO-MATCHA-COLD-FOAM.png', GETDATE());

-- 55. Matcha Cloud Smoothie
INSERT INTO Product (Name, Description, Price, CategoryId, ImageUrl, CreatedDate) VALUES
(N'Matcha Cloud Smoothie', N'Smoothie matcha mịn như mây với kết cấu nhẹ nhàng, vị matcha đậm đà và mát lạnh.', 58000, 2, '/Content/Images/Products/MATCHA-MATCHA-CLOUD-SMOOTHIE-scaled.png', GETDATE());

-- Hiển thị kết quả
PRINT 'Đã tạo lại tất cả sản phẩm thành công!';
PRINT 'Tổng cộng: 55 sản phẩm';
PRINT '- Bánh tráng miệng: 29 sản phẩm';
PRINT '- Nước uống: 26 sản phẩm';
PRINT '';
PRINT 'Lưu ý: Script sử dụng tên bảng và cột chính xác:';
PRINT '- Bảng: Category (cột Name, không phải CategoryName)';
PRINT '- Bảng: Product (cột Name, không phải ProductName)'; 