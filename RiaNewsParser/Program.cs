using RiaNewsParser.HttpRequestServices;
using RiaNewsParser.ParsingServices;
using System;

namespace RiaNewsParser
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //TODO List:
            //0. Executor --?
            //1. Get URL
            //2. Make a request (HTTPRquestHandler)             (Done)
            //3. Parse                                          (Done)
            //3.1 Get Header                                    (Done)
            //3.2 Get Body                                      (Done)
            //3.3 Get Date                                      (Done)
            //3.4 Get Images                                    (Done)
            //3.4a Convert Images to Base64                     (Done)
            //3.5 Get Links
            //4. Make an obj with info
            //5. Serrialize the obj
            //6. Save the serrialized obj

            string url = @"https://ria.ru/20201103/miting-1582793058.html";

            string result = new HttpRequestHandler(url).GetPageSourceCode();
            HtmlParser hp = new HtmlParser(result);
            hp.StartParse();

            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
