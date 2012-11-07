using System;
using System.Security.Permissions;
using System.Security;

using Microsoft.Win32;

namespace BeSharp.Win32
{
    public class ReadOnlyAccessToRegistryKey
    {
        public static void Run(RegistryKey hiveRegistryKey, string subKeyName, Action<RegistryKey> action)
        {
            string fullKeyName = hiveRegistryKey.Combine(subKeyName);
            RegistryPermission readRegistryPermission = new RegistryPermission(RegistryPermissionAccess.Read, fullKeyName);
            readRegistryPermission.Assert();
            try
            {
                // false (readonly) is the default, but I always forget that http://stackoverflow.com/a/877869/29290
                using (RegistryKey registryKey = hiveRegistryKey.OpenSubKey(subKeyName, false))
                {
                    action(registryKey);
                }
            }
            finally
            {
                CodeAccessPermission.RevertAssert();
            }
        }
    }
}