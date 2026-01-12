# Contributing to MmoTool Suite

Thank you for your interest in contributing to the MmoTool Suite! This document provides guidelines and instructions for contributing to the project.

## 📋 Table of Contents

- [Code of Conduct](#code-of-conduct)
- [Getting Started](#getting-started)
- [Development Workflow](#development-workflow)
- [Coding Standards](#coding-standards)
- [Testing Requirements](#testing-requirements)
- [Pull Request Process](#pull-request-process)
- [Commit Message Guidelines](#commit-message-guidelines)

## 📜 Code of Conduct

- Be respectful and inclusive
- Focus on constructive feedback
- Help others learn and grow
- Maintain professional communication

## 🚀 Getting Started

### Prerequisites

1. Install [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
2. Install [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)
3. Install [Git](https://git-scm.com/)
4. Install [Google Chrome](https://www.google.com/chrome/)

### Setting Up Your Development Environment

1. Fork the repository
2. Clone your fork:
   ```bash
   git clone https://github.com/YOUR-USERNAME/MmoTool.git
   cd MmoTool
   ```
3. Add upstream remote:
   ```bash
   git remote add upstream https://github.com/ORIGINAL-OWNER/MmoTool.git
   ```
4. Restore dependencies:
   ```bash
   dotnet restore
   ```
5. Build the solution:
   ```bash
   dotnet build
   ```

## 🔄 Development Workflow

1. **Create a branch** for your feature or bug fix:
   ```bash
   git checkout -b feature/your-feature-name
   ```
   or
   ```bash
   git checkout -b fix/your-bug-fix
   ```

2. **Make your changes** following the coding standards

3. **Test your changes** thoroughly

4. **Commit your changes** with clear commit messages

5. **Push to your fork**:
   ```bash
   git push origin feature/your-feature-name
   ```

6. **Create a Pull Request** from your fork to the main repository

## 💻 Coding Standards

### General Guidelines

- Follow the `.editorconfig` settings in the repository
- Write clean, readable, and maintainable code
- Add XML documentation comments for public APIs
- Keep methods small and focused (single responsibility)
- Use meaningful variable and method names

### C# Conventions

#### Naming Conventions

- **Classes, Methods, Properties**: PascalCase
  ```csharp
  public class UserService
  {
      public string UserName { get; set; }
      public void ProcessUser() { }
  }
  ```

- **Private Fields**: camelCase with underscore prefix
  ```csharp
  private readonly ILogger _logger;
  private string _userName;
  ```

- **Interfaces**: Start with `I`
  ```csharp
  public interface IUserService { }
  ```

- **Local Variables**: camelCase
  ```csharp
  var userName = "John";
  ```

#### Code Organization

- **Usings**: Place at the top of the file, sorted alphabetically
- **Namespace**: Use file-scoped namespaces (C# 10+)
  ```csharp
  namespace MmoTool.Services;
  
  public class MyService { }
  ```

- **Class Structure**: Order members as follows:
  1. Fields
  2. Constructors
  3. Properties
  4. Methods
  5. Nested types

#### MVVM Pattern (WPF Projects)

- Keep ViewModels clean and focused
- Use commands for user interactions
- Implement `INotifyPropertyChanged` properly
- Don't put business logic in ViewModels - use services

Example:
```csharp
public class MainWindowViewModel : BindableBase
{
    private readonly IUserService _userService;
    private string _userName;

    public string UserName
    {
        get => _userName;
        set => SetProperty(ref _userName, value);
    }

    public DelegateCommand LoadUserCommand { get; }

    public MainWindowViewModel(IUserService userService)
    {
        _userService = userService;
        LoadUserCommand = new DelegateCommand(ExecuteLoadUser);
    }

    private async void ExecuteLoadUser()
    {
        UserName = await _userService.GetUserNameAsync();
    }
}
```

#### Dependency Injection

- Always use constructor injection
- Register services in the appropriate DI container
- Prefer interfaces over concrete types

```csharp
public class MyService
{
    private readonly ILogger<MyService> _logger;
    private readonly IConfiguration _configuration;

    public MyService(ILogger<MyService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }
}
```

#### Async/Await

- Use async/await for I/O operations
- Suffix async methods with `Async`
- Avoid `async void` except for event handlers
- Use `ConfigureAwait(false)` in library code

```csharp
public async Task<User> GetUserAsync(int id)
{
    return await _repository.GetByIdAsync(id).ConfigureAwait(false);
}
```

#### Error Handling

- Use try-catch blocks appropriately
- Log exceptions with context
- Don't swallow exceptions
- Use custom exceptions when appropriate

```csharp
try
{
    await ProcessDataAsync();
}
catch (Exception ex)
{
    _logger.LogError(ex, "Failed to process data for user {UserId}", userId);
    throw;
}
```

## 🧪 Testing Requirements

### Unit Tests

- Write unit tests for all business logic
- Use xUnit as the testing framework
- Use Moq for mocking dependencies
- Use FluentAssertions for assertions
- Aim for at least 80% code coverage

Example:
```csharp
public class UserServiceTests
{
    [Fact]
    public async Task GetUserAsync_WithValidId_ReturnsUser()
    {
        // Arrange
        var mockRepo = new Mock<IUserRepository>();
        mockRepo.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(new User { Id = 1, Name = "John" });
        var service = new UserService(mockRepo.Object);

        // Act
        var result = await service.GetUserAsync(1);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("John");
    }
}
```

### Running Tests

Before submitting a PR, ensure all tests pass:
```bash
dotnet test
```

## 📝 Pull Request Process

1. **Update documentation** if you're changing functionality
2. **Add or update tests** for your changes
3. **Ensure all tests pass** locally
4. **Update the README.md** if needed
5. **Create a descriptive PR title** and description
6. **Link related issues** in the PR description
7. **Request review** from maintainers
8. **Address review feedback** promptly

### PR Title Format

Use conventional commit format:
- `feat: Add new feature`
- `fix: Fix bug in user service`
- `docs: Update README`
- `refactor: Restructure user service`
- `test: Add tests for user service`
- `chore: Update dependencies`

### PR Description Template

```markdown
## Description
Brief description of changes

## Type of Change
- [ ] Bug fix
- [ ] New feature
- [ ] Breaking change
- [ ] Documentation update

## Testing
Describe how you tested your changes

## Checklist
- [ ] Code follows project style guidelines
- [ ] Self-review completed
- [ ] Comments added for complex code
- [ ] Documentation updated
- [ ] Tests added/updated
- [ ] All tests pass
```

## 📋 Commit Message Guidelines

### Format

```
<type>(<scope>): <subject>

<body>

<footer>
```

### Types

- **feat**: New feature
- **fix**: Bug fix
- **docs**: Documentation changes
- **style**: Code style changes (formatting, etc.)
- **refactor**: Code refactoring
- **test**: Adding or updating tests
- **chore**: Maintenance tasks

### Examples

```
feat(ZaloTool): Add bulk message sending feature

Implemented bulk message sending to multiple contacts
with configurable delay between messages.

Closes #123
```

```
fix(FacebookTool): Fix cookie extraction for new Facebook layout

Updated selectors to work with Facebook's new UI layout.
Added fallback selectors for compatibility.

Fixes #456
```

## 🔍 Code Review Guidelines

### For Authors

- Keep PRs focused and small
- Respond to feedback constructively
- Be patient with the review process

### For Reviewers

- Be constructive and respectful
- Focus on code quality and maintainability
- Suggest improvements, don't just criticize
- Approve when ready, request changes when needed

## 📚 Additional Resources

- [.NET Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- [Prism Documentation](https://prismlibrary.com/docs/)
- [Entity Framework Core Documentation](https://docs.microsoft.com/en-us/ef/core/)
- [xUnit Documentation](https://xunit.net/)

## ❓ Questions?

If you have questions about contributing, please:
- Check existing documentation
- Search for similar issues
- Create a new issue with the `question` label

Thank you for contributing to MmoTool Suite! 🎉
