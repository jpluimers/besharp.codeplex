using System;
using System.Collections.Generic;
using System.Linq;

using BeSharp.Generic;

namespace BeSharp.Collections.Generic
{
    // based on http://stackoverflow.com/questions/4554455/hashtable-with-mutiple-values-for-single-key/4554628#4554628
    [Obsolete("TODO ##jwp: this class is not yet at BeSharp.net coding standards, but required in one of our projects. Until this Obsolete attribute is removed: use with extreme care.")]
    public class MultiDictionary<TKey, TValue>: IEnumerable<KeyValuePair<TKey, List<TValue>>>
    {
        public MultiDictionary()
        {
        }

        public MultiDictionary(MultiDictionary<TKey, TValue> multiDictionary): this()
        {
            if (multiDictionary == null)
            {
                throw new ArgumentNullException(Reflector.GetName(new { multiDictionary }));
            }
            foreach (KeyValuePair<TKey, List<TValue>> keyValuePairTKey in multiDictionary)
            {
                data.Add(keyValuePairTKey.Key, keyValuePairTKey.Value);
            }
        }

        public void Add(TKey key, TValue value)
        {
            Entry entry = this[key];
            entry.Add(value);
        }

        public bool Contains(TKey key, TValue value)
        {
            Entry entry = this[key];
            bool result = entry.Contains(value);
            return result;
        }

        public int Count
        {
            get
            {
                return data.Count;
            }
        }

        private readonly Dictionary<TKey, List<TValue>> data = new Dictionary<TKey, List<TValue>>();

        public struct Entry : IEnumerable<TValue>
        {
            private readonly MultiDictionary<TKey, TValue> mDictionary;

            public TKey Key { get; private set; }

            public bool IsEmpty
            {
                get
                {
                    return !mDictionary.data.ContainsKey(Key);
                }
            }

            public void Add(TValue value)
            {
                List<TValue> list;
                if (!mDictionary.data.TryGetValue(Key, out list))
                    list = new List<TValue>();
                list.Add(value);
                mDictionary.data[Key] = list;
            }

            public bool Remove(TValue value)
            {
                List<TValue> list;
                if (!mDictionary.data.TryGetValue(Key, out list))
                    return false;
                bool result = list.Remove(value);
                if (list.Count == 0)
                    mDictionary.data.Remove(Key);
                return result;
            }

            public void Clear()
            {
                mDictionary.data.Remove(Key);
            }

            internal Entry(MultiDictionary<TKey, TValue> dictionary, TKey key)
                : this()
            {
                Key = key;
                mDictionary = dictionary;
            }

            public IEnumerator<TValue> GetEnumerator()
            {
                List<TValue> list;
                if (!mDictionary.data.TryGetValue(Key, out list))
                    return Enumerable.Empty<TValue>().GetEnumerator();
                else
                    return list.GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        public Entry this[TKey key]
        {
            get
            {
                return new Entry(this, key);
            }
        }

        #region IEnumerable<KeyValuePair<TKey,List<TValue>>> Members

        IEnumerator<KeyValuePair<TKey, List<TValue>>> IEnumerable<KeyValuePair<TKey, List<TValue>>>.GetEnumerator()
        {
            return data.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return data.GetEnumerator();
        }

        #endregion
    }
}
