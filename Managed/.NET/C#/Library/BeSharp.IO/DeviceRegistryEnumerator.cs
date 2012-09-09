using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Security.Permissions;

namespace BeSharp.IO
{
    public static class CurrentControlSetDeviceEnumerator
    {
        internal static string CombinePath(string prefix, string suffix)
        {
            string result = string.Format(@"{0}\{1}",  // do not translate
                prefix, suffix);
            return result;
        }

        public static string HKLM = @"HKEY_LOCAL_MACHINE"; // do not translate

        public static void EnumerateAllDevices(Action<RegistryKey> enumerationAction)
        {
            string enumKeyName = @"SYSTEM\CurrentControlSet\Enum"; // do not translate
            string HKLMEnumerationPathList = CombinePath(HKLM, enumKeyName);
            RegistryPermission HKLMEnumerationRegistryPermission = new RegistryPermission(RegistryPermissionAccess.Read, HKLMEnumerationPathList);
            HKLMEnumerationRegistryPermission.Assert();
            try
            {
                using (RegistryKey localMachine = Registry.LocalMachine)
                {
                    using (RegistryKey deviceClassesEnumerationKey = localMachine.OpenSubKey(enumKeyName, false))
                    {
                        if (null != deviceClassesEnumerationKey)
                        {
                            string[] deviceClassesSubKeyNames = deviceClassesEnumerationKey.GetSubKeyNames();
                            foreach (string deviceClassSubKeyName in deviceClassesSubKeyNames)
                            {
                                // ACPI, DISPLAY, HID, LPTENUM, ...
                                string deviceClassKeyName = CombinePath(enumKeyName, deviceClassSubKeyName);
                                using (RegistryKey deviceSubClassesEnumerationKey = localMachine.OpenSubKey(deviceClassKeyName))
                                {
                                    if (null != deviceSubClassesEnumerationKey)
                                    {
                                        string[] deviceSubClassesSubKeyNames = deviceSubClassesEnumerationKey.GetSubKeyNames();
                                        foreach (string deviceSubClassSubKeyName in deviceSubClassesSubKeyNames)
                                        {
                                            string deviceSubClassKeyName = CombinePath(deviceClassKeyName, deviceSubClassSubKeyName);
                                            using (RegistryKey devicesEnumerationKey = localMachine.OpenSubKey(deviceSubClassKeyName))
                                            {
                                                if (null != devicesEnumerationKey)
                                                {
                                                    string[] devicesSubKeyNames = devicesEnumerationKey.GetSubKeyNames();
                                                    foreach (string deviceSubKeyName in devicesSubKeyNames)
                                                    {
                                                        string deviceKeyName = CombinePath(deviceSubClassKeyName, deviceSubKeyName);
                                                        using (RegistryKey deviceKey = localMachine.OpenSubKey(deviceKeyName))
                                                        {
                                                            enumerationAction(deviceKey);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            finally
            {
                RegistryPermission.RevertAssert(); // of HKLMEnumerationRegistryPermission.RevertAssert();
            }
        }

    }
}
