using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiaNewsParser.DataRepresentation
{
    class Article
    {
        public string AtricleTitle { get; set; }
        public string ArticleText { get; set; }
        public string PublicationDate { get; set; }
        public IEnumerable<string> ArticleImages { get; set; }
        public IEnumerable<Links> ArticleLinks { get; set; }
    }
}
