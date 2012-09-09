using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using BeSharp.IO.Ports;

namespace ShowPhysicalDeviceNames
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static void run()
        {
            WriteLines("All ports:"); 
            showPortNames(SerialPort.GetPortNames);
            showPortNames(ParallelPort.GetPortNames);
            WriteLines("Physical ports:");
            showPortNames(SerialPortDevice.GetPhysicalSerialPortNames);
            showPortNames(ParallelPortDevice.GetPhysicalParallelPortNames);
            WriteLines("Physical devices:");
            List<SerialPortDevice> serialPortDevices = SerialPortDevice.GetSerialPortDevices();
            foreach (SerialPortDevice serialPortDevice in serialPortDevices)
            {
                Console.WriteLine(serialPortDevice);
            }
            List<ParallelPortDevice> parallelPortDevices = ParallelPortDevice.GetParallelPortDevices();
            foreach (ParallelPortDevice parallelPortDevice in parallelPortDevices)
            {
                Console.WriteLine(parallelPortDevice);
            }
            WriteLines("LPTENUM ports:");
            showPortNames(ParallelPortDevice.GetLptEnumParallelPortNames);
            WriteLines("Done.");
        }

        private static void WriteLines(string newVariable)
        {
            Console.WriteLine();
            Console.WriteLine(newVariable);
            Console.WriteLine();
        }

        private static void showPortNames(IEnumerable<string> portNames)
        {
            foreach (string portName in portNames)
            {
                Console.WriteLine(portName);
            }
        }

        private static void showPortNames(Func<string[]> getPortNamesFunc)
        {
            string[] portNames = getPortNamesFunc();
            showPortNames(portNames.OrderBy(x => x));
        }

    }
}
