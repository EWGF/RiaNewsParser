﻿using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System;
using RiaNewsParser.HttpRequestServices;

namespace RiaNewsParser.ParsingServices
{
    public class HtmlParser
    {
        private string _htmlText;
        private IEnumerable<HtmlElement> _hElements;

        public HtmlParser(string htmlText)
        {
            _htmlText = htmlText;
        }      

        public void StartParse()
        {
            _hElements = ConvertToHtmlDocument(_htmlText).All.Cast<HtmlElement>();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(GetTextContentByClassName("article__title"));

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(GetTextContentByClassName("article__text"));

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(GetTextContentByClassName("article__info-date"));

            Console.ForegroundColor = ConsoleColor.Yellow;
            var res = GetImageLinksFromElements("photoview__open");

            foreach (var item in res)
            {
                HttpRequestHandler Hr = new HttpRequestHandler(item);
                Console.WriteLine(Hr.GetBase64Image());
            }
            

        }
        
        /// <summary>
        /// Returns URLs for all images form article
        /// </summary>
        private IEnumerable<string> GetImageLinksFromElements(string className) => _hElements.Where(element => element.GetAttribute("className") == className)
                                                                                             .FirstOrDefault()
                                                                                             .GetElementsByTagName("img")
                                                                                             .Cast<HtmlElement>()
                                                                                             .Select(element => element.GetAttribute("src"));

        /// <summary>
        /// Returns text content based on selected class name. Can be used to return the content from several elements.
        /// </summary>
        private string GetTextContentByClassName(string className) => _hElements?.Where(element => element.GetAttribute("className") == className)?
                                                                                .Select(element => element.InnerText)?
                                                                                .Aggregate((f, s) =>  f + s);

        /// <summary>
        /// Converts the string with html-markups to html document.
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
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
