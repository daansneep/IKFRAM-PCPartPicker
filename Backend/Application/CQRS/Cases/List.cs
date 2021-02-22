using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.Cases
{
    public class List
    {
        public class Query : IRequest<List<Case>> {}

        public class Handler : IRequestHandler<Query, List<Case>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<Case>> Handle(Query request, CancellationToken cancellationToken)
            {
                var cases = await _context.Cases
                    .Include(x => x.Part)
                    .Include(x => x.FormFactor)
                    .ToListAsync();

                return cases;
            }
        }
    }
}