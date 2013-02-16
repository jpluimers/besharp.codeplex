namespace BeSharp.Win32
{
    public class DiskFreeSpaceEx
    {
        public string DirectoryName { get; private set; }
        public ulong FreeBytesAvailable { get; private set; }
        public ulong TotalNumberOfBytes { get; private set; }
        public ulong TotalNumberOfFreeBytes { get; private set; }

        public DiskFreeSpaceEx(string directoryName, ulong freeBytesAvailable, ulong totalNumberOfBytes, ulong totalNumberOfFreeBytes)
        {
            DirectoryName = directoryName;
            FreeBytesAvailable = freeBytesAvailable;
            TotalNumberOfBytes = totalNumberOfBytes;
            TotalNumberOfFreeBytes = totalNumberOfFreeBytes;
        }
    }
}
