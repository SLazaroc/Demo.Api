using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Demo.Api.Code
{
    public class ApiVersionHeaderParameter : IOperationFilter
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
                    Name = "ApiVersion",
                    In = ParameterLocation.Header,
                    Required = true,
                    Description = "Version number in #.# format",
                    Schema = new OpenApiSchema()
                    {
                        Type = "string",
                        Pattern = @"\d+(?:\.\d+)+"
                    }
                });
        }
    }
}
