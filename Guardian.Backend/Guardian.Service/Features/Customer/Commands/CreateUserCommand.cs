using System.Threading;
using System.Threading.Tasks;
using Guardian.Domain.Entities;
using Guardian.Infrastructure.Database;
using MediatR;

namespace Guardian.Service.Features.Customer.Commands
{
    public class CreateUserCommand : IRequest<string>
    {
        public string Email { get; set; }
        public string Login { get; set; }
        
        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
        {
            private readonly IApplicationDbContext _context;
            public CreateUserCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var customer = new User();
                customer.Email = request.Email;
                customer.Login = request.Login;

                _context.Users.Add(customer);
                await _context.SaveChangesAsync();

                return customer.Id;
            }
        }
    }
}
