using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.Sockets
{
    public class List
    {
        public class Query : IRequest<List<Socket>> {}

        public class Handler : IRequestHandler<Query, List<Socket>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<Socket>> Handle(Query request, CancellationToken cancellationToken)
            {
                var sockets = await _context.Sockets.ToListAsync();

                return sockets;
            }
        }
    }
}