using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.CQRS.Processors
{
    public class Create
    {
        public class Command : IRequest
        {
            public Part Part { get; set; }
            public Socket Socket { get; set; }
            public int Cores { get; set; }
            public int Threads { get; set; }
            public int ClockFreq { get; set; }
            public int TurboFreq { get; set; }
            public bool Oc { get; set; }
            public bool Graph { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                // TODO VALIDATION
            }
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
                var processor = new Processor
                {
                    Part = await _context.Parts.FindAsync(request.Part.PartId),
                    Socket = await _context.Sockets.FindAsync(request.Socket.SocketId),
                    Cores = request.Cores,
                    Threads = request.Threads,
                    ClockFreq = request.ClockFreq,
                    TurboFreq = request.TurboFreq,
                    Oc = request.Oc,
                    Graph = request.Graph
                };

                await _context.Processors.AddAsync(processor);
                var success = await _context.SaveChangesAsync() > 0;
                if (success) return Unit.Value;

                throw new Exception("Problem occured");
            }
        }
    }
}