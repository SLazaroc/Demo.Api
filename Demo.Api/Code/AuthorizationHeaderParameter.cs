using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Demo.Api.Code
{
    public class AuthorizationHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            operation.Parameters.Add(
                new OpenApiParameter()
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Required = true,
                    Description = "JWT Bearer token for OAuth authentication using the bearer scheme. Example: \"bearer {token}\" (eg. 123)",
                    Schema = new OpenApiSchema()
                    {
                        Type = "string"
                    }
                });
            operation.Parameters.Add(
                new OpenApiParameter()
                {
                    Name = "ApiVersion",
                    In = ParameterLocation.Header,
                    Required = false,
                    Description = "Version number in #.# format (eg. 1.0)",
                    Schema = new OpenApiSchema()
                    {
                        Type = "string",
                        Pattern = @"\d+(?:\.\d+)+"
                    }
                });
        }
    }
}
