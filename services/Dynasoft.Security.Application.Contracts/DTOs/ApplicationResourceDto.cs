using Dynasoft.Security.Domain.Contracts.Common;

using System.Collections.Generic;

namespace Dynasoft.Security.Application.Contracts.DTOs
{
    public class ApplicationResourceDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ResourceActionDto> AvailableActions { get; set; }
        public ApplicationLevel ApplicationLevel { get; set; }
    }
}
