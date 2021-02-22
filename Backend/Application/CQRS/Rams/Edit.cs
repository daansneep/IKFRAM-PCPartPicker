using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Persistence;

namespace Application.CQRS.Rams
{
    public class Edit
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
            public Part Part { get; set; }
            public RamType RamType { get; set; }
            public int? Gb { get; set; }
            public int? StickCount { get; set; }
            public int? ClockFreq {get; set; }
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
                var ram = await _context.Rams.FindAsync(request.Id);

                if (ram == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { ram = "Not Found"});
                }

                ram.Part = await _context.Parts.FindAsync(request.Part.PartId) ?? ram.Part;
                ram.RamType = await _context.RamTypes.FindAsync(request.RamType.RamTypeId) ?? ram.RamType;
                ram.Gb = request.Gb ?? ram.Gb;
                ram.StickCount = request.StickCount ?? ram.StickCount;
                ram.ClockFreq = request.ClockFreq ?? ram.ClockFreq;
                ram.Rgb = request.Rgb ?? ram.Rgb;
                
                var success = await _context.SaveChangesAsync() > 0;
                
                if (success) return Unit.Value;

                throw new Exception("Problem occured while saving changes");
            }
        }
    }
}