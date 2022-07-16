using Guardian.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Guardian.Service.Features.Game.Commands
{
    public class DeleteGameCommand : IRequest<int>
    {
        public int Id { get; set; }
        public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public DeleteGameCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
            {
                var game = await _context.Games
                    .Where(a => a.Id == request.Id)
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);

                if (game == null) 
                    return default;

                var deletedGameId = game.Id;
                _context.Games.Remove(game);
                await _context.SaveChangesAsync();
                
                return deletedGameId;
            }
        }
    }
}
