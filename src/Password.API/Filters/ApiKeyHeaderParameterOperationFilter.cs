using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace Password.API.Filters
{
    public class ApiKeyHeaderParameterOperationFilter : IOperationFilter
    {
        private const string API_KEY_NAME = "x-api-key";

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = API_KEY_NAME,
                In = ParameterLocation.Header,
                Description = "API Key",
                Required = true
            });
        }
    }
}
