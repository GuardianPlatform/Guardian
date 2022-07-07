using System.Threading.Tasks;
using Guardian.Domain.Models;
using Guardian.Service.Features.Category.Commands;
using Guardian.Service.Features.Category.Queries;
using Guardian.Service.Features.Game.Commands;
using Guardian.Service.Features.Product.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Guardian.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/Categories")]
    [ApiVersion("1.0")]
    public class CategoryController : ControllerBase
    {

        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        /// <summary>
        /// Get products for given category. Paginated + sorted managed by parameters.
        /// Limit for pageSize is 100, default is set to 10
        /// </summary>
        /// <param name="category">Category for which you need to fetch games</param>
        /// <returns></returns>
        [HttpGet("games/{category}")]
        public async Task<IActionResult> GetProductsForCategory(string category, 
            [FromQuery] PagiantionModel pagination)
        {
            var result = await Mediator.Send(new GetAllGamesForCategoryQuery(category, pagination));
            return Ok(result);
        }

        /// <summary>
        /// Get list of all categories
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllCategoriesQuery());
            return Ok(result);
        }

        /// <summary>
        /// Add a new category
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> Create([FromBody]CreateCategoryCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Delete category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
       // [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteCategoryCommand() { Id = id }));
        }

        /// <summary>
        /// Update category by id. For adding or deleting a game's category use the game controller.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        //[Authorize]
        public async Task<IActionResult> Update(int id, UpdateCategoryCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            
            return Ok(await Mediator.Send(command));
        }
    }
}