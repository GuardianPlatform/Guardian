using Guardian.Domain.Entities;
using Guardian.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Guardian.Service.Features.CustomerFeatures.Queries
{
    public class GetAllCustomerQuery : IRequest<IEnumerable<Customer>>
    {
        public int PageSize { get; set; }
        public int Page { get; set; }



        public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, IEnumerable<User>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllCustomerQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<Customer>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
            {
                var customerList = await _context.Users.ToListAsync();
                var customerList = await _context.Customers
                    .Skip(request.PageSize * request.Page)
                    .Take(request.PageSize)
                    .ToListAsync();

                if (customerList == null)
                {
                    return null;
                }

                return customerList.AsReadOnly();
            }
        }
    }
}
