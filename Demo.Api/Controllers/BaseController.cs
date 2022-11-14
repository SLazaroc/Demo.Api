using Demo.Api.Result;
using Demo.Infrastructure.Common;
using Demo.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Security.Cryptography.Xml;

namespace Demo.Api.Controllers
{
    public class BaseController : Controller
    {
        private readonly ITraceLog logger;
        public readonly IMediator mediator;

        public BaseController(ITraceLog log, IMediator mediator)
        {
            this.logger = log;
            this.mediator = mediator;
        }

        protected IActionResult GetBadRequest(int? referenceTypeId = null, string referenceId = "0")
        {
            var message = ModelState.Values
                .SelectMany(x => x.Errors)
                .Select(c => c).First();

            string error = message.ErrorMessage;
            logger.LogError(referenceTypeId, referenceId, error);

            return new Result.BadRequestResult(Constants.Message.M1001[0], error);
        }

        protected IActionResult GetErrorResult(Exception ex, int? referenceTypeId = null, string referenceId = "0")
        {
            string error = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            logger.LogError(referenceTypeId, referenceId, error);
            return new Result.BadRequestResult(Constants.Message.M1001[0], Constants.Message.M1001[1]);
        }
        protected IActionResult GetCustomErrorResult(string code, string message)
        {
            logger.LogError(0, message, message);
            return new Result.BadRequestResult(code, message); 
        }
    }
}
