namespace IndeedIQ.Common.Domain.Contracts.Exceptions
{
    public class DomainMessages
    {
        public const string ENTITY_NOT_FOUND = "Cound not find a {0} with the the provided criteria: {1}";
        public const string INVALID_COMMAND = "The {0} validation failed, check 'ValidationErrors' property.";
        public const string ENTITY_IS_NOT_DELETABLE_OR_ARCHIVABLE = "The entity type {0} is not deletable or archivable.";
    }
}
