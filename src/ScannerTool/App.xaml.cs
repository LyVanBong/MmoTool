using System.IO;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Prism.DryIoc;
using Prism.Ioc;
using MmoTool.ScannerTool.Views;

namespace MmoTool.ScannerTool;

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

        // Logging
        containerRegistry.RegisterInstance<ILoggerFactory>(LoggerFactory.Create(builder => builder.AddConsole()));
        containerRegistry.Register(typeof(ILogger<>), typeof(Logger<>));

        // Views
        containerRegistry.RegisterForNavigation<MainWindow, ViewModels.MainWindowViewModel>();
    }
}