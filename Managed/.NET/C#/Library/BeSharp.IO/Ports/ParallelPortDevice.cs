using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Win32;
using BeSharp.Generic;
using System.IO;
using System.Security.Permissions;

namespace BeSharp.IO.Ports
{
    /// <summary>
    /// ParallelPortDevice Enumerates the ports from the registry
    /// </summary>
    public class ParallelPortDevice
    {
        protected ParallelPortDevice(RegistryKey deviceKey)
        {
            DeviceKeyName = deviceKey.Name;
            string HKLM_BackSlash = CurrentControlSetDeviceEnumerator.CombinePath(CurrentControlSetDeviceEnumerator.HKLM, string.Empty);
            if (DeviceKeyName.StartsWith(HKLM_BackSlash))
                DeviceKeyName = DeviceKeyName.Replace(HKLM_BackSlash, string.Empty);

            object _class = deviceKey.GetValue("Class"); // do not translate
            Class = objectToString(_class);

            object deviceDesc = deviceKey.GetValue("DeviceDesc"); // do not translate
            DeviceDesc = objectToString(deviceDesc);

            object friendlyName = deviceKey.GetValue("FriendlyName"); // do not translate
            FriendlyName = objectToString(friendlyName);

            object mfg = deviceKey.GetValue("Mfg"); // do not translate
            Mfg = objectToString(mfg);

            using (RegistryKey deviceParametersKey = deviceKey.OpenSubKey("Device Parameters")) // do not translate
            {
                if (null != deviceParametersKey)
                {
                    object portName = deviceParametersKey.GetValue("PortName"); // do not translate
                    PortName = objectToString(portName);
                    if (PortName.EndsWith(":"))
                    {
                        PortName = PortName.Substring(0, PortName.Length - 1);
                    }
                    object ieee1284Manufacturer = deviceParametersKey.GetValue("IEEE_1284_Manufacturer");
                    IEEE_1284_Manufacturer = objectToString(ieee1284Manufacturer);
                    object ieee1284Model = deviceParametersKey.GetValue("IEEE_1284_Model");
                    IEEE_1284_Model = objectToString(ieee1284Model);
                }
            }

            object upperFilters = deviceKey.GetValue("UpperFilters"); // do not translate
            UpperFilters = objectToString(upperFilters);

            object service = deviceKey.GetValue("Service"); // do not translate
            Service = objectToString(service);
        }

        public static List<ParallelPortDevice> GetParallelPortDevices()
        {
            List<ParallelPortDevice> result = new List<ParallelPortDevice>();
            string[] portNames = GetLogicalParallelPortNames();

            CurrentControlSetDeviceEnumerator.EnumerateAllDevices(deviceKey =>
                {
                    ParallelPortDevice device = new ParallelPortDevice(deviceKey);
                    string portName = device.PortName;
                    if (null != portName)
                    {
                        if (-1 != Array.IndexOf<string>(portNames, portName)) // portNames has no IndexOf, but Array<T> has
                        {
                            if (("Ports" == device.Class) || ("Parport" == device.Service)) // do not translate
                            {
                                result.Add(device);
                            }
                            if ("System" == device.Class)
                            {
                                if (!string.IsNullOrWhiteSpace(device.IEEE_1284_Manufacturer))
                                    if (!string.IsNullOrWhiteSpace(device.IEEE_1284_Model))
                                    {
                                        result.Add(device);
                                    }
                            }
                        }
                    }
                }
                );

            return result;
        }

        public static string[] GetPhysicalParallelPortNames()
        {
            //HashSet<string> uniquePortNames = new HashSet<string>();
            //List<ParallelPortDevice> parallelPortDevices = GetParallelPortDevices();
            //foreach (ParallelPortDevice parallelPortDevice in parallelPortDevices)
            //{
            //    uniquePortNames.Add(parallelPortDevice.PortName);
            //}
            //string[] result = uniquePortNames.ToArray<string>();
            List<ParallelPortDevice> parallelPortDevices = GetParallelPortDevices();
            //IEnumerable<IGrouping<string, ParallelPortDevice>> uniqueparallelPortDevicesGrouping = from parallelPortDevice in parallelPortDevices
            //                                                                                       group parallelPortDevice by parallelPortDevice.PortName into uniqueparallelPortDevices
            //                                                                                       select uniqueparallelPortDevices;
            //IEnumerable<string> portNames = from uniqueparallelPortDeviceGrouping in uniqueparallelPortDevicesGrouping
            //                                select uniqueparallelPortDeviceGrouping.Key;
            IEnumerable<string> portNames = from parallelPortDevice in parallelPortDevices
                                            group parallelPortDevice by parallelPortDevice.PortName into uniqueparallelPortDevices
                                            select uniqueparallelPortDevices.Key; // the group by Key is PortName
            string[] result = portNames.ToArray();
            return result;
        }

