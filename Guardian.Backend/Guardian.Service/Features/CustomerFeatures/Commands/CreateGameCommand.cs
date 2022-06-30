using Guardian.Domain.Entities;
using Guardian.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Guardian.Infrastructure.Database;

namespace Guardian.Service.Features.CustomerFeatures.Commands
{
    public class CreateGameCommand : IRequest<string>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string License { get; set; }

        public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, string>
        {
            private readonly IApplicationDbContext _context;
            public CreateGameCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<string> Handle(CreateGameCommand request, CancellationToken cancellationToken)
            {
                var game = new Game();
                game.Name = request.Name;
                game.Description = request.Description;
                game.Author = request.Author;
                game.License = request.License;

                _context.Games.Add(game);
                await _context.SaveChangesAsync();
                return game.Id.ToString();
            }
        }
    }
    
}

