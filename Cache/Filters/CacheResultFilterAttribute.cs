using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.Extensions.Caching.Memory;

namespace Cache.Filters
{
    public class CacheResultFilterAttribute : Attribute, IAsyncResourceFilter
    {    
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            IMemoryCache _cache = context.HttpContext.RequestServices.GetService<IMemoryCache>();
            var key = context.HttpContext.Request.QueryString.Value.Split('=')[1];
            string? data = _cache.Get<string>(key);
            if (!string.IsNullOrEmpty(data))
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
                await context.HttpContext.Response.WriteAsync(data);
            }
            else
            {
                await next.Invoke();
            }
        }
    }
}
