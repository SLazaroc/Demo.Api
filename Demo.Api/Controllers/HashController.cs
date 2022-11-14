using Demo.Application.Hash.Queries;
using Demo.Domain.Hash;
using Demo.Domain.Model;
using Demo.Infrastructure.Exceptions;
using Demo.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Demo.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class HashController : BaseController
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<HashController> _logger;

        public HashController(
            IServiceScopeFactory serviceScopeFactory,
            ITraceLog log,
            IMediator mediator, 
            ILogger<HashController> logger) : base(log, mediator)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("hash/")]
        public async Task<IActionResult> GenerateHash(string password)
        {
            try
            {
                CreateHashQuery query = new CreateHashQuery()
                {
                    Password = password
                };

                HashResult result = await this.mediator.Send(query);
                return Ok(result.Id);
            }
            catch (CustomArgumentException cex)
            {
                return GetCustomErrorResult(cex.Code, cex.Message);
            }
            catch (Exception ex)
            {
                return GetErrorResult(ex);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("hash/{id}")]
        public async Task<IActionResult> GetHash(int id)
        {
            try
            {
                GetHashQuery query = new GetHashQuery()
                {
                    Id = id
                };

                GetHashResult result = await this.mediator.Send(query);
                return Ok(result.SH512);
            }
            catch (CustomArgumentException cex)
            {
                return GetCustomErrorResult(cex.Code, cex.Message);
            }
            catch (Exception ex)
            {
                return GetErrorResult(ex);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("hash/stats")]
        public async Task<IActionResult> GetStats()
        {
            try
            {
                GetStatsQuery query = new GetStatsQuery();

                HashStats result = await this.mediator.Send(query);
                return Ok(result);
            }
            catch (CustomArgumentException cex)
            {
                return GetCustomErrorResult(cex.Code, cex.Message);
            }
            catch (Exception ex)
            {
                return GetErrorResult(ex);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("hash/shutdown")]
        public async Task<IActionResult> Shutdown()
        {
            try
            {
                ShutdownQuery query = new ShutdownQuery() { IsShutdown = true };
                ShutdownResult result = await this.mediator.Send(query);
                return Ok(result.Message);
            }
            catch (CustomArgumentException cex)
            {
                return GetCustomErrorResult(cex.Code, cex.Message);
            }
            catch (Exception ex)
            {
                return GetErrorResult(ex);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("hash/start")]
        public async Task<IActionResult> Start()
        {
            try
            {
                ShutdownQuery query = new ShutdownQuery() { IsShutdown = false };
                ShutdownResult result = await this.mediator.Send(query);
                return Ok(result.Message);
            }
            catch (CustomArgumentException cex)
            {
                return GetCustomErrorResult(cex.Code, cex.Message);
            }
            catch (Exception ex)
            {
                return GetErrorResult(ex);
            }
        }
    }
}
