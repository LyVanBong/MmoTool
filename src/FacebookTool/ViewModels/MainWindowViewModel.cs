using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Prism.Commands;
using Prism.Mvvm;
using MmoTool.FacebookTool.Models;
using MmoTool.FacebookTool.Services;

namespace MmoTool.FacebookTool.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IFacebookService _facebookService;
        private readonly ILogger<MainWindowViewModel> _logger;
        private readonly IConfiguration _configuration;

        private string _title = "Facebook Tool Automation";
        private bool _isBusy;
        private string _statusText = "Ready";

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public string StatusText
        {
            get => _statusText;
            set => SetProperty(ref _statusText, value);
        }

        public DelegateCommand RunAppCommand { get; }
        public DelegateCommand GetCookieCommand { get; }

        public MainWindowViewModel(IFacebookService facebookService, ILogger<MainWindowViewModel> logger, IConfiguration configuration)
        {
            _facebookService = facebookService;
            _logger = logger;
            _configuration = configuration;

            RunAppCommand = new DelegateCommand(async () => await ExecuteRunApp());
            GetCookieCommand = new DelegateCommand(async () => await ExecuteGetCookie());
        }

        private async Task ExecuteRunApp()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                StatusText = "Running automation...";
                await _facebookService.RunAutomationAsync();
                StatusText = "Automation completed successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during Facebook automation");
                StatusText = "Error: " + ex.Message;
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task ExecuteGetCookie()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                StatusText = "Extracting cookies...";
                var cookies = await _facebookService.GetCookiesAsync();
                StatusText = "Cookies extracted.";
                _logger.LogInformation("Extracted cookies: {Cookies}", cookies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error extracting cookies");
                StatusText = "Error: " + ex.Message;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}