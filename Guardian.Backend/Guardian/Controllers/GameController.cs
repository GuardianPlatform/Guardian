using Guardian.Service.Features.CustomerFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Guardian.Domain.Entities;
using Guardian.Service.Features.CustomerFeatures.Commands;
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

        [HttpPost]
        public async Task<IActionResult> Create(CreateGameCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await Mediator.Send(new DeleteGameCommand { Id = id }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateGameCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }
    }
}
