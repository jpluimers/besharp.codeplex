using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using BeSharp.Generic;

namespace BeSharp.Linq
{
    public static class EnumerableExtensions
    {
        public const string Comma = ",";

        // similar to System.Linq.Enumerable.Sum: public static int Sum(this IEnumerable<ulong> source)
        public static ulong Sum(this IEnumerable<ulong> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(Reflector.GetName(new { source }));
            }
            ulong sum = 0;
            foreach (ulong value in source)
            {
                sum += value;
            }
            return sum;
        }

        // similar to System.Linq.Enumerable.Sum: public static int Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
        public static ulong Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, ulong> selector)
        {
            return source.Select<TSource, ulong>(selector).Sum();
        }

        [ComVisible(false)]
        public static string JoinToSimpleCSV<T>(this IEnumerable<T> values)
        {
            string result = string.Join(Comma, values);
            return result;
        }

        [ComVisible(false)]
        public static string JoinToSimpleCSV(this IEnumerable<string> values)
        {
            string result = string.Join(Comma, values);
            return result;
        }
    }
}
