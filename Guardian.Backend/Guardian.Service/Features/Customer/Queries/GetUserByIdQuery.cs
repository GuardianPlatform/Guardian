using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Guardian.Domain.Entities;
using Guardian.Infrastructure.Database;
using MediatR;

namespace Guardian.Service.Features.Customer.Queries
{
    public class GetUserByIdQuery : IRequest<User>
    {
        public string Id { get; set; }
        public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
        {
            private readonly IApplicationDbContext _context;
            public GetUserByIdQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
            {
                var customer = _context.Users.FirstOrDefault(a => a.Id == request.Id);
                return customer;
            }
        }
    }
}
