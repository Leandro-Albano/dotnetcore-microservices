using Dynasoft.Common.Domain.Entities;

namespace Dynasoft.Security.Domain.Entities.ResourceAggregate
{
    public class ResourceAction : Entity<ResourceAction>
    {
        public ApplicationResource Resource { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// A composition of the resource name and the action name.
        /// e.g.: ResourceName_ActionName.
        /// </summary>
        /// <remarks>
        /// This property requires <see cref="Resource"/> to be loaded, otherwise it returns null.
        /// </remarks>
        public string FullName { get; set; }
        public override int GetHashCode() => 1;
        public override string ToString() => $"{nameof(ResourceAction)}: {this.Id}-{this.Name}";
    }
}
