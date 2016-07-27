using CnblogsRSSApp.Models;
using CnblogsRSSApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace CnblogsRSSApp
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ArticlePage : Page
    {
        public ArticlePage()
        {
            this.InitializeComponent();
        }
        public ArticlePageViewModel vmArticlePage { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ShowArticle(e.Parameter as Uri);
        }
        //TODO: 以下代码不符合mvvm设计思想，但因为webview没有html字符串绑定属性，还有采用从后端返回json后前端操作，故采用此方式实现。
        private async void ShowArticle(Uri uri)
        {
            vmArticlePage = new ViewModels.ArticlePageViewModel(uri);
            webArticle.NavigateToString(await LoadedArticle());
        }
        public async Task<string> LoadedArticle()
        {
            HttpClient client = new HttpClient();
            string articleContent = await client.GetStringAsync(vmArticlePage.articleModel.Link.ToString());
            Regex regex = new Regex(@"<div id=""post_detail"">[\s\S]*<a href=""#"" onclick=""AddToWz\(\d+\);return false;"">收藏</a>");
            Match match = regex.Match(articleContent);
            return match.Value + "</div>";
        }
    }
}
