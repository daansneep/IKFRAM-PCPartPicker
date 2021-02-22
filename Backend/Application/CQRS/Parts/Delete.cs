using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using MediatR;
using Persistence;

namespace Application.CQRS.Parts
{
    public class Delete
    {
        public class Command : IRequest
        {
            public int PartId { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var part = await _context.Parts.FindAsync(request.PartId);

                if (part == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { part = "Not Found"});
                }

                _context.Remove(part);
                
                var success = await _context.SaveChangesAsync() > 0;
                
                if (success) return Unit.Value;

                throw new Exception("Problem occured while saving changes");
            }
        }
    }
}