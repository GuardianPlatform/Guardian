using System;
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
    public class GetAllGamesForCategoryQuery : IRequest<IEnumerable<Domain.Entities.Game>>
    {
        public string Category { get; set; }
        public PagiantionModel Pagination { get; set; }

        public GetAllGamesForCategoryQuery(string category, PagiantionModel pagination = null)
        {
            Category = category;
            Pagination = pagination ?? new PagiantionModel();
        }

        public class GetAllGamesForCategoryQueryHandler : IRequestHandler<GetAllGamesForCategoryQuery, IEnumerable<Domain.Entities.Game>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllGamesForCategoryQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Domain.Entities.Game>> Handle(GetAllGamesForCategoryQuery request,
                CancellationToken cancellationToken)
            {
                return _context.Games?
                    .Where(x => x.Categories.Any(y => y.CategoryName.ToLower() == request.Category.ToLower()))
                    .Skip(request.Pagination.ItemsPerPage * request.Pagination.page)
                    .Take(request.Pagination.ItemsPerPage)
                    .ToList().AsReadOnly();
            }
        }
    }
}