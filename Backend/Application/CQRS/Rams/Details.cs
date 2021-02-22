using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.Rams
{
    public class Details
    {
        public class Query : IRequest<Ram>
        {
            public int Id { get; set; }
        }
        
        public class Handler : IRequestHandler<Query, Ram>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            
            public async Task<Ram> Handle(Query request, CancellationToken cancellationToken)
            {
                
                var ram = await _context.Rams
                    .Include(x => x.Part)
                    .Include(x => x.RamType)
                    .SingleOrDefaultAsync(x => x.Id == request.Id);
                
                if (ram == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { ram = "Not Found"});
                }
                
                return ram;
            }
        }
    }
}