using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Persistence;

namespace Application.CQRS.Parts
{
    public class Details
    {
        public class Query : IRequest<Part>
        {
            public int PartId { get; set; }
        }
        
        public class Handler : IRequestHandler<Query, Part>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            
            public async Task<Part> Handle(Query request, CancellationToken cancellationToken)
            {
                
                var part = await _context.Parts.FindAsync(request.PartId);
                
                if (part == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { part = "Not Found"});
                }
                
                return part;
            }
        }
    }
}