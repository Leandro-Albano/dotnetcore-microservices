using System.Collections.Generic;

namespace IndeedIQ.Common.Util
{
    public readonly struct HashCodeHelper
    {
        private readonly int value;

        public HashCodeHelper(int value) => this.value = value;

        public static HashCodeHelper Start { get; } = new HashCodeHelper(17);

        public static implicit operator int(HashCodeHelper hash) => hash.value;

        public HashCodeHelper Hash<T>(T obj)
        {
            var h = EqualityComparer<T>.Default.GetHashCode(obj);
            return unchecked(new HashCodeHelper((this.value * 31) + h));
        }

        public override int GetHashCode() => this.value;
    }
}
