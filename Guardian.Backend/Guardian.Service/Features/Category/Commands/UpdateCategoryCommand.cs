using System.Collections.Generic;
using Guardian.Persistence;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Guardian.Infrastructure.Database;

namespace Guardian.Service.Features.Game.Commands
{
    public class UpdateCategoryCommand : IRequest<string>
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, string>
        {
            private readonly IApplicationDbContext _context;
            public UpdateCategoryCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<string> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
            {
                var category = _context.Categories.FirstOrDefault(a => a.Id == request.Id);

                if (category == null)
                {
                    return default;
                }

                category.CategoryName = request.CategoryName;

                _context.Categories.Update(category);
                await _context.SaveChangesAsync();
                return category.Id.ToString();
            }
        }
    }
}
