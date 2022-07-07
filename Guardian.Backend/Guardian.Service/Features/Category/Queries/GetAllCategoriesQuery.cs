using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Guardian.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Guardian.Service.Features.Category.Queries
{
     public class GetAllCategoriesQuery : IRequest<IEnumerable<Domain.Entities.Category>>
    {
        public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<Domain.Entities.Category>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllCategoriesQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Domain.Entities.Category>> Handle(GetAllCategoriesQuery request,
                CancellationToken cancellationToken)
            {
                var customerList = await _context.Categories
                    .Include(x=>x.Games)
                    .ToListAsync(cancellationToken);
                return customerList?.AsReadOnly();
            }
        }
    }
}
