using System.Collections.Generic;
using System.Linq;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Guardian.Infrastructure.Database;
using System.Collections;
using System;
using Microsoft.EntityFrameworkCore;

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
                    throw new Exception("Category with given name already exists");

                var game = new Domain.Entities.Game();
                game.Name = request.Name;
                game.Description = request.Description;
                game.Author = request.Author;
                game.License = request.License;

                var categories = _context.Categories
                    .Where(c => request.CategoryIds.Contains(c.Id))
                    .ToList();

                game.Categories = categories;

                _context.Games.Add(game);
                await _context.SaveChangesAsync();
                return game.Id.ToString();
            }
        }
    }
}