using Guardian.Persistence;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Guardian.Infrastructure.Database;

namespace Guardian.Service.Features.CustomerFeatures.Commands
{
    public class UpdateUserCommand : IRequest<string>
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, string>
        {
            private readonly IApplicationDbContext _context;
            public UpdateUserCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<string> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                var cust = _context.Users.FirstOrDefault(a => a.Id == request.Id);

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
