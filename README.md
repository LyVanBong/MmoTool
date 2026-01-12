# MmoTool Suite

Bộ công cụ tự động hóa toàn diện cho tiếp thị và quản lý mạng xã hội, bao gồm các công cụ dành cho Facebook, Zalo và các tính năng quét dữ liệu tổng hợp.

## 📋 Tổng quan

MmoTool Suite bao gồm bốn ứng dụng chính:

- **FacebookTool** - Ứng dụng desktop để tự động hóa Facebook và quản lý cookie.
- **ZaloTool** - Ứng dụng desktop để tự động hóa marketing trên Zalo, bao gồm quản lý bạn bè và gửi tin nhắn.
- **ScannerTool** - Ứng dụng desktop để quét và thu thập dữ liệu web.
- **OnlineTool** - Công cụ dựa trên nền tảng web, được xây dựng bằng Blazor WebAssembly cho các hoạt động trực tuyến.

## 🏗️ Cấu trúc Solution

```
MmoTool/
├── src/                          # Mã nguồn
│   ├── MmoTool.Shared/          # Tiện ích và dịch vụ dùng chung
│   ├── MmoTool.Core/            # Logic nghiệp vụ cốt lõi
│   ├── FacebookTool/            # Công cụ tự động hóa Facebook (WPF)
│   ├── ZaloTool/                # Công cụ marketing Zalo (WPF)
│   ├── ScannerTool/             # Công cụ quét dữ liệu (WPF)
│   └── OnlineTool/              # Công cụ nền web (Blazor)
│       ├── Client/              # Blazor WebAssembly client
│       ├── Server/              # ASP.NET Core server
│       └── Shared/              # Models dùng chung cho Web
├── tests/                        # Unit test và Integration tests
├── docs/                         # Tài liệu hướng dẫn
├── Setups/                       # Các gói cài đặt ứng dụng
├── Directory.Build.props         # Cấu hình build chung
├── Directory.Packages.props      # Quản lý package tập trung (CPM)
├── .editorconfig                 # Cấu hình phong cách lập trình (Code style)
└── MmoTool.sln                   # Solution Visual Studio
```

## 🚀 Yêu cầu hệ thống

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) hoặc mới hơn.
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (khuyến nghị) hoặc Visual Studio Code.
- [Google Chrome](https://www.google.com/chrome/) (yêu cầu cho Selenium automation).
- SQL Server (cho OnlineTool) hoặc SQLite (cho ZaloTool).

## 🔧 Hướng dẫn bắt đầu

### Tải mã nguồn

```bash
git clone <repository-url>
cd MmoTool
```

### Khôi phục các thư viện (Dependencies)

```bash
dotnet restore MmoTool.sln
```

### Build Solution

```bash
dotnet build MmoTool.sln --configuration Release
```

### Chạy các dự án

#### FacebookTool
```bash
dotnet run --project src/FacebookTool/FacebookTool.csproj
```

#### ZaloTool
```bash
dotnet run --project src/ZaloTool/ZaloTool.csproj
```

#### ScannerTool
```bash
dotnet run --project src/ScannerTool/ScannerTool.csproj
```

#### OnlineTool (Server)
```bash
dotnet run --project src/OnlineTool/Server/OnlineTool.Server.csproj
```
Sau đó truy cập `https://localhost:7xxx` trên trình duyệt của bạn.

## 📦 Chi tiết các dự án

### FacebookTool
Ứng dụng WPF dành cho các tác vụ tự động hóa Facebook:
- Trích xuất và quản lý cookie.
- Tương tác Facebook tự động.
- Tự động hóa trình duyệt Chrome bằng Selenium.

### ZaloTool
Ứng dụng WPF dành cho marketing trên Zalo:
- Quản lý tài khoản Zalo.
- Gửi lời mời kết bạn qua số điện thoại.
- Gửi tin nhắn hàng loạt.
- Sử dụng SQLite để lưu trữ dữ liệu cục bộ.

### ScannerTool
Ứng dụng WPF để quét và thu thập dữ liệu:
- Khả năng thu thập dữ liệu web (Web scraping).
- Trích xuất và xử lý dữ liệu.
- Tự động hóa Chrome.

### OnlineTool
Ứng dụng web hiện đại dựa trên Blazor WebAssembly:
- Hỗ trợ Progressive Web App (PWA).
- Sử dụng ASP.NET Core Identity để xác thực.
- Backend RESTful API mạnh mẽ.

## 🧪 Kiểm thử (Testing)

Chạy tất cả các test:
```bash
dotnet test MmoTool.sln
```

Chạy test cho một project cụ thể:
```bash
dotnet test tests/ZaloTool.Tests/ZaloTool.Tests.csproj
```

## 📝 Cấu hình

Mỗi ứng dụng desktop sử dụng file `appsettings.json` để cấu hình:
- **FacebookTool**: Cấu hình Chrome driver, log.
- **ZaloTool**: Kết nối Database, đường dẫn Chrome profile, link tải dữ liệu.
- **ScannerTool**: Các thiết lập riêng cho việc quét dữ liệu.

## 🏗️ Kiến trúc

Solution tuân thủ các tiêu chuẩn phát triển .NET hiện đại:
- **Mô hình MVVM**: Tất cả ứng dụng WPF sử dụng Model-View-ViewModel.
- **Dependency Injection**: Sử dụng Prism cho desktop và DI mặc định cho web.
- **Phân tách trách nhiệm**: Tách biệt logic nghiệp vụ khỏi mã giao diện.
- **Quản lý Package tập trung**: Đảm bảo phiên bản thư viện đồng nhất.

## 🤝 Đóng góp

Vui lòng đọc file [CONTRIBUTING.md](CONTRIBUTING.md) để biết chi tiết về quy tắc ứng xử và quy trình gửi Pull Request.

## 📄 Bản quyền

Dự án này được cấp phép theo giấy phép MIT - xem file [LICENSE](LICENSE) để biết chi tiết.

## 🔄 Lịch sử phiên bản

- **2.0.0** - Tái cấu trúc lớn với .NET 8.0, chuẩn hóa kiến trúc doanh nghiệp và thư viện dùng chung.
- **1.0.0** - Phiên bản phát hành đầu tiên.
