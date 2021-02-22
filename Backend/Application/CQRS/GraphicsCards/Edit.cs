using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Persistence;

namespace Application.CQRS.GraphicsCards
{
    public class Edit
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
            public Part Part { get; set; }
            public int? ClockFreq { get; set; }
            public int? Gb { get; set; }
            public string RamType { get; set; }
            public bool? CrossSli { get; set; }
            public bool? Rgb { get; set; }
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
                var graphicsCard = await _context.GraphicsCards.FindAsync(request.Id);

                if (graphicsCard == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { graphicsCard = "Not Found"});
                }

                graphicsCard.Part = await _context.Parts.FindAsync(request.Part.PartId) ?? graphicsCard.Part;
                graphicsCard.ClockFreq = request.ClockFreq ?? graphicsCard.ClockFreq;
                graphicsCard.Gb = request.Gb ?? graphicsCard.Gb;
                graphicsCard.RamType = request.RamType ?? graphicsCard.RamType;
                graphicsCard.CrossSli = request.CrossSli ?? graphicsCard.CrossSli;
                graphicsCard.Rgb = request.Rgb ?? graphicsCard.Rgb;
                
                var success = await _context.SaveChangesAsync() > 0;
                
                if (success) return Unit.Value;

                throw new Exception("Problem occured while saving changes");
            }
        }
    }
}