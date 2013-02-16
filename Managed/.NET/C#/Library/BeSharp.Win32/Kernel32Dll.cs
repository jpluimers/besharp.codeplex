using System;
using System.Runtime.InteropServices;
using System.Text;

namespace BeSharp.Win32
{
    internal class Kernel32Dll
    {
        private const string kernel32_dll = "kernel32.dll";
        internal const int MAX_PATH = 260;
        internal const int MAX_PATH_buffersize = MAX_PATH + 1;

        /// <summary>
        /// http://msdn.microsoft.com/library/windows/desktop/ms683152
        /// Do not call this for PInvoke calls: http://stackoverflow.com/a/2445558/29290
        /// Do not call this for handles obtained from GetModuleHandle.
        /// </summary>
        /// <param name="hModule">A handle to the loaded library module. The LoadLibrary, LoadLibraryEx, GetModuleHandle, or GetModuleHandleEx function returns this handle.</param>
        /// <returns>Boolean indicating if the function succeeds.</returns>
        [DllImport(kernel32_dll, SetLastError = true)]
        public static extern bool FreeLibrary(IntPtr hModule);

        /// <summary>
        /// Do not call FreeLibrary on a handle obtained from GetModuleHandle
        /// http://msdn.microsoft.com/en-us/library/windows/desktop/ms683199
        /// </summary>
        /// <param name="lpModuleName">The name of the loaded module.</param>
        /// <returns>Handle to the specified module. If the function fails, the return value is NULL.</returns>
        [DllImport(kernel32_dll, SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/windows/desktop/ms684175
        /// </summary>
        /// <param name="lpModuleName">The name of the module.</param>
        /// <returns>Handle to the specified module. If the function fails, the return value is NULL.</returns>
        [DllImport(kernel32_dll, SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr LoadLibrary(string lpModuleName);

        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/windows/desktop/aa364937
        /// </summary>
        /// <param name="lpDirectoryName">A directory on the disk.
        /// If this parameter is NULL, the function uses the root of the current disk.
        /// If this parameter is a UNC name, it must include a trailing backslash.</param>
        /// <param name="lpFreeBytesAvailable">Total number of free bytes on a disk that are available to the user who is associated with the calling thread.</param>
        /// <param name="lpTotalNumberOfBytes">Total number of bytes on a disk that are available to the user who is associated with the calling thread.</param>
        /// <param name="lpTotalNumberOfFreeBytes">Total number of free bytes on a disk.</param>
        /// <returns>Boolean indicating if the function succeeds.</returns>
        [DllImport(kernel32_dll, SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool GetDiskFreeSpaceEx(
            string lpDirectoryName,
            out ulong lpFreeBytesAvailable,
            out ulong lpTotalNumberOfBytes,
            out ulong lpTotalNumberOfFreeBytes
            );

        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/windows/desktop/aa364993
        /// </summary>
        /// <param name="lpRootPathName">Root directory of the volume.
        /// If this parameter is NULL, the function uses the root of the current disk.
        /// If this parameter is a Drive or UNC name, it must include a trailing backslash.</param>
        /// <param name="lpVolumeNameBuffer">buffer that receives the name of a specified volume</param>
        /// <param name="nVolumeNameSize">length of a volume name buffer, in TCHARs; max buffer size is MAX_PATH+1</param>
        /// <param name="lpVolumeSerialNumber">volume serial number</param>
        /// <param name="lpMaximumComponentLength">maximum length, in TCHARs, of a file name component that a specified file system supports</param>
        /// <param name="lpFileSystemFlags">flags associated with the specified file system</param>
        /// <param name="lpFileSystemNameBuffer">buffer that receives the name of the file system</param>
        /// <param name="nFileSystemNameSize">length of the file system name buffer, in TCHARs; max buffer size is MAX_PATH+1</param>
        /// <returns>Boolean indicating if the function succeeds.</returns>
        [DllImport(kernel32_dll, SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool GetVolumeInformation(
            string lpRootPathName,
            StringBuilder lpVolumeNameBuffer,
            int nVolumeNameSize,
            out uint lpVolumeSerialNumber,
            out uint lpMaximumComponentLength,
            out FileSystemFlags lpFileSystemFlags,
            StringBuilder lpFileSystemNameBuffer,
            int nFileSystemNameSize
      );
    }
}
