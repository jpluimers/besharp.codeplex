using System;
using System.Security;
using System.Runtime.Serialization;

namespace BeSharp.Generic
{
    /// <summary>
    /// see http://wiert.wordpress.com/2010/07/28/netc-a-generic-exception-class/
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Exception<T> : Exception 
    {
        public Exception()
            : base()
        {
        }

        public Exception(string message)
            : base(message)
        {
        }

        [SecuritySafeCritical]
        protected Exception(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }

        public Exception(string message, Exception innerException)
            : base (message, innerException)
        {
        }
    }
}