using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Guardian.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get list of all categories
        /// </summary>
        /// <returns></returns>
        [HttpGet("/list")]
        public async Task<IActionResult> GetList()
        {
                await Mediator.Send()
        }


        /// <summary>
        /// Add a new category
        /// </summary>
        /// <returns></returns>
        [HttpPost("/category")]
        public async Task<IActionResult> Post([FromBody]string categoryName)
        {
            throw new NotImplementedException();
        }


        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            throw new NotImplementedException();
        }
    }
}