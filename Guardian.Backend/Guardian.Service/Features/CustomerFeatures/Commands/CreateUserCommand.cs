﻿using Guardian.Domain.Entities;
using Guardian.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Guardian.Service.Features.CustomerFeatures.Commands
{
    public class CreateUserCommand : IRequest<int>
    {
        public string Email { get; set; }
        public string Login { get; set; }
        
        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public CreateUserCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
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
