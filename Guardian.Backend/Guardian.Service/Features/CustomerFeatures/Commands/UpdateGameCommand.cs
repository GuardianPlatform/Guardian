﻿using Guardian.Persistence;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Guardian.Infrastructure.Database;

namespace Guardian.Service.Features.CustomerFeatures.Commands
{
    public class UpdateGameCommand : IRequest<string>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string License { get; set; }
        public class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommand, string>
        {
            private readonly IApplicationDbContext _context;
            public UpdateGameCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<string> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
            {
                var game = _context.Games.FirstOrDefault(a => a.Id.ToString() == request.Id);

                if (game == null)
                {
                    return default;
                }
                else
                {
                    game.Name = request.Name;
                    game.Description = request.Description;
                    game.Author = request.Author;
                    game.License = request.License;
                    _context.Games.Update(game);
                    await _context.SaveChangesAsync();
                    return game.Id.ToString();
                }
            }
        }
    }
}