﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.System;


// 空白頁項目範本已記錄在 https://go.microsoft.com/fwlink/?LinkId=234238

namespace LiPTT
{
    /// <summary>
    /// 可以在本身使用或巡覽至框架內的空白頁面。
    /// </summary>
    public sealed partial class PttMainPage : Page
    {
        public PttMainPage()
        {
            this.InitializeComponent();

            LiPTT.Frame = PTTFrame;
            LiPTT.Client.Kicked += async (o, e) =>
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                    PTTFrame.Navigate(typeof(LoginPage));
                });   
            };

            LiPTT.KeepAliveTimer = new DispatcherTimer() { Interval = TimeSpan.FromMinutes(5) };
            LiPTT.KeepAliveTimer.Tick += (a, b) => {
                LiPTT.PressKeepAlive();
            };
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (PTTFrame.CurrentSourcePageType != typeof(LoginPage))
            {
                if (!LiPTT.Client.IsConnected)
                    PTTFrame.Navigate(typeof(LoginPage));
            }
        }
    }
}
