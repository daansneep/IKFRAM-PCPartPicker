using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.Cases
{
    public class Details
    {
        public class Query : IRequest<Case>
        {
            public int Id { get; set; }
        }
        
        public class Handler : IRequestHandler<Query, Case>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            
            public async Task<Case> Handle(Query request, CancellationToken cancellationToken)
            {

                var @case = await _context.Cases
                    .Include(x => x.Part)
                    .Include(x => x.FormFactor)
                    .SingleOrDefaultAsync(x => x.Id == request.Id);

                if (@case == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { @case = "Not Found"});
                }
                
                return @case;
            }
        }
    }
}