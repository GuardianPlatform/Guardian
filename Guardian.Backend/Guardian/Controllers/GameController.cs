using Guardian.Service.Features.CustomerFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Guardian.Domain.Entities;
using System;
using Guardian.Persistence;

namespace Guardian.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/v{version:apiVersion}/Games")]
    [ApiVersion("1.0")]
    public class GameController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll([FromQuery] PagiantionParams pagination)
        {
            var result = await Mediator.Send( new GetAllGamesQuery { Pagiantion = pagination });

            return Ok(result);
        }

    }
}
