using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.CQRS.ProcessorCoolers
{
    public class Create
    {
        public class Command : IRequest
        {
            public Part Part { get; set; }
            public Socket Socket { get; set; }
            public bool Rgb { get; set; }
            public bool Water { get; set; }
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
                var processorCooler = new ProcessorCooler
                {
                    Part = await _context.Parts.FindAsync(request.Part.PartId),
                    Socket = await _context.Sockets.FindAsync(request.Socket.SocketId),
                    Rgb = request.Rgb,
                    Water = request.Water
                };

                await _context.ProcessorCoolers.AddAsync(processorCooler);
                var success = await _context.SaveChangesAsync() > 0;
                if (success) return Unit.Value;

                throw new Exception("Problem occured");
            }
        }
    }
}