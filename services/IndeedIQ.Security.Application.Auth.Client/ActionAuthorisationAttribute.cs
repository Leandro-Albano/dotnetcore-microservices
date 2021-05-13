
using Microsoft.AspNetCore.Mvc;

namespace IndeedIQ.Security.Application.Client
{
    public class ActionAuthorisationAttribute : TypeFilterAttribute
    {
        public ActionAuthorisationAttribute(string resourceName, string action) : base(typeof(ActionAuthorisationFilter))
            => this.Arguments = new[] { resourceName, action };
    }
}
