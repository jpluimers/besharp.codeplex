using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BeSharp.Generic;

namespace UNCInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            if (0 == args.Length)
                Console.WriteLine("{0} UNC-path", "UNCInfo");
            else
            {
                string header = string.Empty;
                StringBuilder result = new StringBuilder();
                foreach (string arg in args)
                {
                    // GetDriveInfo does not work with UNC paths:
                    //System.ArgumentException was unhandled
                    //HResult=-2147024809
                    //Message=Object must be a root directory ("C:\") or a drive letter ("C").
                    //Source=mscorlib
                    //StackTrace:
                    //     at System.IO.DriveInfo..ctor(String driveName)
                    //     at Microsoft.VisualBasic.FileIO.FileSystem.GetDriveInfo(String drive)
                    //System.IO.DriveInfo driveInfo = Microsoft.VisualBasic.FileIO.FileSystem.GetDriveInfo(arg);
                    //Console.WriteLine("path {0}, available free {1}, total free {1}", arg, driveInfo.AvailableFreeSpace, driveInfo.TotalFreeSpace);
                    //System.IO.DriveInfo driveInfo = Microsoft.VisualBasic.FileIO.FileSystem.GetDriveInfo(arg);
                    //Console.WriteLine("path {0}, available free {1}, total free {1}", arg, driveInfo.AvailableFreeSpace, driveInfo.TotalFreeSpace);
                    BeSharp.Win32.DiskFreeSpaceEx diskFreeSpaceEx = BeSharp.Win32.DiskInfo.GetDiskFreeSpaceEx(arg);
                    if (null != diskFreeSpaceEx)
                    {
                        try
                        {
                            const string comma = ",";
                            var values = new { diskFreeSpaceEx.DirectoryName, diskFreeSpaceEx.FreeBytesAvailable, diskFreeSpaceEx.TotalNumberOfBytes, diskFreeSpaceEx.TotalNumberOfFreeBytes };
                            IList<string> valueStrings = Reflector.GetValueStrings(values);
                            string line = string.Join(comma, valueStrings);
                            result.Append(line);
                            IList<string> names = Reflector.GetNames(values);
                            header = string.Join(comma, names);
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
}
