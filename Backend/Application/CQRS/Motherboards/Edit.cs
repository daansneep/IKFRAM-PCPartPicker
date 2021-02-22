using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Persistence;

namespace Application.CQRS.Motherboards
{
    public class Edit
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
            public Part Part { get; set; }
            public Socket Socket { get; set; }
            public FormFactor FormFactor { get; set; }
            public RamType RamType { get; set; }
            public string Chipset { get; set; }
            public bool? Oc { get; set; }
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
                var motherboard = await _context.Motherboards.FindAsync(request.Id);

                if (motherboard == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { motherboard = "Not Found"});
                }

                motherboard.Part = await _context.Parts.FindAsync(request.Part.PartId) ?? motherboard.Part;
                motherboard.Socket = await _context.Sockets.FindAsync(request.Socket.SocketId) ?? motherboard.Socket;
                motherboard.FormFactor = await _context.FormFactors.FindAsync(request.FormFactor.FormFactorId) ?? motherboard.FormFactor;
                motherboard.RamType = await _context.RamTypes.FindAsync(request.RamType.RamTypeId) ?? motherboard.RamType;
                motherboard.Chipset = request.Chipset ?? motherboard.Chipset;
                motherboard.Oc = request.Oc ?? motherboard.Oc;
                motherboard.Rgb = request.Rgb ?? motherboard.Rgb;
                
                var success = await _context.SaveChangesAsync() > 0;
                
                if (success) return Unit.Value;

                throw new Exception("Problem occured while saving changes");
            }
        }
    }
}