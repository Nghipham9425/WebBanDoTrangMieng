# 🚀 Hướng dẫn Deploy Website Bán Đồ Tráng Miệng

## Phương án 1: Deploy lên IIS (Windows Server)

### 1. Chuẩn bị Server

```bash
# Cài đặt IIS và ASP.NET
- Windows Server 2016/2019/2022
- IIS với ASP.NET 4.7.2
- SQL Server 2016+ hoặc SQL Server Express
```

### 2. Publish Project

```bash
# Trong Visual Studio:
1. Right-click project → Publish
2. Chọn "Folder" → Chọn thư mục publish
3. Configuration: Release
4. Target Framework: .NET Framework 4.7.2
5. Click "Publish"
```

### 3. Cấu hình Database

```sql
-- Tạo database trên server
-- Import file backup hoặc chạy script tạo database
-- Cập nhật connection string trong Web.config
```

### 4. Cấu hình Web.config cho Production

```xml
<!-- Cập nhật connection string -->
<connectionStrings>
  <add name="QLStoreTrangMiengEntities"
       connectionString="metadata=res://*/DessertModel.csdl|res://*/DessertModel.ssdl|res://*/DessertModel.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=YOUR_SERVER;initial catalog=QLStoreTrangMieng;User Id=YOUR_USER;Password=YOUR_PASSWORD;trustservercertificate=True;MultipleActiveResultSets=True;App=EntityFramework&quot;"
       providerName="System.Data.EntityClient" />
</connectionStrings>

<!-- Cập nhật VNPay URLs -->
<appSettings>
  <add key="vnp_Returnurl" value="https://yourdomain.com/Payment/VnpayReturn" />
  <add key="vnp_IpnUrl" value="https://yourdomain.com/Payment/VnpayIpn" />
</appSettings>

<!-- Tắt debug mode -->
<system.web>
  <compilation debug="false" targetFramework="4.7.2" />
</system.web>
```

## Phương án 2: Deploy lên Azure App Service

### 1. Tạo Azure App Service

```bash
# Tạo Resource Group
az group create --name bakery-rg --location "Southeast Asia"

# Tạo App Service Plan
az appservice plan create --name bakery-plan --resource-group bakery-rg --sku B1

# Tạo Web App
az webapp create --resource-group bakery-rg --plan bakery-plan --name bakery-shop-app
```

### 2. Tạo Azure SQL Database

```bash
# Tạo SQL Server
az sql server create --name bakery-sql-server --resource-group bakery-rg --location "Southeast Asia" --admin-user sqladmin --admin-password YourPassword123!

# Tạo Database
az sql db create --resource-group bakery-rg --server bakery-sql-server --name QLStoreTrangMieng --service-objective Basic
```

### 3. Deploy Code

```bash
# Sử dụng Visual Studio
1. Right-click project → Publish
2. Chọn "Azure" → "Azure App Service (Windows)"
3. Chọn subscription và app service đã tạo
4. Publish
```

## Phương án 3: Deploy lên Hosting Provider

### Hosting phù hợp cho ASP.NET:

- **SmarterASP.NET** (khuyến nghị)
- **Hostinger** (có hỗ trợ ASP.NET)
- **GoDaddy**
- **1&1 IONOS**

### Các bước:

1. Mua hosting hỗ trợ ASP.NET 4.7.2
2. Upload files qua FTP
3. Tạo database SQL Server
4. Cấu hình connection string
5. Test website

## Phương án 4: Deploy lên VPS

### VPS Providers:

- **DigitalOcean** ($5/month)
- **Vultr** ($6/month)
- **AWS EC2** (t2.micro free tier)

### Setup:

```bash
# Cài đặt Windows Server
1. Tạo VPS với Windows Server 2019
2. Cài đặt IIS
3. Cài đặt .NET Framework 4.7.2
4. Cài đặt SQL Server Express
5. Deploy như phương án 1
```

## Checklist trước khi Deploy

### ✅ Code

- [ ] Build thành công ở mode Release
- [ ] Tất cả packages đã restore
- [ ] Không có lỗi compile

### ✅ Database

- [ ] Backup database hiện tại
- [ ] Test connection string mới
- [ ] Migrate database nếu cần

### ✅ Configuration

- [ ] Cập nhật connection string
- [ ] Cập nhật VNPay URLs
- [ ] Tắt debug mode
- [ ] Cấu hình email settings

### ✅ Testing

- [ ] Test đăng nhập/đăng ký
- [ ] Test giỏ hàng
- [ ] Test thanh toán VNPay
- [ ] Test admin panel
- [ ] Test gửi email

## Ước tính Chi phí

| Phương án         | Chi phí/tháng | Ưu điểm               | Nhược điểm           |
| ----------------- | ------------- | --------------------- | -------------------- |
| Shared Hosting    | $5-15         | Rẻ, dễ setup          | Hiệu năng hạn chế    |
| VPS               | $10-25        | Toàn quyền kiểm soát  | Cần kiến thức server |
| Azure App Service | $15-50        | Tự động scale, backup | Đắt hơn              |
| Dedicated Server  | $50+          | Hiệu năng cao         | Đắt, cần expertise   |

## Khuyến nghị

**Cho website bán hàng nhỏ:** Bắt đầu với shared hosting ASP.NET

**Cho website có traffic cao:** Sử dụng VPS hoặc Azure App Service

**Cho doanh nghiệp:** Azure App Service với SQL Database
