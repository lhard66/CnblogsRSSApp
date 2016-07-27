using CnblogsRSSApp.Common;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CnblogsRSSApp.Models
{
    public class ArticleModel
    {
        public ArticleModel()
        {
            //注册点击文章标题的事件
            GoToContentCommand = new Command(articleURI => {
                var rootFramw = Window.Current.Content as Frame;
                rootFramw.Navigate(typeof(ArticlePage), articleURI);
            });
        }
        public string Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public DateTime PublishedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public Uri Link { get; set; }
        private string _content;
        public string Content
        {
            get
            {
                if (String.IsNullOrEmpty(_content))
                {
                    return "";
                }
                return _content;
            }
            set
            {
                _content = value;
            }
        }
        public AuthorModel Author { get; set; }

        //因为点击的是每一个标题，故应在标题实体中做拓展
        public Command GoToContentCommand { get; set; }
    }
}
