using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System;
using RiaNewsParser.HttpRequestServices;
using RiaNewsParser.DataRepresentation;

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

        public void StartParse()
        {
            _htmlElements = ConvertToHtmlDocument(_htmlText).All.Cast<HtmlElement>();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(GetTextContentByClassName("article__title"));

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(GetTextContentByClassName("article__text"));

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(GetTextContentByClassName("article__info-date"));

            Console.ForegroundColor = ConsoleColor.Yellow;
            var res = GetClassElementsByTags("photoview__open", "img").Select(element => element.GetAttribute("src")); //checked

            var articleLinksTest = GetLinksFromArticleText("article__body js-mediator-article mia-analytics", "A");

            foreach (var item in res)
            {
                var a = new HttpRequestHandler(item).GetBase64ImageAsync();
                a.Wait();
                Console.WriteLine(a.Result);
            }
        }

        /// <summary>
        /// Returns HtmlElement collection for all tags form article class
        /// </summary>
        private IEnumerable<HtmlElement> GetClassElementsByTags(string className, string tagName) => _htmlElements.Where(element => element.GetAttribute("className").Equals(className))
                                                                                                                  .FirstOrDefault()
                                                                                                                  ?.GetElementsByTagName(tagName)
                                                                                                                  ?.Cast<HtmlElement>();

        private IEnumerable<Links> GetLinksFromArticleText(string className, string tagName) => GetClassElementsByTags(className, tagName)
                                                                                                .Where(element => !element.GetAttribute("className").Equals("banner__hidden-button"))
                                                                                                .Select(element => new Links()
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
