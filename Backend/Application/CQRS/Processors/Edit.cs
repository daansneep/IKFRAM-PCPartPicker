using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Persistence;

namespace Application.CQRS.Processors
{
    public class Edit
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
            public Part Part { get; set; }
            public Socket Socket { get; set; }
            public int? Cores { get; set; }
            public int? Threads { get; set; }
            public int? ClockFreq { get; set; }
            public int? TurboFreq { get; set; }
            public bool? Oc { get; set; }
            public bool? Graph { get; set; }
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
                var processor = await _context.Processors.FindAsync(request.Id);

                if (processor == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { processor = "Not Found"});
                }

                processor.Part = await _context.Parts.FindAsync(request.Part.PartId) ?? processor.Part;
                processor.Socket = await _context.Sockets.FindAsync(request.Socket.SocketId) ?? processor.Socket;
                processor.Cores = request.Cores ?? processor.Cores;
                processor.Threads = request.Threads ?? processor.Threads;
                processor.ClockFreq = request.ClockFreq ?? processor.ClockFreq;
                processor.TurboFreq = request.TurboFreq ?? processor.TurboFreq;
                processor.Oc = request.Oc ?? processor.Oc;
                processor.Graph = request.Graph ?? processor.Graph;
                
                var success = await _context.SaveChangesAsync() > 0;
                
                if (success) return Unit.Value;

                throw new Exception("Problem occured while saving changes");
            }
        }
    }
}