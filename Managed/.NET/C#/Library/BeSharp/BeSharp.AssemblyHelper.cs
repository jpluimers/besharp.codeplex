using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace BeSharp
{
    public class AssemblyHelper
    {

        // Logic borrowed from the Delphi.NET RTL
        public static string ExecutableName
        {
            get
            {
                string fullyQualifiedName;

                Assembly entryAssembly = Assembly.GetEntryAssembly();
                if (null != entryAssembly)
                {
                    fullyQualifiedName = entryAssembly.GetModules()[0].FullyQualifiedName;
                }
                else
                {
                    // http://msdn.microsoft.com/en-us/library/system.reflection.assembly.getentryassembly.aspx
                    // might be null when started from an unmanaged application.
                    // or from a WCF process, or as an addin
                    // http://stackoverflow.com/a/2848696/29290
                    // http://stackoverflow.com/a/616606/29290
                    fullyQualifiedName = Process.GetCurrentProcess().MainModule.FileName;
                }
                return Path.GetFileName(fullyQualifiedName);
            }
        }

    }
}