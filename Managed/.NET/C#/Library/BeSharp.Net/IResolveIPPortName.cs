using System.Collections.Generic;

namespace BeSharp.Net
{
    public interface IResolveIPPortName
    {
        IEnumerable<string> GetPortName(int port);
    }
}
