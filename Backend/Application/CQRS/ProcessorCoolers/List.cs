using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.ProcessorCoolers
{
    public class List
    {
        public class Query : IRequest<List<ProcessorCooler>> {}

        public class Handler : IRequestHandler<Query, List<ProcessorCooler>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<ProcessorCooler>> Handle(Query request, CancellationToken cancellationToken)
            {
                var processorCoolers = await _context.ProcessorCoolers
                    .Include(x => x.Part)
                    .Include(x => x.Socket)
                    .ToListAsync();

                return processorCoolers;
            }
        }
    }
}