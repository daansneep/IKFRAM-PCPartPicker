using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.StorageDevices
{
    public class Details
    {
        public class Query : IRequest<StorageDevice>
        {
            public int Id { get; set; }
        }
        
        public class Handler : IRequestHandler<Query, StorageDevice>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            
            public async Task<StorageDevice> Handle(Query request, CancellationToken cancellationToken)
            {
                
                var storageDevice = await _context.StorageDevices
                    .Include(x => x.Part)
                    .SingleOrDefaultAsync(x => x.Id == request.Id);
                
                if (storageDevice == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { storageDevice = "Not Found"});
                }
                
                return storageDevice;
            }
        }
    }
}