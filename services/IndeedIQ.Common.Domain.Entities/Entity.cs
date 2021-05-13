using IndeedIQ.Common.Util;

using System;

namespace IndeedIQ.Common.Domain.Entities
{
    /// <summary>
    /// Base entity class.
    /// </summary>
    public abstract class Entity<T> : EquatableObject<T>, IEntity where T : Entity<T>
    {
        /// <summary>
        /// Entity's id.
        /// </summary>
        public long Id { get; protected set; }

        /// <inheritdoc/>
        public abstract override string ToString();

        /// <inheritdoc/>
        protected override bool IsEqualTo(T other) => this.Id == other.Id && this.Id != 0;

        /// <inheritdoc/>
        public override int GetHashCode()
            => HashCode.Combine(this.Id);

    }

}
