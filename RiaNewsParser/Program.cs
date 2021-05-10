using RiaNewsParser.ConsoleUIHandler;
using System;

namespace RiaNewsParser
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //string url = @"https://ria.ru/20201103/miting-1582793058.html";
            InputHandler exec = new InputHandler();
            exec.Run();
        }
    }
}
