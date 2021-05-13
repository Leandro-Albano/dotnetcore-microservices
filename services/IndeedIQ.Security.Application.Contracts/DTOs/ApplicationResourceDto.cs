using IndeedIQ.Security.Domain.Contracts.Common;

using System.Collections.Generic;

namespace IndeedIQ.Security.Application.Contracts.DTOs
{
    public class ApplicationResourceDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ResourceActionDto> AvailableActions { get; set; }
        public ApplicationLevel ApplicationLevel { get; set; }
    }
}