        public static string[] GetLptEnumParallelPortNames()
        {
            const string HKLM = @"HKEY_LOCAL_MACHINE"; // do not translate
            const string enumKeyName = @"SYSTEM\CurrentControlSet\Enum"; // do not translate
            const string lptEnum_SubKeyName = "LPTENUM"; // do not translate
            const string LocationInformation = "LocationInformation"; // do not translate

            List<string> portNames = new List<string>();

            string lptEnum_KeyName = Path.Combine(enumKeyName, lptEnum_SubKeyName);

            string HKLM_lptEnum_KeyName = Path.Combine(HKLM, lptEnum_KeyName);
            RegistryPermission HKLMEnumerationRegistryPermission = new RegistryPermission(RegistryPermissionAccess.Read, HKLM_lptEnum_KeyName);
            HKLMEnumerationRegistryPermission.Assert();
            try
            {
                using (RegistryKey localMachine = Registry.LocalMachine)
                {   // LPTENUM:
                    using (RegistryKey lptEnumKey = localMachine.OpenSubKey(lptEnum_KeyName, false))
                    {
                        string[] rawPort_SubKeyNames = lptEnumKey.GetSubKeyNames();
                        foreach (string rawPort_SubKeyName in rawPort_SubKeyNames)
                        {
                            string rawPort_KeyName = Path.Combine(lptEnum_KeyName, rawPort_SubKeyName);
                            using (RegistryKey rawPort_Key = localMachine.OpenSubKey(rawPort_KeyName))
                            {   // MicrosoftRawPort, etc
                                if (null != rawPort_Key)
                                {
                                    string[] devices_SubKeyNames = rawPort_Key.GetSubKeyNames();
                                    foreach (string device_SubKeyName in devices_SubKeyNames)
                                    {   // 5&1d62032d&0&LPT1
                                        // 5&35fb2ad7&0&LPT1
                                        string device_KeyName = Path.Combine(rawPort_KeyName, device_SubKeyName);
                                        using (RegistryKey deviceKey = localMachine.OpenSubKey(device_KeyName))
                                        {
                                            object portNameObject = deviceKey.GetValue(LocationInformation);
                                            if (null != portNameObject)
                                            portNames.Add(portNameObject.ToString());
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
                RegistryPermission.RevertAssert();
                // or CodeAccessPermission.RevertAssert();
            }

            return portNames.ToArray();
        }

        public static string[] GetLogicalParallelPortNames()
        {
            return ParallelPort.GetPortNames();
        }

        #region ToString

        private string objectToString(object value)
        {
            if (null == value)
            {
                return string.Empty;
            }
            if (value is Array)
            {
                StringBuilder result = new StringBuilder();
                foreach (object item in value as Array)
                {
                    if (0 != result.Length)
                    {
                        result.Append(Environment.NewLine);
                    }
                    result.Append(objectToString(item));
                }
                return result.ToString();
            }
            return value.ToString();
        }

        public override string ToString()
        {
            List<string> nameSeparatorValues = Reflector.GetNameSeparatorValues(new { DeviceKeyName, Class, DeviceDesc, FriendlyName, Mfg, PortName, UpperFilters, Service });
            string result = string.Join("; ", nameSeparatorValues);
            return result;
        }

        #endregion ToString

        #region public Properties

        public string Class { get; private set; }

        public string DeviceDesc { get; private set; }

        public string DeviceKeyName { get; private set; }

        public string FriendlyName { get; private set; }

        public string Mfg { get; private set; }

        public string IEEE_1284_Manufacturer { get; private set; }

        public string IEEE_1284_Model { get; private set; }

        public string PortName { get; private set; }

        public string Service { get; private set; }

        public string UpperFilters { get; private set; }

        #endregion

    }
}
