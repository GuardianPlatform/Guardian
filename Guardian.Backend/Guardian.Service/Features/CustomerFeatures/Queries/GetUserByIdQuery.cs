using Guardian.Domain.Entities;
using Guardian.Persistence;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Guardian.Service.Features.CustomerFeatures.Queries
{
    public class GetUserByIdQuery : IRequest<User>
    {
        public int Id { get; set; }
        public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
        {
            private readonly IApplicationDbContext _context;
            public GetUserByIdQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
            {
                var customer = _context.Users.Where(a => a.Id == request.Id).FirstOrDefault();
                if (customer == null) return null;
                return customer;
            }
        }
    }
}
