using Guardian.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Guardian.Service.Features.CustomerFeatures.Commands
{
    public class DeleteUserByIdCommand : IRequest<string>
    {
        public string Id { get; set; }
        public class DeleteUserByIdCommandHandler : IRequestHandler<DeleteUserByIdCommand, string>
        {
            private readonly IApplicationDbContext _context;
            public DeleteUserByIdCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<string> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
            {
                var customer = await _context.Users
                    .Where(a => a.Id == request.Id)
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);

                if (customer == null) return default;
                _context.Users.Remove(customer);
                await _context.SaveChangesAsync();
                return customer.Id;
            }
        }
    }
}
