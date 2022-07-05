using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Guardian.Infrastructure.Database;
using Guardian.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Guardian.Service.Features.Category.Commands
{
    public class CreateCategoryCommand : IRequest<Domain.Entities.Category>
    {
        public string CategoryName { get; set; }

        public CreateCategoryCommand(string categoryName)
        {
            CategoryName = categoryName;
        }

        public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Domain.Entities.Category>
        {
            private readonly IApplicationDbContext _context;
            public CreateCategoryCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Domain.Entities.Category> Handle(CreateCategoryCommand request,
                CancellationToken cancellationToken)
            {
                var exist = await _context.Categories.AnyAsync(x => x.CategoryName == request.CategoryName, cancellationToken);
                if (exist)
                    throw new Exception("Category with given name already exists");

                var category = _context.Categories.Add(new Domain.Entities.Category()
                {
                    CategoryName = request.CategoryName,
                    Games = new List<Domain.Entities.Game>()
                });

                await _context.SaveChangesAsync();
                return category.Entity;
            }
        }
    }
}
