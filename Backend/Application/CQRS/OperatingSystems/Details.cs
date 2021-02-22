using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.OperatingSystems
{
    public class Details
    {
        public class Query : IRequest<OperatingSystem>
        {
            public int Id { get; set; }
        }
        
        public class Handler : IRequestHandler<Query, OperatingSystem>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            
            public async Task<OperatingSystem> Handle(Query request, CancellationToken cancellationToken)
            {
                
                var operatingSystem = await _context.OperatingSystems
                    .Include(x => x.Part)
                    .SingleOrDefaultAsync(x => x.Id == request.Id);
                
                if (operatingSystem == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { operatingSystem = "Not Found"});
                }
                
                return operatingSystem;
            }
        }
    }
}