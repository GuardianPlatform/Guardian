using Guardian.Domain.Entities;
using Guardian.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Guardian.Service.Features.CustomerFeatures.Commands
{
    public class CreateCustomerCommand : IRequest<int>
    {
        public string CustomerName { get; set; }
        public string ContactName { get; set; }

        public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public CreateCustomerCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
            {
                var customer = new Customer();
                customer.CustomerName = request.CustomerName;
                customer.ContactName = request.ContactName;

                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
                return customer.Id;
            }
        }
    }
}
