using System.Collections.Generic;

namespace BeSharp.Net
{
    public class ResolveIPPortName : IResolveIPPortName
    {
        IEnumerable<string> IResolveIPPortName.GetPortName(int port)
        {
            List<string> result = new List<string>();
            result.Add(port.ToString());
            return result;
        }
    }
}
