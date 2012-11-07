using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

namespace BeSharp.Net
{
    public class IPAddressTcpConnectionInformationsSummary
    {
        private readonly IPAddress ipAddress;
        private readonly List<int> localPorts = new List<int>();
        private readonly List<int> remotePorts = new List<int>();
        public readonly IResolveIPAdress ResolveIPAdress;
        public readonly IResolveIPPortName ResolveIPPortName;

        public IPAddressTcpConnectionInformationsSummary(
            IGrouping<IPAddress, TcpConnectionInformation> tcpConnectionsByAddressGroup, 
            ICollection<int> portsToInclude = null,
            IResolveIPAdress resolveIPAdress = null,
            IResolveIPPortName resolveIPPortName = null)
        {
            if (null == tcpConnectionsByAddressGroup)
                return;

            if (null == portsToInclude)
                portsToInclude = new List<int>();

            if (null == resolveIPAdress)
                resolveIPAdress = new ResolveIPAdress();

            if (null == resolveIPPortName)
                resolveIPPortName = new ResolveIPPortName();

            ipAddress = tcpConnectionsByAddressGroup.Key;

            ResolveIPAdress = resolveIPAdress;
            ResolveIPPortName = resolveIPPortName;

            IEnumerable<int> allLocalPorts = from tcpConnectionsByLocaladdress in tcpConnectionsByAddressGroup
                                             orderby tcpConnectionsByLocaladdress.LocalEndPoint.Port
                                             select tcpConnectionsByLocaladdress.LocalEndPoint.Port;

            addPorts(allLocalPorts, portsToInclude, ref localPorts);

            IEnumerable<int> allRemotePorts = from tcpConnectionsByRemoteaddress in tcpConnectionsByAddressGroup
                                              orderby tcpConnectionsByRemoteaddress.RemoteEndPoint.Port
                                              select tcpConnectionsByRemoteaddress.RemoteEndPoint.Port;

            addPorts(allRemotePorts, portsToInclude, ref remotePorts);
        }

        private static void addPorts(IEnumerable<int> allPorts, ICollection<int> portsToInclude, ref List<int> ports)
        {
            allPorts = allPorts.Distinct();

            if (0 == portsToInclude.Count)
                ports.AddRange(allPorts);
            else
                foreach (int port in allPorts)
                {
                    if (portsToInclude.Contains(port))
                    {
                        ports.Add(port);
                    }
                }
        }

        protected virtual List<string> GetResolvedIPPorts(List<int> ports)
        {
            List<string> result = new List<string>();
            foreach (int port in ports)
            {
                IEnumerable<string> portNames = ResolveIPPortName.GetPortName(port);
                foreach (string portName in portNames)
                {
                    if (!result.Contains(portName)) // duplicates should not occur, but better be safe than sorry
                        result.Add(portName);
                }
            }
            return result;
        }

        public bool HasActiveLocalOrRemotePorts
        {
            get
            {
                bool result = (0 != localPorts.Count) || (0 != remotePorts.Count);
                return result;
            }
        }

        public IEnumerable<string> HostNames
        {
            get
            {
                IEnumerable<string> result = ResolveIPAdress.ResolveIPAddress(ipAddress);
                return result;
            }
        }

        public IEnumerable<string> LocalPorts
        {
            get
            {
                List<string> result = GetResolvedIPPorts(localPorts);
                return result;
            }
        }

        public IEnumerable<string> RemotePorts
        {
            get
            {
                List<string> result = GetResolvedIPPorts(remotePorts);
                return result;
            }
        }

    }
}
