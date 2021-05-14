namespace Dynasoft.Common.Domain.Entities
{

    /// <summary>
    /// Represents an archivable entity.
    /// </summary>
    public interface IArchivableEntity : IEntity
    {
        /// <summary>
        /// Indicate if this entity is archived.
        /// </summary>
        bool Archived { get; set; }

        /// <summary>
        /// Performe the required actions to archive the entity.
        /// Once all actions are successfully performed <see cref="Archived"/> should be set to <see cref="true"/>.
        /// </summary>
        void Archive();

        /// <summary>
        /// Performe the required actions to unarchive the entity.
        /// Once all actions are successfully performed <see cref="Archived"/> should be set to <see cref="false"/>.
        /// </summary>
        void Unarchive();
    }
}
