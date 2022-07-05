using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Guardian.Infrastructure.Database;
using MediatR;

namespace Guardian.Service.Features.Customer.Commands
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
                var user = _context.Users.FirstOrDefault(a => a.Id == request.Id);

                if (user == null)
                {
                    return default;
                }

                user.Email = request.Email;
                user.Login = request.Login;

                _context.Users.Update(user);
                
                await _context.SaveChangesAsync();
                return user.Id;
            }
        }
    }
}
