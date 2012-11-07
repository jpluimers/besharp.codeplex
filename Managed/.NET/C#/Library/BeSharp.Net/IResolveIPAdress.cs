using System.Collections.Generic;
using System.Net;

namespace BeSharp.Net
{
    public interface IResolveIPAdress
    {
        IEnumerable<string> ResolveIPAddress(IPAddress ipAddress);
    }
}