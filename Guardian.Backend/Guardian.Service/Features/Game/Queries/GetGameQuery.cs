using System.Threading;
using System.Threading.Tasks;
using Guardian.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Guardian.Service.Features.Game.Queries
{
    public class GetGameQuery : IRequest<Domain.Entities.Game>
    {
        public int Id { get; set; }

        public class GetGameQueryHandler : IRequestHandler<GetGameQuery, Domain.Entities.Game>
        {

            private readonly IApplicationDbContext _context;
            public GetGameQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<Domain.Entities.Game> Handle(GetGameQuery request, CancellationToken cancellationToken)
            {
                var game = await _context.Games
                    .Include(x => x.Categories)
                    .Include(x => x.Ratings)
                    .Include(x => x.Users)
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                return game;
            }
        }

    }
}
