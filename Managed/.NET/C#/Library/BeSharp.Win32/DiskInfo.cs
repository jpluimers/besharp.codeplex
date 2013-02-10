namespace BeSharp.Win32
{
    public class DiskInfo
    {
        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/windows/desktop/aa364937
        /// </summary>
        /// <param name="directoryName">A directory on the disk.
        /// If this parameter is NULL, the function uses the root of the current disk.
        /// If this parameter is a UNC name, it must include a trailing backslash.</param>
        /// <returns>DiskFreeSpaceEx on success, null on error.</returns>
        public static DiskFreeSpaceEx GetDiskFreeSpaceEx(string directoryName)
        {
            long freeBytesAvailable;
            long totalNumberOfBytes;
            long totalNumberOfFreeBytes;

            DiskFreeSpaceEx result;
            if (Kernel32Dll.GetDiskFreeSpaceEx(directoryName, out freeBytesAvailable, out totalNumberOfBytes, out totalNumberOfFreeBytes))
                result = new DiskFreeSpaceEx(directoryName, freeBytesAvailable, totalNumberOfBytes, totalNumberOfFreeBytes);
            else
                result = null;
            return result;
        }
    }
}
