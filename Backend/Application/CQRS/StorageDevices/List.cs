using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.StorageDevices
{
    public class List
    {
        public class Query : IRequest<List<StorageDevice>> {}

        public class Handler : IRequestHandler<Query, List<StorageDevice>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<StorageDevice>> Handle(Query request, CancellationToken cancellationToken)
            {
                var storageDevices = await _context.StorageDevices
                    .Include(x => x.Part)
                    .ToListAsync();

                return storageDevices;
            }
        }
    }
}