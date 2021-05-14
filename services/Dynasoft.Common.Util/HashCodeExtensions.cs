using System;
using System.Collections.Generic;

namespace Dynasoft.Common.Util
{
    public static class HashCodeExtensions
    {
        public static HashCode AddValue<T>(this HashCode hash, T value)
        {
            hash.Add(value);
            return hash;
        }

        public static HashCode AddValue<T>(this HashCode hash, T value, IEqualityComparer<T> comparer)
        {
            hash.Add(value, comparer);
            return hash;
        }
    }
}
