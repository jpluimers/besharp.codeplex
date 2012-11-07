using System.Net;
using System.Collections.Generic;

namespace BeSharp.Net
{
    public class ResolveIPAdress : IResolveIPAdress
    {
        public IEnumerable<string> ResolveIPAddress(IPAddress ipAddress)
        {
            List<string> result = new List<string>();
            result.Add(ipAddress.ToString());
            return result;
        }
    }
}
