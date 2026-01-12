using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Prism.Mvvm;
using RestSharp;
using src.ZaloTool.Services;

namespace src.ZaloTool.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IConfiguration _configuration;
        private readonly IZaloAccountService _accountService;
        private string _title = "Zalo Marketing Online";
        private bool _isBusy;
        private string _pathChromeProfileDefault;

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

        public MainWindowViewModel(IConfiguration configuration, IZaloAccountService accountService)
        {
            _configuration = configuration;
            _accountService = accountService;
            
            var profilePath = _configuration["ZaloSettings:ChromeProfilePath"] ?? "ChromeProfile";
            _pathChromeProfileDefault = Path.Combine(Directory.GetCurrentDirectory(), profilePath);
            
            _ = CreateDefaultData();
        }

        private async Task CreateDefaultData()
        {
            try
            {
                if (!Directory.Exists(_pathChromeProfileDefault))
                {
                    IsBusy = true;
                    Directory.CreateDirectory(_pathChromeProfileDefault);
                    
                    var downloadUrl = _configuration["ZaloSettings:DownloadUrl"];
                    if (!string.IsNullOrEmpty(downloadUrl))
                    {
                        var client = new RestClient();
                        var request = new RestRequest(downloadUrl);
                        var response = await client.ExecuteAsync(request);
                        
                        if (response.RawBytes != null)
                        {
                            var filePath = Path.Combine(_pathChromeProfileDefault, "ChromeProfileDefault.zip");
                            await File.WriteAllBytesAsync(filePath, response.RawBytes);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                // In a professional app, avoid MessageBox in VM, use a notification service
                MessageBox.Show("Error initializing data: " + e.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}