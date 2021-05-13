using IndeedIQ.Security.Domain.Entities.ResourceAggregate;
using IndeedIQ.Security.Tests.Common;

using Xunit;

namespace IndeedIQ.Security.Tests.Unit.Domain.Entities.ResourceAggregate
{
    public class ResourceActionTests
    {
        [Fact]
        public void ToString_ShouldReturnCorrectFormat()
        {
            var createResourceCmd = ApplicationResourceCmdGenerator.CreateApplicationResourceCommand;
            var resource = ApplicationResource.Create(createResourceCmd);
            var addActionCmd = ApplicationResourceCmdGenerator.AddResourceActionCommand;
            var action = resource.AddResourceAction(addActionCmd);
            Assert.Equal($"{nameof(ResourceAction)}: {action.Id}-{action.Name}", action.ToString());
        }

        [Fact]
        public void FullName_ShouldReturnCorrectFormat()
        {
            var createResourceCmd = ApplicationResourceCmdGenerator.CreateApplicationResourceCommand;
            var resource = ApplicationResource.Create(createResourceCmd);
            var addActionCmd = ApplicationResourceCmdGenerator.AddResourceActionCommand;
            var action = resource.AddResourceAction(addActionCmd);
            Assert.Equal(action.FullName, action.FullName);
        }

        [Fact]
        public void GetHashCode_ShouldCalculateCorrectHashValue()
        {
            var resource = ApplicationResource.Create(ApplicationResourceCmdGenerator.CreateApplicationResourceCommand);
            var resource2 = ApplicationResource.Create(ApplicationResourceCmdGenerator.CreateApplicationResourceCommand);

            var addActionCmd = ApplicationResourceCmdGenerator.AddResourceActionCommand;
            var resource1action1 = resource.AddResourceAction(addActionCmd);
            var resource1action2 = resource2.AddResourceAction(addActionCmd);

            Assert.True(resource1action1.Name == resource1action2.Name && resource1action1.GetHashCode() == resource1action2.GetHashCode());
        }
    }
}
