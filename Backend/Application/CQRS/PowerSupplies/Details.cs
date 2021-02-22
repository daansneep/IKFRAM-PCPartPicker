using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.PowerSupplies
{
    public class Details
    {
        public class Query : IRequest<PowerSupply>
        {
            public int Id { get; set; }
        }
        
        public class Handler : IRequestHandler<Query, PowerSupply>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            
            public async Task<PowerSupply> Handle(Query request, CancellationToken cancellationToken)
            {
                
                var powerSupply = await _context.PowerSupplies
                    .Include(x => x.Part)
                    .SingleOrDefaultAsync(x => x.Id == request.Id);
                
                if (powerSupply == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { powerSupply = "Not Found"});
                }
                
                return powerSupply;
            }
        }
    }
}