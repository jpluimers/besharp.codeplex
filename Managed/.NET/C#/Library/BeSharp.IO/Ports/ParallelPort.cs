using System;
using System.Security.Permissions;
using System.Security;
using System.IO;
using Microsoft.Win32;

namespace BeSharp.IO.Ports
{
    public class ParallelPort
    {
        private const string HARDWARE_DEVICEMAP_PARALLELPORTS = @"HARDWARE\DEVICEMAP\PARALLEL PORTS";

        public static string[] GetPortNames()
        {
            string[] portNames = null;
            string fullKeyName = Path.Combine(CurrentControlSetDeviceEnumerator.HKLM, HARDWARE_DEVICEMAP_PARALLELPORTS);
            new RegistryPermission(RegistryPermissionAccess.Read, fullKeyName).Assert();
            try
            {
                using (RegistryKey localMachine = Registry.LocalMachine)
                {
                    // http://stackoverflow.com/questions/374200/finding-available-lpt-parallel-ports-and-addresses-in-delphi
                    using (RegistryKey portsRegistryKey = localMachine.OpenSubKey(HARDWARE_DEVICEMAP_PARALLELPORTS, false))
                    {
                        if (null != portsRegistryKey)
                        {
                            string[] valueNames = portsRegistryKey.GetValueNames();
                            portNames = new string[valueNames.Length];
                            for (int i = 0; i < valueNames.Length; i++)
                            {
                                object value = portsRegistryKey.GetValue(valueNames[i]);
                                if (null != value)
                                {
                                    string fullPortName = (string)value;
                                    portNames[i] = fullPortName.Replace(@"\DosDevices\", string.Empty);
                                }
                            }
                        }
                    }
                }
            }
            finally
            {
                CodeAccessPermission.RevertAssert();
            }
            if (portNames == null)
            {
                portNames = new string[0];
            }
            return portNames;
        }
    }
}