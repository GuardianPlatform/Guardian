using Guardian.Domain.Entities;
using Guardian.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Guardian.Domain.Models;


namespace Guardian.Service.Features.CustomerFeatures.Queries
{
    public class GetAllGamesQuery : IRequest<IEnumerable<Game>>
    {
        public PagiantionModel Pagination { get; set; }

        public class GetAllGamesQueryHandler : IRequestHandler<GetAllGamesQuery, IEnumerable<Game>>
        {

            private readonly IApplicationDbContext _context;
            public GetAllGamesQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<Game>> Handle(GetAllGamesQuery request, CancellationToken cancellationToken)
            {

                var gameList = await _context.Games
                    .Skip(request.Pagination.ItemsPerPage * request.Pagination.page)
                    .Take(request.Pagination.ItemsPerPage)
                    .ToListAsync(cancellationToken: cancellationToken);

                return gameList?.AsReadOnly();
            }
        }

    }
}
