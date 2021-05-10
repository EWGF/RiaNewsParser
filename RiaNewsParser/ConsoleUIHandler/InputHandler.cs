using System;

namespace RiaNewsParser.ConsoleUIHandler
{
    /// <summary>
    /// Handles the console input commands
    /// </summary>
    public class InputHandler
    {
        public InputHandler()
        {
            exec = new Executor();
        }
        private Executor exec;
        private AppState currentState;
        
        public void Run()
        {
            Console.WriteLine("Welcome to RiaNewsParser! Type '-help' for a list of commands. ");
            while (currentState == AppState.Running)
            {
                ParseInput(Console.ReadLine());
            }
            
            Console.WriteLine("The parser has been stopped, press any key to close the programm.");
            Console.ReadKey();
        }

        private void Exit()
        {
            currentState = AppState.Stopped;
        }

        private void ParseInput(string line)
        {
            switch (line)
            {
                case "-help":
                    exec.PrintHelp();
                    break;
                case "-parse":
                    exec.StartParse();
                    break;
                case "-setUrl":
                    exec.SetUrl();
                    break;
                case "-setSavePath":
                    exec.SetSavePath();
                    break;
                case "-setDBPath":
                    exec.SetDBPath();
                    break;
                case "-setDBName":
                    exec.SetDBName();
                    break;
                case "-setDBTable":
                    exec.SetDBTable();
                    break;
                case "-save":
                    exec.Save();
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
