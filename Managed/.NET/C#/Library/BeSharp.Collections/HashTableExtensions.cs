using System;
using System.Collections;

namespace BeSharp.Collections
{
    public static class HashTableExtensions
    {
        /// <summary>
        /// Centralize all the null/value behaviour.
        /// </summary>
        /// <typeparam name="T">type to get; can be simple value type (int, double, etc) or string</typeparam>
        /// <param name="properties">HashTable to get property from</param>
        /// <param name="propertyName">name of property to get</param>
        /// <returns>default(T) if property does not have value, or value.</returns>
        public static T LoadValue<T>(this Hashtable properties, string propertyName)
        {
            T defaultValue = default(T);
            T result = LoadValue(properties, propertyName, defaultValue);
            return result;
        }

        /// <summary>
        /// Centralize all the null/value behaviour.
        /// </summary>
        /// <typeparam name="T">type to get; can be simple value type (int, double, etc) or string</typeparam>
        /// <param name="properties">HashTable to get property from</param>
        /// <param name="propertyName">name of property to get</param>
        /// <param name="nullStringAllowed">What to do when a string property is null; True: return null, otherwise return String.Empty.</param>
        /// <returns>default(T) if property does not have value, otherwise Trimmed value for string, or value for other types.</returns>
        public static string LoadValue(this Hashtable properties, string propertyName, bool nullStringAllowed)
        {
            string defaultValue;
            if (nullStringAllowed)
                defaultValue = null;
            else
                defaultValue = string.Empty;
            string result = LoadValue(properties, propertyName, defaultValue);
            return result;
        }

        /// <summary>
        /// Centralize all the null/value behaviour.
        /// </summary>
        /// <typeparam name="T">type to get; can be simple value type (int, double, etc) or string</typeparam>
        /// <param name="properties">HashTable to get property from</param>
        /// <param name="propertyName">name of property to get</param>
        /// <returns>default(T) if property does not have value, otherwise Trimmed value for string, or value for other types.</returns>
        public static T LoadValue<T>(this Hashtable properties, string propertyName, T defaultValue)
        {
            T result;
            bool haveValue = properties[propertyName] != null;

            Type genericType = typeof(T);
            Type stringType = typeof(string);
            if (genericType == stringType)
            {
                // http://stackoverflow.com/questions/15157555/is-there-a-solution-that-feels-less-clumsy-than-convert-changetype-to-get-the-va/15157706#15157706
                object stringResult;
                if (haveValue)
                    stringResult = ((string)properties[propertyName]).Trim();
                else
                    stringResult = defaultValue;
                result = (T)stringResult;
            }
            else
            {
                if (haveValue)
                    result = (T)(properties[propertyName]);
                else
                    result = defaultValue;
            }
            return result;
        }
    }
}