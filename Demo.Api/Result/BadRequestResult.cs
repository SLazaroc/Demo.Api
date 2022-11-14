using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.Result
{
    public class BadRequestResult : IActionResult
    {
        public readonly Error error = new();
        public BadRequestResult(
            string code, 
            string message)
        {
            error.Code = code;
            error.Message = message;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            BadRequest _errorContent = new()
            {
                Error = error
            };

            var objectResult = new ObjectResult(_errorContent)
            {
                StatusCode = StatusCodes.Status400BadRequest
            };

            await objectResult.ExecuteResultAsync(context);
        }
    }

    public class Error
    {
        public string? Code { get; set; }
        public string? Message { get; set; }
    }

    public class BadRequest
    {
        public Error? Error { get; set; }
    }
}
