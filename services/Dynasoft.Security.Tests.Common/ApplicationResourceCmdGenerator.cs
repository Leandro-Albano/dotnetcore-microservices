using Bogus;

using Dynasoft.Security.Application.Contracts.ApplicationResource;
using Dynasoft.Security.Domain.Contracts.Common;
using Dynasoft.Security.Domain.Entities;
using Dynasoft.Security.Domain.Entities.ResourceAggregate.Commands;

namespace Dynasoft.Security.Tests.Common
{
    public static class ApplicationResourceCmdGenerator
    {
        public static CreateApplicationResourceCommand CreateApplicationResourceCommand => new Faker<CreateApplicationResourceCommand>()
            .RuleFor(o => o.Name, f => f.Commerce.Department())
            .RuleFor(o => o.ApplicationLevel, f => f.PickRandomWithout(ApplicationLevel.NotSet)).Generate();

        public static UpdateApplicationResourceCommand UpdateApplicationResourceCommand => new Faker<UpdateApplicationResourceCommand>()
            .RuleFor(o => o.Name, f => f.Random.String2(10, 40))
            .RuleFor(o => o.ApplicationLevel, f => f.PickRandomWithout(ApplicationLevel.NotSet)).Generate();

        public static AddResourceActionCommand AddResourceActionCommand => new Faker<AddResourceActionCommand>()
            .RuleFor(o => o.Name, f => f.Random.String2(10, 25)).Generate();

        public static UpdateResourceActionCommand UpdateResourceActionCommand => new Faker<UpdateResourceActionCommand>()
            .RuleFor(o => o.Name, f => f.Name.JobTitle()).Generate();

        public static UpdateResourceActionApplicationCommand UpdateResourceActionApplicationCommand => new Faker<UpdateResourceActionApplicationCommand>()
            .RuleFor(o => o.Name, f => f.Name.JobTitle()).Generate();

    }
}
