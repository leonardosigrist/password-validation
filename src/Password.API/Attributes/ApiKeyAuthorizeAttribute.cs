using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Password.API.Attributes
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAuthorizeAttribute : Attribute, IAsyncActionFilter
    {
        private const string API_KEY_NAME = "x-api-key";
        private const string API_KEY_CONFIGURATION_NAME = "Security:ApiKey";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(API_KEY_NAME, out var apiKeyFromHeader))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "API Key não fornecida"
                };
                return;
            }

            var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = appSettings.GetValue<string>(API_KEY_CONFIGURATION_NAME);

            if (!apiKey.Equals(apiKeyFromHeader))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "API Key inválida"
                };
                return;
            }

            await next();
        }
    }
}
