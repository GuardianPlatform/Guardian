using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Guardian.Domain.Enum;
using Guardian.Domain.Models;
using Guardian.Service.Features.Customer.Queries;
using Guardian.Service.Features.Game.Commands;
using Guardian.Service.Features.Game.Queries;

namespace Guardian.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/Games")]
    [ApiVersion("1.0")]
    public class GameController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll([FromQuery] PagiantionModel pagination)
        {
            var result = await Mediator.Send( new GetAllGamesQuery { Pagination = pagination });

            return Ok(result);
        }

        [Authorize(Roles = "Moderator")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id )
        {
            var result = await Mediator.Send(new GetGameQuery() { Id = id});

            return Ok(result);
        }

        [HttpPost]
       // [Authorize]
        public async Task<IActionResult> Create(CreateGameCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
       // [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await Mediator.Send(new DeleteGameCommand { Id = id }));
        }

        [HttpPut("{id}")]
       // [Authorize]        
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
