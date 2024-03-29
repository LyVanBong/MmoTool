﻿using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using OpenQA.Selenium;
using ZaloTool.Database;
using ZaloTool.Models;
using ZaloTool.Services;

namespace ZaloTool.ViewModels
{
    public class SendMessagePhoneNumberViewModel : BindableBase
    {
        private bool _isBusy = true;
        private ZaloToolContext _dbZalo = new ZaloToolContext();
        private IChromeBrowserService _chromeBrowserService;

        public SettingSendMessagePhoneNumber Setting { get; set; } = new SettingSendMessagePhoneNumber();
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }
        private bool _isSendingMessage;
        public bool IsSendingMessage
        {
            get { return _isSendingMessage; }
            set { SetProperty(ref _isSendingMessage, value); }
        }
        public ObservableCollection<AccountZalo> AccountZalos { get; set; } = new ObservableCollection<AccountZalo>();

        private DelegateCommand _upPhoneNumberCommand;
        public DelegateCommand UpPhoneNumberCommand =>
            _upPhoneNumberCommand ?? (_upPhoneNumberCommand = new DelegateCommand(ExecuteCommandName));

        public ObservableCollection<PhoneZalo> PhoneZalos { get; set; } = new ObservableCollection<PhoneZalo>();

        private DelegateCommand _upMediaMessageCommand;
        public DelegateCommand UpMediaMessageCommand =>
            _upMediaMessageCommand ?? (_upMediaMessageCommand = new DelegateCommand(ExecuteUpMediaMessageCommand));
        private DelegateCommand<string> _startCommand;
        public DelegateCommand<string> StartCommand =>
            _startCommand ?? (_startCommand = new DelegateCommand<string>((para) => ExecuteStartCommand(para)));

        public SendMessagePhoneNumberViewModel(IChromeBrowserService chromeBrowserService)
        {
            _chromeBrowserService = chromeBrowserService;
            CreateDefaultData();
        }
        private async Task ExecuteStartCommand(string parameter)
        {
            IsSendingMessage = !IsSendingMessage;
            if (parameter == "0")
            {
                IsSendingMessage = false;
            }
            else if (parameter == "1")
            {
                try
                {
                    if (!PhoneZalos.Any())
                    {
                        IsSendingMessage = false;
                        return;
                    }
                    var account = AccountZalos[1];
                    var drive = await _chromeBrowserService.OpenChrome(account.PathChromeProfile);
                    drive.Navigate().GoToUrl("https://id.zalo.me/");
                    await Task.Delay(8000);
                    foreach (var phone in PhoneZalos)
                    {
                        if (!IsSendingMessage) break;
                        var search = drive.FindElement(By.XPath("/html/body/div/div/div[2]/nav/div[2]/div[1]/div[1]/input"));
                        await Task.Delay(1500);
                        search.SendKeys(phone.PhoneNumber);
                        await Task.Delay(1500);
                        var sourceItem = drive.PageSource.Contains("friend-item");
                        if (sourceItem)
                        {
                            var itemZalo = drive.FindElement(By.XPath(
                                "/html/body/div/div/div[2]/nav/div[2]/div[3]/div/div[2]/div/div/div[1]/div/div[1]/div/div/div[2]/div/div[3]/div[1]/div[1]/span"));
                            if (itemZalo != null)
                            {
                                itemZalo.Click();
                                await Task.Delay(1500);
                                var textBox = drive.FindElement(By.XPath(
                                    "/html/body/div/div/div[2]/main/div/article/div[4]/div[3]/div/div/div/div/div[1]/div"));
                                textBox.SendKeys(Setting.Message);
                                await Task.Delay(1500);
                                var send = drive.FindElement(By.XPath(
                                    "/html/body/div/div/div[2]/main/div/article/div[4]/div[3]/div/div/div/div/div[2]/div[5]"));
                                send.Click();
                                phone.Status = "Đã gửi xong";
                            }
                        }
                        else
                        {
                            phone.Status = "SDT chưa đký zalo";
                        }
                        search.Clear();
                        Setting.AmountDone++;
                        await Task.Delay(TimeSpan.FromSeconds(Setting.TimeSleep));
                    }

                    drive.Quit();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error: " + e.Message);
                }

                IsSendingMessage = true;
            }
        }
        private void ExecuteUpMediaMessageCommand()
        {
            IsBusy = false;
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|*.mp4|*.mp3|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == true)
                {
                    Setting.PathMediaMessage = openFileDialog.FileName;
                    MessageBox.Show("tải file thành công");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
            IsBusy = true;
        }
        private void ExecuteCommandName()
        {
            IsBusy = false;
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == true)
                {
                    Setting.PathPhoneNumber = openFileDialog.FileName;
                    var phones = File.ReadAllText(Setting.PathPhoneNumber);
                    if (phones != null)
                    {
                        var ls = phones.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                        if (ls != null && ls.Length > 0)
                        {
                            var num = 0;
                            foreach (var s in ls)
                            {
                                PhoneZalos.Add(new PhoneZalo(s));
                                num++;
                            }
                            Setting.TotalMessage = num;
                            MessageBox.Show("Tải danh sách số điện thoại thành công");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
            IsBusy = true;
        }


        /// <summary>
        /// Khởi tạo dữ liệu ban đầu
        /// </summary>
        /// <returns></returns>
        private Task CreateDefaultData()
        {
            try
            {
                if (_dbZalo.AccountZalos.Any())
                {
                    AccountZalos.AddRange(_dbZalo.AccountZalos);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error");
            }

            IsBusy = true;
            return Task.CompletedTask;
        }
    }
}