using Guardian.Persistence;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Guardian.Infrastructure.Database;

namespace Guardian.Service.Features.CustomerFeatures.Commands
{
    public class UpdateUserCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public UpdateUserCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                var cust = _context.Users.Where(a => a.Id == request.Id).FirstOrDefault();

                if (cust == null)
                {
                    return default;
                }
                else
                {
                    cust.Email = request.Email;
                    cust.Login = request.Login;
                    _context.Users.Update(cust);
                    await _context.SaveChangesAsync();
                    return cust.Id;
                }
            }
        }
    }
}
