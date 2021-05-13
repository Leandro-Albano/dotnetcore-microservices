using System.Collections.Generic;

namespace IndeedIQ.Security.Application.Client
{
    //public class AccountLevelResourceActionFilter : IAsyncActionFilter
    //{
    //    //public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    //    //{
    //    //    //var multiAccountRequests = context.ActionArguments.Values.OfType<IMultiAccountResourceActionRequest>();
    //    //    //var user = context.HttpContext.User.Identities.FirstOrDefault(id => id.ty == ClaimTypes.Role && c.Value == );

    //    //    //foreach (var item in multiAccountRequests)
    //    //    //{
    //    //    //    if (item.Accounts == null || item.Accounts.Count == 0)
    //    //    //    {
    //    //    //        item.Accounts.Add()
    //    //    //    }

    //    //    //    context.HttpContext.Response.StatusCode == (int)HttpStatusCode.RequestedRangeNotSatisfiable;
    //    //    //}

    //    //    //var multiAccountRequests = context.ActionArguments.Values.OfType<ISingleAccountResourceActionRequest>();
    //    //    //return next();
    //    //}
    //}

    internal interface ISingleAccountResourceActionRequest
    {
    }

    public interface IMultiAccountResourceActionRequest
    {
        public List<long> Accounts { get; set; }
    }
}
