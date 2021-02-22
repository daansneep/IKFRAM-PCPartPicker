using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.PowerSupplies
{
    public class List
    {
        public class Query : IRequest<List<PowerSupply>> {}

        public class Handler : IRequestHandler<Query, List<PowerSupply>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<PowerSupply>> Handle(Query request, CancellationToken cancellationToken)
            {
                var powerSupplies = await _context.PowerSupplies
                    .Include(x => x.Part)
                    .ToListAsync();

                return powerSupplies;
            }
        }
    }
}