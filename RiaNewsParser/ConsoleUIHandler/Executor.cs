using System;
using RiaNewsParser.DataRepresentation;
using RiaNewsParser.Services.DBServices;
using RiaNewsParser.Services.FileOutputServices;
using RiaNewsParser.Services.HttpRequestServices;
using RiaNewsParser.Services.ParsingServices;

namespace RiaNewsParser.ConsoleUIHandler
{
    /// <summary>
    /// Executes the console commands from InputHandler
    /// </summary>
    public class Executor
    {
        public Article ParsedArticle { get; set; }
        public string ArticleUrl { get; private set; }

        /// <summary>
        /// Saves an article to database
        /// </summary>
        public void SaveDB()
        {
            try
            {
                DBAcessHandler dBAcessHandler = new DBAcessHandler();
                dBAcessHandler.SaveArticle(ParsedArticle);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Article saved to Database: {AppSettings.DBSavePath}!");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                ExceptionConsoleLogger.PrintException("Database saving exception: ", ex);
            }
        }

        /// <summary>
        /// Saves an article to disk
        /// </summary>
        public void Save()
        {
            try
            {
                ArticleSaveHandler fs = new ArticleSaveHandler(ParsedArticle, AppSettings.FileSavePath);

                fs.SaveArticle();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Article saved to {AppSettings.FileSavePath}!");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                ExceptionConsoleLogger.PrintException("File saving exception:", ex);
            }
        }

        /// <summary>
        /// Sets an appropriate path for database
        /// </summary>
        public void SetDBPath()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Paste the required path to save data dase:");

            AppSettings.DBSavePath = Console.ReadLine();

            Console.WriteLine($"Data base path succsessfully set to: {AppSettings.DBSavePath}");
            Console.ResetColor();
        }

        /// <summary>
        /// Sets an approptiate name for database
        /// </summary>
        public void SetDBName()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Paste the required name for data base:");

            AppSettings.DBName = Console.ReadLine();

            Console.WriteLine($"Data base name succsessfully set to: {AppSettings.DBName}");
            Console.ResetColor();
        }

        /// <summary>
        /// Sets an appropriate name for database table
        /// </summary>
        public void SetDBTable()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Paste the required name for DB table:");

            AppSettings.DBTableName = Console.ReadLine();

            Console.WriteLine($"Table name succsessfully set to: {AppSettings.DBTableName}");
            Console.ResetColor();
        }

        /// <summary>
        /// Sets an URL for aricle
        /// </summary>
        public void SetUrl()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Paste the required URL:");

            ArticleUrl = Console.ReadLine();

            Console.WriteLine($"Article URL succsessfully set to: {ArticleUrl}");
            Console.ResetColor();
        }

        /// <summary>
        /// Starts parsing an article
        /// </summary>
        public void StartParse()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Parsing has been started.");
            try
            {
                var result = new HttpRequestHandler(ArticleUrl).GetPageSourceCodeAsync().Result;
                HtmlParser hp = new HtmlParser(result);
                ParsedArticle = hp.ParseArticle();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Article parsed! Title: {ParsedArticle.AtricleTitle}; Symbols count: {ParsedArticle.ArticleText.Length}");
                Console.ResetColor();
            }
            catch(Exception ex)
            {
                ExceptionConsoleLogger.PrintException("Parsing exception:", ex);
            }
        }

        /// <summary>
        /// Sets an appropriate path to save the article files on disk
        /// </summary>
        public void SetSavePath()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Paste the required path to save articles:");

            AppSettings.FileSavePath = Console.ReadLine();

            Console.WriteLine($"Save path succsessfully set to: {AppSettings.FileSavePath}");
            Console.ResetColor();
        }

        /// <summary>
        /// Prints help to console
        /// </summary>
        public void PrintHelp()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Command list" +
                              $"\n-help : prints a list of commands" +
                              $"\n-setUrl : sets an article URL to parse from" +
                              $"\n-setSavePath : sets a path for saving articles; current: [{AppSettings.FileSavePath}]" +
                              $"\n-setDBPath : sets a path for data base; current: [{AppSettings.DBSavePath}]" +
                              $"\n-setDBName : sets a name for data base; current: [{AppSettings.DBName}]" +
                              $"\n-setDBTable : sets a name for data base table; current: [{AppSettings.DBTableName}]" +
                              $"\n-parse : to start parsing" +
                              $"\n-save : to save the parsed article" +
                              $"\n-saveDB : to save the parsed article in Data Base" +
                              $"\n-exit : to qiut the programm\n");
            Console.ResetColor();
        }
    }
}
 