using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

namespace DotNetStat
{
    class Program
    {
        static void Main(/*string[] args*/)
        {

            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();

            TcpConnectionInformation[] tcpConnections = ipProperties.GetActiveTcpConnections();

            consoleWriteLineHeader(tcpConnections, "active TCP connections");
            if (tcpConnections.Length != 0)
            {
                consoleWriteLine(
                    "Local",
                    "Remote",
                    "State");
                foreach (TcpConnectionInformation tcpConnection in tcpConnections)
                {
                    consoleWriteLine(
                        tcpConnection.LocalEndPoint.ToAddressPortString(),
                        tcpConnection.RemoteEndPoint.ToAddressPortString(),
                        tcpConnection.State);
                }
                Console.WriteLine("Distinct Remote Address:Port pairs by Remote Address:");

                IEnumerable<IGrouping<IPAddress, TcpConnectionInformation>> tcpConnectionsByRemoteaddressGroups = from tcpConnection in tcpConnections
                                                                                                                  //orderby tcpConnection.RemoteEndPoint.Address // "At least one object must implement IComparable." because Address has no IComparable
                                                                                                                  orderby tcpConnection.RemoteEndPoint.Address.ToString()
                                                                                                                  group tcpConnection by tcpConnection.RemoteEndPoint.Address;

                foreach (IGrouping<IPAddress, TcpConnectionInformation> tcpConnectionsByRemoteaddressGroup in tcpConnectionsByRemoteaddressGroups)
                {
                    //123456789012345  
                    //255.255.255.255  80, 443
                    Console.Write("{0,-15}  ", tcpConnectionsByRemoteaddressGroup.Key);
                    IEnumerable<int> allRemotePorts = from tcpConnectionsByRemoteaddress in tcpConnectionsByRemoteaddressGroup
                                                      orderby tcpConnectionsByRemoteaddress.RemoteEndPoint.Port
                                                      select tcpConnectionsByRemoteaddress.RemoteEndPoint.Port;
                    IEnumerable<int> distinctRemotePorts = allRemotePorts.Distinct();
                    bool first = true;
                    foreach (int distinctRemotePort in distinctRemotePorts)
                    {
                        if (!first)
                            Console.Write(", ");
                    	Console.Write(distinctRemotePort);
                        first = false;
                    }
                    Console.WriteLine();
                }
            }

            IPEndPoint[] tcpEndPoints = ipProperties.GetActiveTcpListeners();
            consoleWriteLineHeader(tcpEndPoints, "listening TCP end points");
            if (tcpEndPoints.Length != 0)
            {
                foreach (IPEndPoint tcpEndPoint in tcpEndPoints)
                {
                    Console.WriteLine(tcpEndPoint.ToAddressPortString());
                }
            }
        }

        private static void consoleWriteLine(object first, object second, object third)
        {
            //Local                  Remote                 State
            //255.255.255.255:65535  255.255.255.255:65535  Established
            //123456789012345678901  123456789012345678901  
            //192.168.0.105:5856     65.52.103.74:443       TimeWait
            //192.168.0.105:5858     64.4.11.25:80          CloseWait
            //192.168.0.105:5861     10.0.20.43:8080        SynSent
            Console.WriteLine("{0,-21}  {1,-21}  {2}", first, second, third);
        }

        private static void consoleWriteLineHeader(Array array, string description)
        {
            if (array.Length == 0)
                Console.WriteLine("No {0}.", description);
            else
                Console.WriteLine("All {0}:", description);
        }

    }
}

