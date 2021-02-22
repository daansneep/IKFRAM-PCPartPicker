using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.Parts
{
    public class List
    {
        public class Query : IRequest<List<Part>> {}

        public class Handler : IRequestHandler<Query, List<Part>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<Part>> Handle(Query request, CancellationToken cancellationToken)
            {
                var parts = await _context.Parts.ToListAsync();

                return parts;
            }
        }
    }
}