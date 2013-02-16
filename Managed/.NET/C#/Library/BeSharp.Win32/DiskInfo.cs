using System.Text;
using System.IO;
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
            ulong freeBytesAvailable;
            ulong totalNumberOfBytes;
            ulong totalNumberOfFreeBytes;

            DiskFreeSpaceEx result;
            if (Kernel32Dll.GetDiskFreeSpaceEx(directoryName, out freeBytesAvailable, out totalNumberOfBytes, out totalNumberOfFreeBytes))
                result = new DiskFreeSpaceEx(directoryName, freeBytesAvailable, totalNumberOfBytes, totalNumberOfFreeBytes);
            else
                result = null;
            return result;
        }

        public static VolumeInformation GetVolumeInformation(string rootPathName)
        {
            StringBuilder volumeName;
            uint volumeSerialNumber;
            uint maximumComponentLength;
            FileSystemFlags fileSystemFlags;
            StringBuilder fileSystemName;

            if (!rootPathName.EndsWith(Path.DirectorySeparatorChar.ToString()))
                rootPathName = rootPathName + Path.DirectorySeparatorChar;

            volumeName = new StringBuilder(Kernel32Dll.MAX_PATH_buffersize);
            fileSystemName = new StringBuilder(Kernel32Dll.MAX_PATH_buffersize);

            VolumeInformation result;
            if (Kernel32Dll.GetVolumeInformation(rootPathName, volumeName, volumeName.Capacity, out volumeSerialNumber, out maximumComponentLength, out fileSystemFlags, fileSystemName, fileSystemName.Capacity))
                result = new VolumeInformation(rootPathName, volumeName.ToString(), volumeSerialNumber, maximumComponentLength, fileSystemFlags, fileSystemName.ToString());
            else
                result = null;
            return result;
        }
    }
}
