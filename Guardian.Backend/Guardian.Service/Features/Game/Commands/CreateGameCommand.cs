using System.Collections.Generic;
using System.Linq;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Guardian.Infrastructure.Database;
using System;
using Guardian.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Guardian.Service.Features.Game.Commands
{
    public class CreateGameCommand : IRequest<string>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string License { get; set; }
        public string ImageUrl { get; set; }

        public List<int> CategoryIds { get; set; }

        public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, string>
        {
            private readonly IApplicationDbContext _context;
            public CreateGameCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(CreateGameCommand request, CancellationToken cancellationToken)
            {
                var exist = await _context.Games.AnyAsync(x => x.Name == request.Name, cancellationToken);
                if (exist)
                    throw new Exception("Game with given name already exists");

                var game = new Domain.Entities.Game();
                game.Name = request.Name;
                game.Description = request.Description;
                game.Author = request.Author;
                game.License = request.License;
                game.ImageUrl = request.ImageUrl;
                game.GameCategories = new List<GameCategory>();

                var categoriesToAdd = _context.Categories
                    .AsNoTracking()
                    .Where(x => request.CategoryIds.Contains(x.Id)).ToList();

                game.GameCategories = categoriesToAdd
                    .Select(categoryId => new GameCategory() { CategoryId = categoryId.Id, Game = game })
                    .ToList();

                _context.Games.Add(game);
                await _context.SaveChangesAsync();



                return game.Id.ToString();
            }
        }
    }
}