using System;
using System.Linq;
using RiaNewsParser.DataRepresentation;
using RiaNewsParser.FileOutputServices;
using RiaNewsParser.HttpRequestServices;
using RiaNewsParser.ParsingServices;

namespace RiaNewsParser
{
    public class Executor
    {
        public Article ParsedArticle { get; set; }
        public string ArticleUrl { get; private set; }
        public string FileSavePath { get; private set; }
        public string DBSavePath { get; private set; }

        public void SaveDB()
        {
            throw new NotImplementedException();
        }

        public void SaveArticle()
        {
            FileSaveHandler fs = new FileSaveHandler();

            fs.SaveArticleJSON(FileSavePath, ParsedArticle);
            fs.SaveArticleXML(FileSavePath, ParsedArticle);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Article saved to {FileSavePath}!");
        }

        public void SetDBPath()
        {
            throw new NotImplementedException();
        }

        public void SetUrl()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Paste the required article URL:");
            ArticleUrl = Console.ReadLine();
            Console.WriteLine($"Article URL succsessfully set to: {ArticleUrl}");
        }

        public void StartParse()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Parsing has been started.");

            var result = new HttpRequestHandler(ArticleUrl).GetPageSourceCodeAsync().Result;
            HtmlParser hp = new HtmlParser(result);
            ParsedArticle = hp.ParseArticle();

            Console.WriteLine($"{ParsedArticle.AtricleTitle}" +
                              $"\n{ParsedArticle.ArticleText}" +
                              $"\n{ParsedArticle.PublicationDate}" +
                              $"\nLinks count: {ParsedArticle.ArticleLinks}");
        }
   
        public void SetSavePath()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Paste the required path to save articles:");
            FileSavePath = Console.ReadLine();
            Console.WriteLine($"Save path succsessfully set to: {FileSavePath}");
        }

        public void PrintHelp()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Command list" +
                              $"\n-help : prints a list of commands" +
                              $"\n-setURL : sets an article URL to parse from" +
                              $"\n-setSavePath : sets a path for saving articles" +
                              $"\n-start : to start parsing" +
                              $"\n-save : to save the parsed article" +
                              $"\n-exit : to qiut the programm\n");
        }
    }
}
