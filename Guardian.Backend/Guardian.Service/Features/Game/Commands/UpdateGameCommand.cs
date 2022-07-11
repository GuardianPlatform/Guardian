using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Guardian.Infrastructure.Database;
using System.Collections.Generic;
using Guardian.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Guardian.Service.Features.Game.Commands
{
    public class UpdateGameCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string License { get; set; }
        public string ImageUrl { get; set; }
        public List<int> CategoryIds { get; set; }

        public class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public UpdateGameCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
            {
                var game = _context.Games.FirstOrDefault(a => a.Id == request.Id);

                if (game == null)
                {
                    return default;
                }

                game.Name = request.Name;
                game.Description = request.Description;
                game.Author = request.Author;
                game.License = request.License;
                game.ImageUrl = request.ImageUrl;

                var categoriesToAdd = _context.Categories
                    .AsNoTracking()
                    .Where(x => request.CategoryIds.Contains(x.Id))
                    .ToList();

                game.GameCategories = categoriesToAdd
                    .Select(categoryId => new GameCategory() { CategoryId = categoryId.Id, Game = game })
                    .ToList();

                _context.Games.Update(game);
                await _context.SaveChangesAsync();
                return game.Id;
            }
        }
    }
}
