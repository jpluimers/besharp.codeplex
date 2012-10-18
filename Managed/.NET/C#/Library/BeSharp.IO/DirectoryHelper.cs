using System.IO;

namespace BeSharp.IO
{
    public class DirectoryHelper
    {
        public static string GetTemporaryDirectory()
        {
            do
            {
                try
                {
                    string tempPath = Path.GetTempPath();
                    string randomFileName = Path.GetRandomFileName();
                    string tempDirectory = Path.Combine(tempPath, randomFileName);
                    Directory.CreateDirectory(tempDirectory);
                    return tempDirectory;
                }
                catch (IOException /* ex */)
                {
                    // continue
                }
            } while (true);
        }
    }
}
