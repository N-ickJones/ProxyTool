using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyVenvApi.Filters
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAuthAttribute : Attribute, IAsyncActionFilter
    {
        private const string _ApiKey = "ApiKey";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string ApiKey = _ApiKey;

            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKey, out var potentialApiKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            IConfiguration configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            string _apiKey = configuration.GetValue<string>(key: ApiKey);

            if (!_apiKey.Equals(potentialApiKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}
