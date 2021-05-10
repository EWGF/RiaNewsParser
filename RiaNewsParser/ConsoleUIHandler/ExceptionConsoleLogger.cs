using System;

namespace RiaNewsParser.ConsoleUIHandler
{
    public static class ExceptionConsoleLogger
    {
        public static void PrintException(string uiMessage, Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{uiMessage} : {ex.InnerException?.Message}");
            Console.ResetColor();
        }
    }
}
 