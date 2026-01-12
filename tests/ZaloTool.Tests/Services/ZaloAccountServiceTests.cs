using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using src.ZaloTool.Database;
using src.ZaloTool.Models;
using src.ZaloTool.Services;
using Xunit;
using FluentAssertions;

namespace ZaloTool.Tests.Services;

public class ZaloAccountServiceTests
{
    private readonly Mock<IDbContextFactory<ZaloToolContext>> _mockContextFactory;

    public ZaloAccountServiceTests()
    {
        _mockContextFactory = new Mock<IDbContextFactory<ZaloToolContext>>();
    }

    [Fact]
    public async Task GetAllAccountsAsync_ShouldReturnAccounts()
    {
        // Arrange
        // (Simplified for demo, real DB testing should use InMemory or SQLite)
        var options = new DbContextOptionsBuilder<ZaloToolContext>()
            .UseInMemoryDatabase(databaseName: "ZaloTestDb")
            .Options;
        
        using var context = new ZaloToolContext(options);
        context.AccountZalos.Add(new AccountZalo { Id = 1, Name = "Test Account" });
        await context.SaveChangesAsync();

        var factoryMock = new Mock<IDbContextFactory<ZaloToolContext>>();
        factoryMock.Setup(f => f.CreateDbContextAsync(default)).ReturnsAsync(new ZaloToolContext(options));

        var service = new ZaloAccountService(factoryMock.Object);

        // Act
        var result = await service.GetAllAccountsAsync();

        // Assert
        result.Should().HaveCount(1);
        result.First().Name.Should().Be("Test Account");
    }
}
