using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.Processors
{
    public class Details
    {
        public class Query : IRequest<Processor>
        {
            public int Id { get; set; }
        }
        
        public class Handler : IRequestHandler<Query, Processor>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            
            public async Task<Processor> Handle(Query request, CancellationToken cancellationToken)
            {
                
                var processor = await _context.Processors
                    .Include(x => x.Part)
                    .Include(x => x.Socket)
                    .SingleOrDefaultAsync(x => x.Id == request.Id);
                
                if (processor == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { processor = "Not Found"});
                }
                
                return processor;
            }
        }
    }
}