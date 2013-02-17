using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace BeSharp
{
    public class AssemblyHelper
    {
        public static string ExecutableDirectory
        {
            get
            {
                string result = Path.GetDirectoryName(ExecutablePath);
                return result;
            }
        }

        public static string ExecutableName
        {
            get
            {
                string result = Path.GetFileName(ExecutablePath);
                return result;
            }
        }

        // Logic partially borrowed from the Delphi.NET RTL
        public static string ExecutablePath
        {
            get
            {
                string result = GetExecutablePath(true);
                return result;
            }
        }

        private static string GetExecutablePath(bool replaceTrailingVsHostExeWithExe = false)
        {
            string result = EntryAssemblyPath;
            if (string.IsNullOrEmpty(result))
            {
                result = GetCurrentProcessMainModuleFileName(replaceTrailingVsHostExeWithExe);
            }
            return result;
        }

        public static string GetCurrentProcessMainModuleFileName(bool replaceTrailingVsHostExeWithExe = false)
        {
            Process currentProcess = Process.GetCurrentProcess();
            string currentProcessMainModuleFileName = currentProcess.MainModule.FileName;
            string result = replaceTrailingVsHostExeWithExe ?
                ReplaceTrailingVsHostExeWithExe(currentProcessMainModuleFileName)
                :
                currentProcessMainModuleFileName;
            return result;
        }

        private static string ReplaceTrailingVsHostExeWithExe(string result)
        {
            const string exe = ".exe";
            const string vshostExe = ".vshost" + exe;
            if (result.EndsWith(vshostExe))
            {
                string noVsHostExe = result.Substring(0, result.Length - vshostExe.Length);
                result = noVsHostExe + exe;
            }
            return result;
        }

        public static string EntryAssemblyPath
        {
            get
            {
                Assembly assembly = Assembly.GetEntryAssembly();
                // http://msdn.microsoft.com/en-us/library/system.reflection.assembly.getentryassembly.aspx
                // might be null when started from an unmanaged application.
                // or from a WCF process, or as an addin
                // http://stackoverflow.com/a/2848696/29290
                // http://stackoverflow.com/a/616606/29290
                if (null == assembly)
                    return string.Empty;
                else
                {
                    string result;
                    Module[] modules = assembly.GetModules();
                    Module firstModule = modules[0];
                    result = firstModule.FullyQualifiedName;
                    return result;
                }
            }
        }

        public static string DotNetVersionString
        {
            get
            {
                string result = DotNetVersion.ToString();
                return result;
            }
        }

        public static Version DotNetVersion
        {
            get
            {
                Version result = Environment.Version;
                return result;
            }
        }
    }
}