# MmoTool Suite

A comprehensive suite of automation tools for marketing and social media management, including tools for Facebook, Zalo, and general scanning functionality.

## 📋 Overview

The MmoTool Suite consists of four main applications:

- **FacebookTool** - Desktop application for Facebook automation and cookie management
- **ZaloTool** - Desktop application for Zalo marketing automation, including friend management and message sending
- **ScannerTool** - Desktop application for scanning and data collection
- **OnlineTool** - Web-based tool built with Blazor WebAssembly for online operations

## 🏗️ Solution Structure

```
MmoTool/
├── src/                          # Source code
│   ├── MmoTool.Shared/          # Shared utilities and services
│   ├── MmoTool.Core/            # Core business logic
│   ├── FacebookTool/            # Facebook automation tool (WPF)
│   ├── ZaloTool/                # Zalo marketing tool (WPF)
│   ├── ScannerTool/             # Scanner tool (WPF)
│   └── OnlineTool/              # Web-based tool (Blazor)
│       ├── Client/              # Blazor WebAssembly client
│       ├── Server/              # ASP.NET Core server
│       └── Shared/              # Shared models
├── tests/                        # Unit and integration tests
├── docs/                         # Documentation
├── Setups/                       # Installation packages
├── Directory.Build.props         # Common build properties
├── Directory.Packages.props      # Central package management
├── .editorconfig                 # Code style configuration
└── MmoTool.sln                   # Visual Studio solution

```

## 🚀 Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (recommended) or Visual Studio Code
- [Google Chrome](https://www.google.com/chrome/) (required for Selenium automation)
- SQL Server (for OnlineTool) or SQLite (for ZaloTool)

## 🔧 Getting Started

### Clone the Repository

```bash
git clone <repository-url>
cd MmoTool
```

### Restore Dependencies

```bash
dotnet restore MmoTool.sln
```

### Build the Solution

```bash
dotnet build MmoTool.sln --configuration Release
```

### Run Individual Projects

#### FacebookTool
```bash
dotnet run --project FacebookTool/FacebookTool.csproj
```

#### ZaloTool
```bash
dotnet run --project ZaloTool/ZaloTool.csproj
```

#### ScannerTool
```bash
dotnet run --project ScannerTool/ScannerTool.csproj
```

#### OnlineTool (Server)
```bash
dotnet run --project OnlineTool/Server/OnlineTool.Server.csproj
```
Then navigate to `https://localhost:7xxx` in your browser.

## 📦 Project Details

### FacebookTool

A WPF desktop application for Facebook automation tasks:
- Cookie extraction and management
- Automated Facebook interactions
- Chrome browser automation using Selenium

**Key Features:**
- MVVM architecture with Prism framework
- Selenium WebDriver integration
- RESTful API communication

### ZaloTool

A WPF desktop application for Zalo marketing automation:
- Account management
- Friend requests via phone numbers
- Automated message sending
- SQLite database for data persistence

**Key Features:**
- Entity Framework Core with SQLite
- Chrome profile management
- Bulk operations support
- MVVM pattern with Prism

### ScannerTool

A WPF desktop application for scanning and data collection:
- Web scraping capabilities
- Data extraction and processing
- Chrome automation

**Key Features:**
- Selenium WebDriver integration
- Customizable scanning functions
- MVVM architecture

### OnlineTool

A modern web application built with Blazor WebAssembly:
- Progressive Web App (PWA) support
- ASP.NET Core Identity for authentication
- RESTful API backend

**Key Features:**
- Blazor WebAssembly client
- ASP.NET Core Web API
- Entity Framework Core with SQL Server
- Identity Server integration

## 🧪 Testing

Run all tests:
```bash
dotnet test MmoTool.sln
```

Run tests for a specific project:
```bash
dotnet test tests/ZaloTool.Tests/ZaloTool.Tests.csproj
```

## 📝 Configuration

Each desktop application uses `appsettings.json` for configuration:

- **FacebookTool**: Chrome driver settings, logging configuration
- **ZaloTool**: Database connection, Chrome profile path, download URLs
- **ScannerTool**: Application-specific settings
- **OnlineTool**: Database connection, authentication settings

Example `appsettings.json`:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=app.db"
  }
}
```

## 🏗️ Architecture

The solution follows modern .NET development practices:

- **MVVM Pattern**: All WPF applications use Model-View-ViewModel pattern
- **Dependency Injection**: Prism framework for DI in desktop apps, built-in DI for web apps
- **Separation of Concerns**: Business logic separated from UI code
- **Repository Pattern**: Data access abstraction
- **Central Package Management**: Consistent package versions across projects

## 🤝 Contributing

Please read [CONTRIBUTING.md](CONTRIBUTING.md) for details on our code of conduct and the process for submitting pull requests.

### Code Style

This project uses EditorConfig for consistent code style. Please ensure your IDE respects the `.editorconfig` file.

Key conventions:
- Use 4 spaces for indentation
- Use PascalCase for public members
- Use camelCase with underscore prefix for private fields (`_fieldName`)
- Interfaces should start with `I` (e.g., `IService`)

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🐛 Known Issues

- Chrome driver version must match installed Chrome browser version
- ZaloTool requires Chrome profile setup on first run

## 📞 Support

For issues and questions:
- Create an issue in the repository
- Check existing documentation in the `docs/` folder

## 🔄 Version History

- **2.0.0** - Major restructuring with .NET 8.0, improved architecture, and shared libraries
- **1.0.0** - Initial release

## 🙏 Acknowledgments

- [Prism Library](https://prismlibrary.com/) - MVVM framework
- [Selenium WebDriver](https://www.selenium.dev/) - Browser automation
- [Entity Framework Core](https://docs.microsoft.com/ef/core/) - ORM
- [RestSharp](https://restsharp.dev/) - HTTP client library
