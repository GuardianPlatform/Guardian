using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Guardian.Domain.Models;
using Guardian.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Guardian.Service.Features.Game.Queries
{
    public class GetAllGamesQuery : IRequest<IEnumerable<Domain.Entities.Game>>
    {
        public PagiantionModel Pagination { get; set; }

        public class GetAllGamesQueryHandler : IRequestHandler<GetAllGamesQuery, IEnumerable<Domain.Entities.Game>>
        {

            private readonly IApplicationDbContext _context;
            public GetAllGamesQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<Domain.Entities.Game>> Handle(GetAllGamesQuery request, CancellationToken cancellationToken)
            {
                var gameList = await _context.Games
                    .Include(x => x.Categories)
                    .Skip(request.Pagination.ItemsPerPage * request.Pagination.page)
                    .Take(request.Pagination.ItemsPerPage)
                    .ToListAsync(cancellationToken: cancellationToken);

                return gameList?.AsReadOnly();
            }
        }

    }
}
