using System.Collections.Generic;
using System.Linq;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Guardian.Infrastructure.Database;
using System.Collections;
using System;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Guardian.Service.Features.Game.Commands
{
    public class CreateGameCommand : IRequest<string>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string License { get; set; }
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

                _context.Games.Add(game);
                await _context.SaveChangesAsync();

                await AddCategories(request, game);
                await _context.SaveChangesAsync();

                return game.Id.ToString();
            }

            private async Task AddCategories(CreateGameCommand request, Domain.Entities.Game game)
            {
                var categories = _context.Categories
                    .Include(x=>x.Games)
                    .Where(c => request.CategoryIds.Contains(c.Id))
                    .ToList();

                foreach (var category in categories)
                {
                    category.Games ??= new List<Domain.Entities.Game>();
                    category.Games.Add(game);
                }
            }
        }
    }
}