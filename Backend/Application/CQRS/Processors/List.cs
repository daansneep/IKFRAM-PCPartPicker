using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.Processors
{
    public class List
    {
        public class Query : IRequest<List<Processor>> {}

        public class Handler : IRequestHandler<Query, List<Processor>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<Processor>> Handle(Query request, CancellationToken cancellationToken)
            {
                var processors = await _context.Processors
                    .Include(x => x.Part)
                    .Include(x => x.Socket)
                    .ToListAsync();

                return processors;
            }
        }
    }
}