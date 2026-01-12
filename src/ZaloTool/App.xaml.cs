using System.IO;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Prism.DryIoc;
using Prism.Ioc;
using MmoTool.ZaloTool.Database;
using MmoTool.ZaloTool.Services;
using src.ZaloTool.Views;

namespace MmoTool.ZaloTool;

public partial class App : PrismApplication
{
    private IConfiguration? _configuration;

    protected override void OnStartup(StartupEventArgs e)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        _configuration = builder.Build();

        base.OnStartup(e);
    }

    protected override Window CreateShell()
    {
        return Container.Resolve<MainWindow>();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        // Configuration
        containerRegistry.RegisterInstance<IConfiguration>(_configuration!);

        // Database
        var connectionString = _configuration?.GetConnectionString("DefaultConnection") ?? "Data Source=ZaloToolDb.db";
        
        var services = new ServiceCollection();
        services.AddPooledDbContextFactory<ZaloToolContext>(options =>
        {
            options.UseSqlite(connectionString);
            options.UseLazyLoadingProxies();
        });

        // Register the factory in Prism's container
        var sp = services.BuildServiceProvider();
        containerRegistry.RegisterInstance(sp.GetRequiredService<IDbContextFactory<ZaloToolContext>>());

        // Services
        containerRegistry.RegisterSingleton<IZaloAccountService, ZaloAccountService>();
        containerRegistry.RegisterSingleton<IChromeBrowserService, ChromeBrowserService>();
        
        // Logging (Simulated for now, can add Serilog later)
        containerRegistry.RegisterInstance<ILoggerFactory>(LoggerFactory.Create(builder => builder.AddConsole()));
        containerRegistry.Register(typeof(ILogger<>), typeof(Logger<>));

        // Views
        containerRegistry.RegisterForNavigation<MainWindow, ViewModels.MainWindowViewModel>();
        containerRegistry.RegisterForNavigation<AccountView, ViewModels.AccountViewModel>();
    }
}