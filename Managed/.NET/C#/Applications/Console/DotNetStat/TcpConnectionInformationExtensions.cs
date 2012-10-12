using System;
using System.Net;

namespace DotNetStat
{
    public static class IPEndPointExtensions
    {
        public static string ToAddressPortString(this IPEndPoint endPoint)
        {
            string result = string.Format("{0}:{1}", endPoint.Address, endPoint.Port);
            return result;
        }
    }
}
