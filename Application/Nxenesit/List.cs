using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Nxenesit
{
    public class List
    {
        public class Query : IRequest<Result<List<Nxenesi>>> { }

        public class Handler : IRequestHandler<Query, Result<List<Nxenesi>>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<List<Nxenesi>>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Result<List<Nxenesi>>.Success(await _context.Nxenesi.ToListAsync(cancellationToken));
            }
        }
    }
}