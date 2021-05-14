
using Microsoft.AspNetCore.Mvc;

namespace Dynasoft.Security.Application.Client
{
    public class ActionAuthorisationAttribute : TypeFilterAttribute
    {
        public ActionAuthorisationAttribute(string resourceName, string action) : base(typeof(ActionAuthorisationFilter))
            => this.Arguments = new[] { resourceName, action };
    }
}
