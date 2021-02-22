using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.Motherboards
{
    public class Details
    {
        public class Query : IRequest<Motherboard>
        {
            public int Id { get; set; }
        }
        
        public class Handler : IRequestHandler<Query, Motherboard>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            
            public async Task<Motherboard> Handle(Query request, CancellationToken cancellationToken)
            {
                
                var motherboard = await _context.Motherboards
                    .Include(x => x.Part)
                    .Include(x => x.Socket)
                    .Include(x => x.FormFactor)
                    .Include(x => x.RamType)
                    .SingleOrDefaultAsync(x => x.Id == request.Id);

                if (motherboard == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { motherboard = "Not Found"});
                }
                
                return motherboard;
            }
        }
    }
}