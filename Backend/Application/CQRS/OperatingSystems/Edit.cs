using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Persistence;

namespace Application.CQRS.OperatingSystems
{
    public class Edit
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
            public Part Part { get; set; }
            public double? Size { get; set; }
            public bool? OpenSource { get; set; }
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
                var operatingSystem = await _context.OperatingSystems.FindAsync(request.Id);

                if (operatingSystem == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { operatingSystem = "Not Found"});
                }

                operatingSystem.Part = await _context.Parts.FindAsync(request.Part.PartId) ?? operatingSystem.Part;
                operatingSystem.Size = request.Size ?? operatingSystem.Size;
                operatingSystem.OpenSource = request.OpenSource ?? operatingSystem.OpenSource;

                var success = await _context.SaveChangesAsync() > 0;
                
                if (success) return Unit.Value;

                throw new Exception("Problem occured while saving changes");
            }
        }
    }
}