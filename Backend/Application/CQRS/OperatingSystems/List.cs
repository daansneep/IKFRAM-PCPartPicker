using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.OperatingSystems
{
    public class List
    {
        public class Query : IRequest<List<OperatingSystem>> {}

        public class Handler : IRequestHandler<Query, List<OperatingSystem>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<OperatingSystem>> Handle(Query request, CancellationToken cancellationToken)
            {
                var operatingSystems = await _context.OperatingSystems
                    .Include(x => x.Part)
                    .ToListAsync();

                return operatingSystems;
            }
        }
    }
}