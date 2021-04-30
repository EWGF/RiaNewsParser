using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiaNewsParser
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO List:
            //0. Executor --?
            //1. Get URL (from GUI?)
            //2. Make a request (HTTPRquestHandler)
            //3. Parse
                //3.1 Get Header
                //3.2 Get Body
                //3.3 Get Date
                //3.4 Get Images
                    //3.4a Convert Images to Base64
                //3.5 Get Links
            //4. Make an obj with info
            //5. Serrialize the obj
            //6. Save the serrialized obj

            string url = @"https://ria.ru/20201103/miting-1582793058.html";
            

            Console.WriteLine(new HttpRequestHandler(url).GetPageSourceCode());
            Console.ReadLine();
        }
    }
}
