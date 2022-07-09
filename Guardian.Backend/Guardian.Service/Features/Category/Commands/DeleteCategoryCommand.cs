using System.Threading;
using System.Threading.Tasks;
using Guardian.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Guardian.Service.Features.Category.Commands
{
    public class DeleteCategoryCommand : IRequest<string>
    {
        public int Id { get; set; }
        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, string>
        {
            private readonly IApplicationDbContext _context;
            public DeleteCategoryCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            
            public async Task<string> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            {
                var category = await _context.Categories
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (category == null) 
                    return default;

                _context.Categories.Remove(category);                
                await _context.SaveChangesAsync();
                
                return category.Id.ToString();
            }
        }
    }
}
