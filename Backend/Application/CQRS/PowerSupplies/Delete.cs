using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using MediatR;
using Persistence;

namespace Application.CQRS.PowerSupplies
{
    public class Delete
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
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
                var powerSupply = await _context.PowerSupplies.FindAsync(request.Id);

                if (powerSupply == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { powerSupply = "Not Found"});
                }

                _context.Remove(powerSupply);
                
                var success = await _context.SaveChangesAsync() > 0;
                
                if (success) return Unit.Value;

                throw new Exception("Problem occured while saving changes");
            }
        }
    }
}