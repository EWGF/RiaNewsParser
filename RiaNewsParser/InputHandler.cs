using System;

namespace RiaNewsParser
{
    public class InputHandler
    {
        public InputHandler()
        {
            exec = new Executor();
        }
        private Executor exec;
        private ParserState currentState;
        
        public void Run()
        {
            Console.WriteLine("Welcome to RiaNewsParser! Type '-help' for a list of commands. ");
            while (currentState == ParserState.Running)
            {
                ParseInput(Console.ReadLine());
            }
            
            Console.WriteLine("The parser has been stopped, press any key to close the programm.");
            Console.ReadKey();
        }

        private void Exit()
        {
            currentState = ParserState.Stopped;
        }

        private void ParseInput(string line)
        {
            switch (line)
            {
                case "-help":
                    exec.PrintHelp();
                    break;
                case "-start":
                    exec.StartParse();
                    break;
                case "-setURL":
                    exec.SetUrl();
                    break;
                case "-setSavePath":
                    exec.SetSavePath();
                    break;
                case "-setDBPath":
                    exec.SetDBPath();
                    break;
                case "-saveArticle":
                    exec.SaveArticle();
                    break;
                case "-saveDB":
                    exec.SaveDB();
                    break;
                case "-exit":
                    Exit();
                    break;
                default:
                    Console.WriteLine("unnknown command.");
                    break;
            }
        }
    }
}
