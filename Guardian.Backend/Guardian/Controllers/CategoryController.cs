using System;
using System.Linq;
using System.Threading.Tasks;
using Guardian.Service.Features.Category.Commands;
using Guardian.Service.Features.Category.Queries;
using Guardian.Service.Features.Product.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Guardian.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CategoryController : ControllerBase
    {

        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        /// <summary>
        /// Get products for given category. Paginated + sorted managed by parameters.
        /// Limit for pageSize is 100, default is set to 10
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpGet("/products/{category}")]
        public async Task<IActionResult> GetProductsForCategory(string category, int pageSize, int page)
        {
            var result = await Mediator.Send(new GetAllProductsForCategoryQuery(category));
            return Ok(result);
        }

        /// <summary>
        /// Get list of all categories
        /// </summary>
        /// <returns></returns>
        [HttpGet("/list")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetAllCategoriesQuery());
            return Ok(result);
        }


        /// <summary>
        /// Add a new category
        /// </summary>
        /// <returns></returns>
        [HttpPost("/category")]
        public async Task<IActionResult> Post([FromBody]CreateCategoryCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}