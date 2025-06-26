-- ==========================================
-- CẬP NHẬT DANH MỤC THÀNH 2 DANH MỤC CHÍNH
-- ==========================================

USE QLStoreTrangMieng;

-- Xóa các danh mục cũ và tạo mới
DELETE FROM Category;
DBCC CHECKIDENT ('Category', RESEED, 0);

-- Thêm 2 danh mục chính
INSERT INTO Category (Name, Description) VALUES 
(N'Bánh Tráng Miệng', N'Tất cả các loại bánh ngọt và tráng miệng'),
(N'Nước Uống', N'Tất cả các loại đồ uống');

-- Cập nhật tất cả sản phẩm bánh (CategoryId 1-5) thành CategoryId = 1
UPDATE Product 
SET CategoryId = 1 
WHERE CategoryId IN (1, 2, 3, 4, 5);

-- Cập nhật tất cả sản phẩm nước uống (CategoryId 6-10) thành CategoryId = 2  
UPDATE Product 
SET CategoryId = 2 
WHERE CategoryId IN (6, 7, 8, 9, 10);

PRINT N'Đã cập nhật thành 2 danh mục chính!';

-- Kiểm tra kết quả
SELECT * FROM Category ORDER BY CategoryId;

SELECT CategoryId, COUNT(*) as ProductCount, 
       CASE CategoryId 
           WHEN 1 THEN N'Bánh Tráng Miệng'
           WHEN 2 THEN N'Nước Uống'
           ELSE N'Khác'
       END as CategoryName
FROM Product 
GROUP BY CategoryId 
ORDER BY CategoryId; 