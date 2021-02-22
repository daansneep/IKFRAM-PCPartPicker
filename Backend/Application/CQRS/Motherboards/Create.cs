using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.CQRS.Motherboards
{
    public class Create
    {
        public class Command : IRequest
        {
            public Part Part { get; set; }
            public Socket Socket { get; set; }
            public FormFactor FormFactor { get; set; }
            public RamType RamType { get; set; }
            public string Chipset { get; set; }
            public bool Oc { get; set; }
            public bool Rgb { get; set; }
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
                var motherboard = new Motherboard
                {
                    Part = await _context.Parts.FindAsync(request.Part.PartId),
                    Socket = await _context.Sockets.FindAsync(request.Socket.SocketId),
                    FormFactor = await _context.FormFactors.FindAsync(request.FormFactor.FormFactorId),
                    RamType = await _context.RamTypes.FindAsync(request.RamType.RamTypeId),
                    Chipset = request.Chipset,
                    Oc = request.Oc,
                    Rgb = request.Rgb
                };
                
                await _context.Motherboards.AddAsync(motherboard);
                var success = await _context.SaveChangesAsync() > 0;
                if (success) return Unit.Value;

                throw new Exception("Problem occured");
            }
        }
    }
}