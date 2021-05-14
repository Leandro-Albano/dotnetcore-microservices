namespace Dynasoft.Common.Domain.Contracts.Exceptions
{
    public class EntityNotFoundException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="EntityNotFoundException"/> class.
        /// </summary>
        /// <param name="entityName">The entity type name.</param>
        /// <param name="id">The id that was not found in the database.</param>
        public EntityNotFoundException(string entityName, long id)
            : base(ExceptionCode.EntityNotFoundException, DomainMessages.ENTITY_NOT_FOUND, entityName, $"id: {id}")
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="EntityNotFoundException"/> class.
        /// </summary>
        /// <param name="entityName">The entity type name.</param>
        /// <param name="criteria">The criteria used to match the entity.</param>
        public EntityNotFoundException(string entityName, string criteria)
            : base(ExceptionCode.EntityNotFoundException, DomainMessages.ENTITY_NOT_FOUND, entityName, criteria)
        {
        }

    }
}
