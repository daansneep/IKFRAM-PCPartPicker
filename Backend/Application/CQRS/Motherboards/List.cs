using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.Motherboards
{
    public class List
    {
        public class Query : IRequest<List<Motherboard>> {}

        public class Handler : IRequestHandler<Query, List<Motherboard>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<Motherboard>> Handle(Query request, CancellationToken cancellationToken)
            {
                var motherboards = await _context.Motherboards
                    .Include(x => x.Part)
                    .Include(x => x.Socket)
                    .Include(x => x.FormFactor)
                    .Include(x => x.RamType)
                    .ToListAsync();

                return motherboards;
            }
        }
    }
}