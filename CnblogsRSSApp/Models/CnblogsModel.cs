using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CnblogsRSSApp.Models
{
    public class CnblogsModel
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Id { get; set; }
        public DateTime UpdatedTime { get; set; }
        public string Generator { get; set; }
        public List<ArticleModel> ArticleList { get; set; }
    }
}
