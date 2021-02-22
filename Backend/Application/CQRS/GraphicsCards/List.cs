using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.GraphicsCards
{
    public class List
    {
        public class Query : IRequest<List<GraphicsCard>> {}

        public class Handler : IRequestHandler<Query, List<GraphicsCard>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<GraphicsCard>> Handle(Query request, CancellationToken cancellationToken)
            {
                var graphicsCard = await _context.GraphicsCards
                    .Include(x => x.Part)
                    .ToListAsync();

                return graphicsCard;
            }
        }
    }
}