
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

using System;

namespace IndeedIQ.Common.Api.Controllers
{
    public abstract class AbstractApiController : ControllerBase
    {
        protected CreatedResult Created([ActionResultObjectValue] object id)
        {
            string url = this.HttpContext?.Request?.GetDisplayUrl();

            Uri path = new Uri(new Uri(url), id.ToString());
            return this.Created(path, id);
        }
    }
}
