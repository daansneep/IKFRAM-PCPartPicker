using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Persistence;

namespace Application.CQRS.PowerSupplies
{
    public class Edit
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
            public Part Part { get; set; }
            public int? Power { get; set; }
            public bool? Modular { get; set; }
            public string PowerRating { get; set; }
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

                powerSupply.Part = await _context.Parts.FindAsync(request.Part.PartId) ?? powerSupply.Part;
                powerSupply.Power = request.Power ?? powerSupply.Power;
                powerSupply.Modular = request.Modular ?? powerSupply.Modular;
                powerSupply.PowerRating = request.PowerRating ?? powerSupply.PowerRating;

                var success = await _context.SaveChangesAsync() > 0;
                
                if (success) return Unit.Value;

                throw new Exception("Problem occured while saving changes");
            }
        }
    }
}