using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.GraphicsCards
{
    public class Details
    {
        public class Query : IRequest<GraphicsCard>
        {
            public int Id { get; set; }
        }
        
        public class Handler : IRequestHandler<Query, GraphicsCard>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            
            public async Task<GraphicsCard> Handle(Query request, CancellationToken cancellationToken)
            {
                
                var graphicsCard = await _context.GraphicsCards
                    .Include(x => x.Part)
                    .SingleOrDefaultAsync(x => x.Id == request.Id);
                
                if (graphicsCard == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { graphicsCard = "Not Found"});
                }
                
                return graphicsCard;
            }
        }
    }
}