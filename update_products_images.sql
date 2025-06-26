-- ==========================================
-- UPDATE PRODUCTS WITH MULTIPLE IMAGES
-- ==========================================

USE QLStoreTrangMieng;

-- Cập nhật Tiramisu với multiple images
UPDATE Product 
SET ImageUrl = '/Content/Images/Products/tiramisu_1.jpg,/Content/Images/Products/tiramisu_2.jpg,/Content/Images/Products/OriginalTiramisu.png'
WHERE Name LIKE N'%Tiramisu%' AND ProductId = 3;

UPDATE Product 
SET ImageUrl = '/Content/Images/Products/matchamisu_1.jpg,/Content/Images/Products/matchamisu_2.jpg,/Content/Images/Products/MelonTiramisu.png'
WHERE Name LIKE N'%Tiramisu%' AND ProductId = 4;

-- Cập nhật Cheesecake với multiple images
UPDATE Product 
SET ImageUrl = '/Content/Images/Products/strawBerrytiramisu.png,/Content/Images/Products/CROCHI_Strawberry.png,/Content/Images/Products/merryBerry_1.jpg'
WHERE Name LIKE N'%Cheesecake%' AND ProductId = 5;

UPDATE Product 
SET ImageUrl = '/Content/Images/Products/choco_cake_1.jpg,/Content/Images/Products/CROCHI_choco.png,/Content/Images/Products/SaltedChocolate.png'
WHERE Name LIKE N'%Chocolate%' AND ProductId = 6;

-- Cập nhật Pudding với multiple images  
UPDATE Product 
SET ImageUrl = '/Content/Images/Products/MousseXoai_1.jpg,/Content/Images/Products/MousseXoai_2.jpg,/Content/Images/Products/crepeOvocado.png'
WHERE Name LIKE N'%Xoài%' AND ProductId = 7;

UPDATE Product 
SET ImageUrl = '/Content/Images/Products/mousseVai_1.jpg,/Content/Images/Products/mousseVai_2.jpg,/Content/Images/Products/mousseVai_3.jpg'
WHERE Name LIKE N'%Dâu%' AND ProductId = 8;

-- Cập nhật Bánh Flan
UPDATE Product 
SET ImageUrl = '/Content/Images/Products/flan_caramel.png,/Content/Images/Products/ButterScotchBrulee.png,/Content/Images/Products/CreamBruleeBriocheFrenchToasts.png'
WHERE Name LIKE N'%Flan%' AND ProductId = 1;

-- Cập nhật các sản phẩm nước uống
UPDATE Product 
SET ImageUrl = '/Content/Images/Products/MatchaLatteS.png,/Content/Images/Products/MATCHA-COCO-EARL-GREY-MATCHA-LATTE.png,/Content/Images/Products/StrawMatchaLate.png'
WHERE Name LIKE N'%Trà Sữa Matcha%' AND ProductId = 12;

UPDATE Product 
SET ImageUrl = '/Content/Images/Products/Capuccino.png,/Content/Images/Products/Espresso.png,/Content/Images/Products/EspressoMatchaLatte.png'
WHERE Name LIKE N'%Cappuccino%' AND ProductId = 15;

UPDATE Product 
SET ImageUrl = '/Content/Images/Products/LatteFlatWhite.png,/Content/Images/Products/TiramissuLatte.png,/Content/Images/Products/SIGNATURE-MOCHA-BROWN-SUGAR-LATTE.png'
WHERE Name LIKE N'%Latte%' AND ProductId = 16;

PRINT N'Đã cập nhật multiple images cho các sản phẩm!';

-- Kiểm tra kết quả
SELECT ProductId, Name, ImageUrl 
FROM Product 
WHERE ImageUrl LIKE '%,%'
ORDER BY ProductId; 