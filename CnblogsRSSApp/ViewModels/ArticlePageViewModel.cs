using CnblogsRSSApp.Models;
using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CnblogsRSSApp.ViewModels
{
    public class ArticlePageViewModel
    {
        public ArticlePageViewModel(Uri link)
        {
            articleModel = new Models.ArticleModel() { Link=link};
        }
        public ArticleModel articleModel { get; set; }

        
    }
}
