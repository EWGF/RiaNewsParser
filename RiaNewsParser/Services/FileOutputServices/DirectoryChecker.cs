using System.IO;


namespace RiaNewsParser.Services.FileOutputServices
{
    /// <summary>
    /// Checks the directory for saving files. Creates a appropriate dirrectory if necessery.
    /// </summary>
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
