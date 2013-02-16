using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using BeSharp;
using BeSharp.Generic;
using BeSharp.Win32;

namespace UNCInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            if (0 == args.Length)
            {
                string executableName = AssemblyHelper.ExecutableName;
                string applicationName = Path.GetFileNameWithoutExtension(executableName);
                Console.WriteLine("{0} [UNC-path|Drive]...", applicationName);
                Console.WriteLine(@"Example: {0} C: \\{1}\Users", applicationName, Environment.MachineName);
            }
            else
            {
                string header = string.Empty;
                StringBuilder result = new StringBuilder();
                foreach (string arg in args)
                {
                    try
                    {
                        DiskFreeSpaceEx diskFreeSpaceEx = DiskInfo.GetDiskFreeSpaceEx(arg);
                        VolumeInformation volumeInformation = DiskInfo.GetVolumeInformation(arg);

                        var values = new
                            {
                                diskFreeSpaceEx.DirectoryName,
                                diskFreeSpaceEx.FreeBytesAvailable,
                                diskFreeSpaceEx.TotalNumberOfBytes,
                                diskFreeSpaceEx.TotalNumberOfFreeBytes,
                                volumeInformation.VolumeSerialNumber,
                                volumeInformation.MaximumComponentLength,
                                volumeInformation.FileSystemFlags,
                                volumeInformation.FileSystemName,
                                volumeInformation.VolumeName,
                            };
                        IList<string> valueStrings = Reflector.GetValueStrings(values);

                        const string separator = ";";

                        string line = string.Join(separator, valueStrings);
                        result.AppendLine(line);

                        IList<string> names = Reflector.GetNames(values);
                        header = string.Join(separator, names);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception processing '{0}': {1}", arg, ex);
                    }
                }
                if (result.Length > 0)
                {
                    Console.WriteLine(header);
                    Console.Write(result);
                }
            }
        }
    }
}
