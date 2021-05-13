
using System;

namespace IndeedIQ.Common.Util
{
    public abstract class EquatableObject<T> : IEquatable<T>
    {
        public static bool operator ==(EquatableObject<T> left, EquatableObject<T> right)
            => !(left is null ^ right is null) && (left is null || left.Equals(right));

        public static bool operator !=(EquatableObject<T> left, EquatableObject<T> right) => !(left == right);

        /// <inheritdoc/>
        public override bool Equals(object obj)
            => obj != null && obj.GetType() == this.GetType() && (ReferenceEquals(this, obj) || this.IsEqualTo((T)obj));

        /// <inheritdoc/>
        public bool Equals(T other)
            => other != null && (ReferenceEquals(this, other) || this.IsEqualTo(other));

        /// <summary>
        /// Additional checks for equality.
        /// </summary>
        /// <param name="other">The other object to be compared.</param>
        /// <returns><see cref="true"/> is <paramref name="other"/> is equal to this instance; otherwise, <see cref="false"/>.</returns>
        protected abstract bool IsEqualTo(T other);

        /// <inheritdoc/>
        public abstract override int GetHashCode();

    }
}
