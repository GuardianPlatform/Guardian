﻿using Guardian.Domain.Entities;
using Guardian.Infrastructure.Database;
using Guardian.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Guardian.Infrastructure.Database;


namespace Guardian.Service.Features.CustomerFeatures.Queries
{
    public class GetAllGamesQuery : IRequest<IEnumerable<Game>>
    {
        public PagiantionModel Pagiantion { get; set; }

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
                    .Skip(request.Pagiantion.ItemsPerPage * request.Pagiantion.page)
                    .Take(request.Pagiantion.ItemsPerPage)
                    .ToListAsync(cancellationToken: cancellationToken);

                return gameList?.AsReadOnly();
            }
        }

    }
}
