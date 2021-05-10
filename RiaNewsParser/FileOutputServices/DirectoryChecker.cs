using System.IO;


namespace RiaNewsParser.FileOutputServices
{
    public static class DirectoryChecker
    {
        public static void CheckDirectory( string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
