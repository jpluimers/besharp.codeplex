using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

namespace BeSharp.Net
{
    public static class IPGlobalPropertiesExtensions
    {
        public static IEnumerable<IGrouping<IPAddress, TcpConnectionInformation>> GetTcpConnectionsByRemoteAddressGroups()
        {
            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();

            return GetTcpConnectionsByRemoteAddressGroups(ipProperties);
        }

        public static IEnumerable<IGrouping<IPAddress, TcpConnectionInformation>> GetTcpConnectionsByRemoteAddressGroups(this IPGlobalProperties ipProperties)
        {
            TcpConnectionInformation[] tcpConnections = ipProperties.GetActiveTcpConnections();

            IEnumerable<IGrouping<IPAddress, TcpConnectionInformation>> result =
                from tcpConnection in tcpConnections
                //orderby tcpConnection.RemoteEndPoint.Address // "At least one object must implement IComparable." because Address of type IPAddress has no IComparable
                orderby tcpConnection.RemoteEndPoint.Address.ToString()
                group tcpConnection by tcpConnection.RemoteEndPoint.Address;
            return result;
        }
    }
}
