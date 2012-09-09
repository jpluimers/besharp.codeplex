using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO.Ports;

using Microsoft.Win32;
using BeSharp.Generic;

namespace BeSharp.IO.Ports
{
    /// <summary>
    /// SerialPortDevices Enumerates the ports from the registry
    /// </summary>
    public class SerialPortDevice
    {
        protected SerialPortDevice(RegistryKey deviceKey)
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
                }
            }

            object upperFilters = deviceKey.GetValue("UpperFilters"); // do not translate
            UpperFilters = objectToString(upperFilters);

            object service = deviceKey.GetValue("Service"); // do not translate
            Service = objectToString(service);
        }

        public static List<SerialPortDevice> GetSerialPortDevices()
        {
            List<SerialPortDevice> result = new List<SerialPortDevice>();
            string[] portNames = GetSerialPortNames();

            CurrentControlSetDeviceEnumerator.EnumerateAllDevices(deviceKey =>
                {
                    SerialPortDevice device = new SerialPortDevice(deviceKey);
                    string portName = device.PortName;
                    if (null != portName)
                        if (-1 != Array.IndexOf<string>(portNames, portName)) // portNames has no IndexOf, but Array<T> has
                            if (("Ports" == device.Class) || ("serenum" == device.UpperFilters)) // do not translate
                            {
                                result.Add(device);
                            }
                }
                );

            return result;
        }

        public static string[] GetPhysicalSerialPortNames()
        {
            List<SerialPortDevice> serialPortDevices = GetSerialPortDevices();
            IEnumerable<string> portNames = from serialPortDevice in serialPortDevices
                                            group serialPortDevice by serialPortDevice.PortName into uniqueparallelPortDevices
                                            select uniqueparallelPortDevices.Key; // the group by Key is PortName
            string[] result = portNames.ToArray();
            return result;
        }

        public static string[] GetSerialPortNames()
        {
            return SerialPort.GetPortNames();
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

        public string PortName { get; private set; }

        public string Service { get; private set; }

        public string UpperFilters { get; private set; }

        #endregion

    }
}