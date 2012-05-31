using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Globalization;

namespace SqlConnectionStringBuilderKeysConsoleApplication
{
    public class SqlConnectionStringBuilderHelper
    {

        public static IEnumerable<string> Keys
        {
            get
            {
                SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
                ICollection keys = connectionStringBuilder.Keys;
                // Reflection shows that SqlConnectionStringBuilder actually returns System.Data.Common.ReadOnlyCollection<string> which is an internal type.
                // but it implements IEnumerable<string>
                return (IEnumerable<string>)keys; 
            }
        }

        public static List<string> EquivalentKeys(string key)
        {
            object keywordsObject = GetStaticPrivateProperty<SqlConnectionStringBuilder>("_keywords");
            // keywordsObject is actually Dictionary<string, Keywords>, but KeyWords is a private enum inside the SqlClient namespace.
            IDictionary keywords = (IDictionary)keywordsObject;

            object value = keywords[key];

            List<string> result = new List<string>();

            foreach (DictionaryEntry keyword in keywords)
            {
                if (value.Equals(keyword.Value))
                    result.Add(keyword.Key.ToString());
            }

            return result;
        }

        public static string ShortestEquivalentKey(string key)
        {
            List<string> equivalentKeys = EquivalentKeys(key);

            string result = key;

            foreach (string equivalentKey in equivalentKeys)
            {
                if (equivalentKey.Length < result.Length)
                    result = equivalentKey;
            }

            return result;
        }

        // mimicked after the getter from SqlConnectionStringBuilder.ConnectionString
        public static string ShortestConnectionString(SqlConnectionStringBuilder connectionStringBuilder)
        {
            string result;
            StringBuilder builder = new StringBuilder();
            foreach (string key in connectionStringBuilder.Keys)
            {
                object valueObject;
                if (connectionStringBuilder.ShouldSerialize(key) && connectionStringBuilder.TryGetValue(key, out valueObject))
                {
                    string value = (valueObject != null) ? Convert.ToString(valueObject, CultureInfo.InvariantCulture) : null;
                    string shortKey = ShortestEquivalentKey(key);
                    bool UseOdbcRules = false;
                    SqlConnectionStringBuilder.AppendKeyValuePair(builder, shortKey, value, UseOdbcRules);
                }
            }
            result = builder.ToString();
            return result;
        }

        // http://stackoverflow.com/a/628687/29290
        protected static object GetStaticPrivateProperty<T>(string name)
        {
            Type type = typeof(T);
            FieldInfo info = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Static);
            object result = info.GetValue(null);
            return result;
        }

    }
}
