using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FASTLogToXml
{
    public static class EnumerableExtensions
    {
        // similar to System.Linq.Enumerable.Sum: public static int Sum(this IEnumerable<ulong> source)
        public static ulong Sum(this IEnumerable<ulong> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
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
    }
}
