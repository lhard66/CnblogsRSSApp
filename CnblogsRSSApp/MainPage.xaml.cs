using System;
using CnblogsRSSApp.ViewModels;
using Windows.UI.Xaml.Controls;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace CnblogsRSSApp
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPageViewModel vmMainPage { get; set; }
        public MainPage()
        {
            vmMainPage = new ViewModels.MainPageViewModel();
            this.InitializeComponent();
        }       

       
    }
}
