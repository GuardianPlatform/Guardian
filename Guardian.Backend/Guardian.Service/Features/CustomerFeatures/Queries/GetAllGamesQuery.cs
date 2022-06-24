using Guardian.Domain.Entities;
using Guardian.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Guardian.Domain.Entities;

namespace Guardian.Service.Features.CustomerFeatures.Queries
{
    public class GetAllGamesQuery : IRequest<IEnumerable<Game>>
    {       
        PagiantionParams pagiantionParams = new PagiantionParams();

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
                        .Skip(request.pagiantionParams.ItemsPerPage * request.pagiantionParams.page)
                        .Take(request.pagiantionParams.ItemsPerPage)
                        .ToListAsync();
                    if (gameList == null)
                    {
                        return null;
                    }
                    return gameList.AsReadOnly();
                }
            }
        
    }
}
