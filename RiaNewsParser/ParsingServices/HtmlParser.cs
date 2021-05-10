using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System;
using RiaNewsParser.HttpRequestServices;
using RiaNewsParser.DataRepresentation;
using RiaNewsParser.ConversionServices;

namespace RiaNewsParser.ParsingServices
{
    public class HtmlParser
    {
        private string _htmlText;
        private IEnumerable<HtmlElement> _htmlElements;

        public HtmlParser(string htmlText)
        {
            _htmlText = htmlText;
        }

        public Article ParseArticle()
        {
            _htmlElements = ConvertToHtmlDocument(_htmlText).All.Cast<HtmlElement>();
            var article = new Article();

            article.AtricleTitle = GetTextContentByClassName(ParserSettings.ArticleTitleParseTag);
            article.ArticleText = GetTextContentByClassName(ParserSettings.ArticleTextParseTag);
            article.PublicationDate = GetTextContentByClassName(ParserSettings.ArticlePublicationDateParseTag);
            article.ArticleLinks = GetLinksFromArticleText(ParserSettings.ArticleLinksParseTag, "A").ToList();
            article.ArticleImages = ImageConvertService.GetBase64Images(GetClassElementsByTags(ParserSettings.ArticleImagesParseTag, "img").Select(element => element.GetAttribute("src")));
            return article;
        }

        /// <summary>
        /// Returns HtmlElement collection for all tags form article class
        /// </summary>
        private IEnumerable<HtmlElement> GetClassElementsByTags(string className, string tagName) => _htmlElements.Where(element => element.GetAttribute("className").Equals(className))
                                                                                                                  .FirstOrDefault()
                                                                                                                  ?.GetElementsByTagName(tagName)
                                                                                                                  ?.Cast<HtmlElement>();

        private IEnumerable<Link> GetLinksFromArticleText(string className, string tagName) => GetClassElementsByTags(className, tagName)
                                                                                                .Where(element => !element.GetAttribute("className").Equals("banner__hidden-button"))
                                                                                                .Select(element => new Link()
                                                                                                {
                                                                                                    LinkUrl = element.GetAttribute("HREF"),
                                                                                                    LinkName = element.InnerText
                                                                                                });

        /// <summary>
        /// Returns text content based on selected class name. Can be used to return the content from several elements.
        /// </summary>
        private string GetTextContentByClassName(string className) => _htmlElements.Where(element => element.GetAttribute("className").Equals(className))
                                                                                    .Select(element => element.InnerText)
                                                                                    .Aggregate((f, s) => f + s);

        /// <summary>
        /// Converts the string with html-markups to html document.
        /// </summary>
        private HtmlDocument ConvertToHtmlDocument(string html)
        {
            using (WebBrowser browser = new WebBrowser())
            {
                browser.ScriptErrorsSuppressed = true;
                browser.DocumentText = html;
                browser.Document.OpenNew(true);
                browser.Document.Write(html);
                browser.Refresh();
                return browser.Document;
            }
        }
    }
}
