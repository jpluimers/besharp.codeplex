using System.IO;

namespace BeSharp.IO
{
    public static class FileHelper
    {

        public static void CreateUnderlyingDirectory(string path)
        {
            string directoryPath = Path.GetDirectoryName(path);
            Directory.CreateDirectory(directoryPath); // NOP if it exists, will create all parent directories if not
        }

        public static StreamWriter AppendTextOrCreateTextAndParentDirectories(string path)
        {
            StreamWriter result;
            if (File.Exists(path))
            {
                result = File.AppendText(path);
            }
            else
            {
                CreateUnderlyingDirectory(path);
                result = File.CreateText(path);
            }
            return result;
        }

        public static StreamWriter CreateOrOverwriteTextAndCreateParentDirectories(string path)
        {
            CreateUnderlyingDirectory(path);
            StreamWriter result = new StreamWriter(path, false);
            return result;
        }
    }
}
