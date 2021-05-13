namespace IndeedIQ.Common.Domain.Contracts.Exceptions
{
    public class NonDeletableOrArchivableEntityException : DomainException
    {
        public NonDeletableOrArchivableEntityException(string entityTypeName)
            : base(ExceptionCode.NonDeletableOrArchivableEntity, null, DomainMessages.INVALID_COMMAND, entityTypeName) { }
    }
}
