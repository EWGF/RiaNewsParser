using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiaNewsParser.DataRepresentation
{
    public class Article
    {
        public string AtricleTitle { get; set; }
        public string ArticleText { get; set; }
        public string PublicationDate { get; set; }
        public List<string> ArticleImages { get; set; }
        public List<Link> ArticleLinks { get; set; }
    }
}
