using Guardian.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Guardian.Service.Features.CustomerFeatures.Commands
{
    public class DeleteUserByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
        public class DeleteUserByIdCommandHandler : IRequestHandler<DeleteUserByIdCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public DeleteUserByIdCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
            {
                var customer = await _context.Users.Where(a => a.Id == request.Id).FirstOrDefaultAsync();
                if (customer == null) return default;
                _context.Users.Remove(customer);
                await _context.SaveChangesAsync();
                return customer.Id;
            }
        }
    }
}
