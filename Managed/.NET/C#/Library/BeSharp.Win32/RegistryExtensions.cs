using System;

using Microsoft.Win32;

namespace BeSharp.Win32
{
    public static class RegistryExtensions
    {
        // Hives: http://en.wikipedia.org/wiki/Windows_Registry
        public readonly static string HKCC = Registry.CurrentConfig.Name;
        public readonly static string HKCR = Registry.ClassesRoot.Name;
        public readonly static string HKCU = Registry.CurrentUser.Name;
#if Obsolete
        [Obsolete]
        public readonly static string HKDD = Registry.DynData.Name;
#endif
        public readonly static string HKLM = Registry.LocalMachine.Name;
        public readonly static string HKPD = Registry.PerformanceData.Name;
        public readonly static string HKU = Registry.Users.Name;

        public static string Combine(this string keyName, string subKeyName)
        {
            string result = string.Format(@"{0}\{1}", keyName, subKeyName);
            return result;
        }

        public static string Combine(this RegistryKey registryKey, string subKeyName)
        {
            string result = registryKey.Name.Combine(subKeyName);
            return result;
        }

        public static string Combine(this RegistryKey registryKey, RegistryKey subRegistryKey)
        {
            return registryKey.Combine(subRegistryKey.Name);
        }

        // http://stackoverflow.com/questions/444798/case-insensitive-containsstring/444818#444818

        public static bool Contains(this string value, string substring, StringComparison stringComparison = StringComparison.CurrentCulture)
        {
            int index = value.IndexOf(substring, stringComparison);
            bool result = (index >= 0);
            return result;
        }
    }
}
