using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.ProcessorCoolers
{
    public class Details
    {
        public class Query : IRequest<ProcessorCooler>
        {
            public int Id { get; set; }
        }
        
        public class Handler : IRequestHandler<Query, ProcessorCooler>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            
            public async Task<ProcessorCooler> Handle(Query request, CancellationToken cancellationToken)
            {
                
                var processorCooler = await _context.ProcessorCoolers
                    .Include(x => x.Part)
                    .Include(x => x.Socket)
                    .SingleOrDefaultAsync(x => x.Id == request.Id);
                
                if (processorCooler == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { processorCooler = "Not Found"});
                }
                
                return processorCooler;
            }
        }
    }
}