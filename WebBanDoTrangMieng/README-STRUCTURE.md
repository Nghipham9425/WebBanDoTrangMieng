# Cấu Trúc Code Website Đồ Tráng Miệng

## 📁 Tổ Chức File CSS

### 1. **Site.css** (File CSS chính)

- Chứa các styles cơ bản và global
- Bootstrap overrides
- Typography và spacing utilities
- Form controls và validation styles

### 2. **layout.css** (CSS cho Layout)

- Header và navigation styles
- Footer styles
- Global layout components
- Color variables (CSS custom properties)
- Responsive design rules

### 3. **auth-modal.css** (CSS cho Modal Đăng Nhập)

- Modal authentication styles
- Form styling trong modal
- Tab switching effects
- Social login buttons

### 4. **components.css** (CSS cho Components)

- Search overlay
- Product cards
- Toast notifications
- Loading states
- Breadcrumb
- Back to top button

### 5. **admin.css** (CSS cho Admin Panel)

- Admin dashboard styles
- Sidebar navigation
- Admin tables và forms
- Charts và statistics components

## 📁 Tổ Chức File JavaScript

### 1. **layout.js** (JavaScript chính cho Layout)

```javascript
// Các module chính:
- LayoutManager: Quản lý tổng thể layout
- SearchManager: Xử lý tìm kiếm
- CartManager: Quản lý giỏ hàng
- NotificationManager: Hiển thị thông báo
- Utils: Các utility functions
```

### 2. **auth-modal.js** (JavaScript cho Modal)

```javascript
// Chức năng:
- AuthManager: Quản lý đăng nhập/đăng ký
- Form validation
- AJAX submissions
- Password toggle
- Tab switching
```

## 🏗️ Cấu Trúc Layout HTML

### Layout chính (\_Layout.cshtml)

```html
<!DOCTYPE html>
<html>
  <head>
    <!-- Meta tags -->
    <!-- CSS includes -->
  </head>
  <body>
    <!-- Top Header -->
    <!-- Main Navigation -->
    <!-- Content Area -->
    <!-- Footer -->
    <!-- JavaScript includes -->
  </body>
</html>
```

## 🎨 Color Scheme (CSS Variables)

```css
:root {
  --matcha-dark: #3a5a40; /* Xanh matcha đậm */
  --matcha-medium: #588157; /* Xanh matcha vừa */
  --matcha-light: #a3b18a; /* Xanh matcha nhạt */
  --matcha-pale: #dad7cd; /* Xanh matcha rất nhạt */
  --cream-light: #f8f4e5; /* Kem nhạt */
  --cream-white: #ffffff; /* Trắng */
  --matcha-darkest: #344e41; /* Xanh matcha đậm nhất */
}
```

## 📱 Responsive Design

### Breakpoints:

- **Mobile**: < 576px
- **Tablet**: 576px - 991px
- **Desktop**: > 992px

### Responsive Components:

- Navigation collapse trên mobile
- Search overlay responsive
- Product grid responsive
- Footer stacking trên mobile

## 🔧 JavaScript Modules

### 1. LayoutManager

```javascript
- init(): Khởi tạo tất cả components
- initNavigation(): Xử lý navigation
- initDropdowns(): Dropdown hover effects
- initSmoothScroll(): Smooth scrolling
```

### 2. SearchManager

```javascript
- toggle(): Mở/đóng search overlay
- createSearchOverlay(): Tạo search modal
- Gợi ý tìm kiếm
```

### 3. CartManager

```javascript
- addToCart(): Thêm sản phẩm vào giỏ
- updateCartCount(): Cập nhật số lượng
```

### 4. NotificationManager

```javascript
- show(): Hiển thị toast notification
- hide(): Ẩn notification
- Hỗ trợ 4 loại: success, error, warning, info
```

## 💡 Best Practices Được Áp Dụng

### 1. **Separation of Concerns**

- HTML: Structure only
- CSS: Styling only
- JavaScript: Behavior only

### 2. **Modular CSS**

- Component-based CSS
- CSS variables cho colors
- Responsive design patterns

### 3. **Modular JavaScript**

- Object-oriented approach
- Namespace pattern
- Event delegation

### 4. **Performance**

- CSS và JS được tách file riêng
- Lazy loading cho non-critical CSS
- Debounced event handlers

### 5. **Maintainability**

- Clear naming conventions
- Comprehensive comments
- Consistent code structure

## 🚀 Cách Sử Dụng

### Thêm CSS mới:

1. Nếu là component mới → thêm vào `components.css`
2. Nếu là layout style → thêm vào `layout.css`
3. Nếu là admin style → thêm vào `admin.css`

### Thêm JavaScript mới:

1. Nếu là layout function → thêm vào `layout.js`
2. Nếu là auth function → thêm vào `auth-modal.js`
3. Nếu là component riêng → tạo file mới

### Responsive Design:

- Luôn mobile-first approach
- Sử dụng CSS Grid/Flexbox
- Test trên nhiều device sizes

## 📋 Checklist Bảo Trì

### Hàng tuần:

- [ ] Kiểm tra console errors
- [ ] Test responsive trên mobile
- [ ] Validate HTML/CSS

### Hàng tháng:

- [ ] Update dependencies
- [ ] Performance audit
- [ ] Code review

### Khi thêm tính năng mới:

- [ ] Follow naming conventions
- [ ] Add appropriate comments
- [ ] Test cross-browser
- [ ] Update documentation

## 🎯 Performance Tips

1. **CSS Optimization**:

   - Minimize CSS nesting
   - Use CSS variables
   - Avoid !important

2. **JavaScript Optimization**:

   - Use event delegation
   - Debounce scroll/resize events
   - Lazy load non-critical code

3. **Images**:
   - Optimize image sizes
   - Use modern formats (WebP)
   - Implement lazy loading

## 🐛 Debug Tips

1. **CSS Issues**:

   - Use browser dev tools
   - Check CSS specificity
   - Validate CSS syntax

2. **JavaScript Issues**:

   - Check console for errors
   - Use debugger statements
   - Test in different browsers

3. **Layout Issues**:
   - Check responsive design
   - Validate HTML structure
   - Test with different content lengths

---

**Tác giả**: AI Assistant  
**Ngày cập nhật**: ${new Date().toLocaleDateString('vi-VN')}  
**Version**: 1.0
