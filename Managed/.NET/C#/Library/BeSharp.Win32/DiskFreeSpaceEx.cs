namespace BeSharp.Win32
{
    public class DiskFreeSpaceEx
    {
        public string DirectoryName { get; private set; }
        public long FreeBytesAvailable { get; private set; }
        public long TotalNumberOfBytes { get; private set; }
        public long TotalNumberOfFreeBytes { get; private set; }

        public DiskFreeSpaceEx(string directoryName, long freeBytesAvailable, long totalNumberOfBytes, long totalNumberOfFreeBytes)
        {
            DirectoryName = directoryName;
            FreeBytesAvailable = freeBytesAvailable;
            TotalNumberOfBytes = totalNumberOfBytes;
            TotalNumberOfFreeBytes = totalNumberOfFreeBytes;
        }
    }
}
