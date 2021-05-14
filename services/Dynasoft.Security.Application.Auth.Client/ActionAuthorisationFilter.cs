using Dynasoft.Security.Domain.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using System.Threading.Tasks;

namespace Dynasoft.Security.Application.Client
{
    public class ActionAuthorisationFilter : IAsyncAuthorizationFilter
    {
        private readonly string resourceName;
        private readonly string actionName;
        private readonly ISecurityDataContext dataContext;

        public ActionAuthorisationFilter(string resourceName, string actionName, ISecurityDataContext dataContext)
        {
            this.resourceName = resourceName;
            this.actionName = actionName;
            this.dataContext = dataContext;
        }

        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User == null)
                context.Result = new ForbidResult();

            return Task.CompletedTask;
        }
    }
}
