using Dynasoft.Common.Domain.Contracts;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dynasoft.Common.Domain.Entities
{
    /// <inheritdoc />
    public abstract class AggregateRoot<T> : Entity<T>, IAggregateRoot, IEventEmitter where T : Entity<T>
    {
        private readonly List<DomainEvent> events = new List<DomainEvent>();

        /// <inheritdoc />
        [NotMapped]
        public IReadOnlyCollection<DomainEvent> Events => this.events.AsReadOnly();

        /// <summary>
        /// Add new event to the session of this entity.
        /// </summary>
        /// <param name="event"></param>
        protected void AddEvent(DomainEvent @event) => this.events.Add(@event);
    }
}
