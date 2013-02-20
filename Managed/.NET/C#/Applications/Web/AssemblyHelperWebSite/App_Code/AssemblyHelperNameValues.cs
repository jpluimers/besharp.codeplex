using System;
using System.Collections.Generic;
using System.Text;

using BeSharp;
using BeSharp.Generic;

namespace AssemblyHelperHelper
{
    public static class AssemblyHelperNameValues
    {
        public static string NameValues
        {
            get
            {
                StringBuilder result = new StringBuilder();
                var values = new
                {
                    AssemblyHelper.ExecutableName,
                    AssemblyHelper.ExecutableDirectory,
                    AssemblyHelper.ExecutablePath,
                    AssemblyHelper.EntryAssemblyPath,
                    AssemblyHelper.EntryAssemblyDirectory,
                    AssemblyHelper.ExecutingAssemblyPath,
                    AssemblyHelper.ExecutingAssemblyDirectory,
                    AssemblyHelper.CallingAssemblyPath,
                    AssemblyHelper.CallingAssemblyDirectory,
                    CurrentProcessMainModuleFileName = AssemblyHelper.GetCurrentProcessMainModuleFileName(),
                    CurrentProcessMainModuleFileNameWithoutVsHost = AssemblyHelper.GetCurrentProcessMainModuleFileName(true),
                    AssemblyHelper.DotNetVersion,
                };
                IList<KeyValuePair<string, string>> nameValues = Reflector.GetNameValues(values);
                foreach (KeyValuePair<string, string> nameValue in nameValues)
                {
                    result.AppendLine(string.Format("{0}={1}", nameValue.Key, nameValue.Value));
                }

                return result.ToString();
            }
        }

        public static string NameValuesXHtml
        {
            get
            {
                string result = NameValues;
                result = result.Replace(Environment.NewLine, Environment.NewLine + "<br />" + Environment.NewLine);
                return result;
            }
        }
    }
}
