using CnblogsRSSApp.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Web.Syndication;

namespace CnblogsRSSApp.ViewModels
{
    public class MainPageViewModel: BaseViewModel
    {
        public MainPageViewModel()
        {
            VmCnblogsHome = new ObservableCollection<Models.CnblogsModel>();
            //TODO: 点击对应栏目后实例化此对象
            VmCnblogsEssence = new ObservableCollection<Models.CnblogsModel>();
            VmCnblogsCandidate = new ObservableCollection<Models.CnblogsModel>();
            VmCnblogsNews = new ObservableCollection<Models.CnblogsModel>();
            InitializeData();
            
        }
        public ObservableCollection<CnblogsModel> VmCnblogsHome { get; set; }
        public ObservableCollection<CnblogsModel> VmCnblogsEssence { get; set; }
        public ObservableCollection<CnblogsModel> VmCnblogsCandidate { get; set; }
        public ObservableCollection<CnblogsModel> VmCnblogsNews { get; set; }

        

        private bool _loadingStatus;
        public bool LoadingStatus
        {
            get { return _loadingStatus; }
            set
            {
                SetProperty<bool>(ref _loadingStatus, value);
            }
        }

        private async void InitializeData()
        {
            LoadingStatus = true;
            SyndicationFeed feedHome = await LoadedFeed(new Uri("http://feed.cnblogs.com/blog/sitehome/rss"));
            if (feedHome == null) { return; }
            LoadedModel(feedHome,VmCnblogsHome);
            LoadingStatus = false;
            //TODO: 暂时先启动时加载全部数据
            SyndicationFeed feedEssence = await LoadedFeed(new Uri("http://feed.cnblogs.com/blog/picked/rss"));
            LoadedModel(feedEssence, VmCnblogsEssence);
            SyndicationFeed feedCandidate = await LoadedFeed(new Uri("http://feed.cnblogs.com/blog/candidate/rss"));
            LoadedModel(feedCandidate, VmCnblogsCandidate);

            //SyndicationFeed feedNews = await LoadedFeed(new Uri("http://feed.cnblogs.com/news/rss"));
            //LoadedNewsModel(feedNews, VmCnblogsNews);
        }

        private void LoadedModel(SyndicationFeed feed, ObservableCollection<CnblogsModel> vmCnblogsObj)
        {
            vmCnblogsObj.Add(new CnblogsModel()
            {
                Title = feed.Title.Text,
                Subtitle = feed.Subtitle.Text,
                UpdatedTime = feed.LastUpdatedTime.DateTime,
                ArticleList = feed.Items.Select(c => new ArticleModel()
                {
                    Id=c.Id,
                    Title = c.Title.Text,
                    PublishedTime = c.PublishedDate.DateTime,
                    UpdatedTime = c.LastUpdatedTime.DateTime,
                    Link = c.Links[0].Uri,
                    Content = c.Content.Text,
                    Author = new AuthorModel()
                    {
                        Name = c.Authors[0].Name,
                        Uri = c.Authors[0].Uri
                    }
                }).ToList()
            });
        }
        private void LoadedNewsModel(SyndicationFeed feed, ObservableCollection<CnblogsModel> vmCnblogsObj)
        {
            vmCnblogsObj.Add(new CnblogsModel()
            {
                Title = feed.Title.Text,
                UpdatedTime = feed.LastUpdatedTime.DateTime,                
                ArticleList = feed.Items.Select(c => new ArticleModel()
                {
                    Title = c.Title.Text,
                    UpdatedTime=c.PublishedDate.DateTime,
                    Link = c.Links[0].Uri                 
                }).ToList()
            });
        }

        private async Task<SyndicationFeed> LoadedFeed(Uri uri)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.TryParseAdd("lhard");
            var rssXml = await client.GetStringAsync(uri);
            SyndicationFeed feed = new SyndicationFeed();
            feed.Load(rssXml);
            return feed;
        }

    }
}
