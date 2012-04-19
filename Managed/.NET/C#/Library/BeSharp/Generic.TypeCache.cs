using System;
using System.Collections.Generic;
using System.Reflection;

namespace BeSharp.Generic
{
    static class TypeCache<T>
    {
        public static string Name
        {
            get
            {
                if (Names.Length != 1)
                    throw new ArgumentException("type T must pass exactly one element", "T");
                string result = Names[0];
                return result;
            }
        }
        public static readonly string[] Names;

        // http://abdullin.com/journal/2008/12/13/how-to-find-out-variable-or-parameter-name-in-c.html
        static TypeCache()
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            List<string> names = new List<string>();
            foreach (PropertyInfo property in properties)
            {
                names.Add(property.Name);
            }
            Names = names.ToArray();
        }
    }
}