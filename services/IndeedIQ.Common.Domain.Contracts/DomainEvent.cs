namespace IndeedIQ.Common.Domain.Contracts
{
    public abstract class DomainEvent : IDomainEvent
    {
        private readonly IEventEmitter emitter;

        private DomainEvent()
        {

        }

        public DomainEvent(IEventEmitter emitter)
        {
            this.emitter = emitter;
            this.Metadata = new EventMetadata
            {
                EventName = this.GetType().Name
            };
        }

        private long id;
        public long Id
        {
            get => this.emitter?.Id ?? this.id;
            set => this.id = value;
        }

        public EventMetadata Metadata { get; set; }
    }

    public interface IEventEmitter
    {
        public long Id { get; }
    }
}
