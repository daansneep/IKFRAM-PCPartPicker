using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.Rams
{
    public class List
    {
        public class Query : IRequest<List<Ram>> {}

        public class Handler : IRequestHandler<Query, List<Ram>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<Ram>> Handle(Query request, CancellationToken cancellationToken)
            {
                var rams = await _context.Rams
                    .Include(x => x.Part)
                    .Include(x => x.RamType)
                    .ToListAsync();

                return rams;
            }
        }
    }
}