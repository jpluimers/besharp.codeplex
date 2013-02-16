namespace BeSharp.Win32
{
    public class VolumeInformation
    {
        public string RootPathName { get; private set; }
        public string VolumeName { get; private set; }
        public uint VolumeSerialNumber { get; private set; }
        public uint MaximumComponentLength { get; private set; }
        public FileSystemFlags FileSystemFlags { get; private set; }
        public string FileSystemName { get; private set; }

        public VolumeInformation(string rootPathName, string volumeName, uint volumeSerialNumber, uint maximumComponentLength, FileSystemFlags fileSystemFlags, string fileSystemName)
        {
            RootPathName = rootPathName;
            VolumeName = volumeName;
            VolumeSerialNumber = volumeSerialNumber;
            MaximumComponentLength = maximumComponentLength;
            FileSystemFlags = fileSystemFlags;
            FileSystemName = fileSystemName;
        }

    }
}
