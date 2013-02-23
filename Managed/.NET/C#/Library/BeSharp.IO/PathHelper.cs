using System.IO;

namespace BeSharp.IO
{
    public class PathHelper
    {
        public static string RemoveTrailingDirectorySeparators(string directory)
        {
            string result = directory.TrimEnd(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            return result;
        }
    }
}
