using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Persistence;

namespace Application.CQRS.ProcessorCoolers
{
    public class Edit
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
            public Part Part { get; set; }
            public Socket Socket { get; set; }
            public bool? Rgb { get; set; }
            public bool? Water { get; set; }
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
                var processorCooler = await _context.ProcessorCoolers.FindAsync(request.Id);

                if (processorCooler == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { processorCooler = "Not Found"});
                }

                processorCooler.Part = await _context.Parts.FindAsync(request.Part.PartId) ?? processorCooler.Part;
                processorCooler.Socket = await _context.Sockets.FindAsync(request.Socket.SocketId) ?? processorCooler.Socket;
                processorCooler.Rgb = request.Rgb ?? processorCooler.Rgb;
                processorCooler.Water = request.Water ?? processorCooler.Water;
                
                var success = await _context.SaveChangesAsync() > 0;
                
                if (success) return Unit.Value;

                throw new Exception("Problem occured while saving changes");
            }
        }
    }
}