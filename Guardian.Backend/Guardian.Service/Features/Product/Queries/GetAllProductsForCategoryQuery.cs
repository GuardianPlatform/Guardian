using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Guardian.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Guardian.Service.Features.Product.Queries
{
    public class GetAllProductsForCategoryQuery : IRequest<IEnumerable<Domain.Entities.Product>>
    {
        public string Category { get; set; }

        public GetAllProductsForCategoryQuery(string category)
        {
            Category = category;
        }

        public class GetAllCustomerQueryHandler : IRequestHandler<GetAllProductsForCategoryQuery, IEnumerable<Domain.Entities.Product>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllCustomerQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Domain.Entities.Product>> Handle(GetAllProductsForCategoryQuery request,
                CancellationToken cancellationToken)
            {
                var category = await _context.Categories
                    .FirstOrDefaultAsync(x => x.CategoryName == request.Category, cancellationToken);

                return category.Products?.AsReadOnly();
            }
        }
    }
}