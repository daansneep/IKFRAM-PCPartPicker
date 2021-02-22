using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Persistence;

namespace Application.CQRS.RamTypes
{
    public class Details
    {
        public class Query : IRequest<RamType>
        {
            public int RamTypeId { get; set; }
        }
        
        public class Handler : IRequestHandler<Query, RamType>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            
            public async Task<RamType> Handle(Query request, CancellationToken cancellationToken)
            {
                
                var ramType = await _context.RamTypes.FindAsync(request.RamTypeId);
                
                if (ramType == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { ramType = "Not Found"});
                }
                
                return ramType;
            }
        }
    }
}