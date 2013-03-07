using System;
using System.Collections;
using System.Collections.Generic;

using BeSharp.Collections;
using BeSharp.Generic;

namespace HashTableExtensionsDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Hashtable properties = new Hashtable();
            properties["int"] = 21;
            properties["bool"] = false;
            properties["double"] = 3.14;
            properties["string"] = "foo";
            properties["null"] = null;

            // get values that are not null
            int i = properties.LoadValue<int>("int");
            bool b = properties.LoadValue<bool>("bool");
            double d = properties.LoadValue<double>("double");
            string s = properties.LoadValue<string>("string");

            printValues(i, d, s);

            // convert null values to a specified default
            i = properties.LoadValue<int>("null", -i);
            b = properties.LoadValue<bool>("bool", !b);
            d = properties.LoadValue<double>("null", -d);
            s = properties.LoadValue<string>("null", "empty");

            printValues(i, d, s);

            // convert null values to the default for the type
            i = properties.LoadValue<int>("null");
            b = properties.LoadValue<bool>("bool");
            d = properties.LoadValue<double>("null");
            s = properties.LoadValue<string>("null");

            printValues(i, d, s);
        }

        private static void printValues(int i, double f, string s)
        {
            IList<string> nameSeparatorValues = Reflector.GetNameSeparatorValues(new { i, f, s });
            Console.WriteLine(string.Join(Environment.NewLine, nameSeparatorValues));
        }
    }
}
