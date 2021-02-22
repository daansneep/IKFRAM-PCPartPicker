using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.CQRS.GraphicsCards
{
    public class Create
    {
        public class Command : IRequest
        {
            public Part Part { get; set; }
            public int ClockFreq { get; set; }
            public int Gb { get; set; }
            public string RamType { get; set; }
            public bool CrossSli { get; set; }
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
                var graphicsCard = new GraphicsCard
                {
                    Part = await _context.Parts.FindAsync(request.Part.PartId),
                    ClockFreq = request.ClockFreq,
                    Gb = request.Gb,
                    RamType = request.RamType,
                    CrossSli = request.CrossSli,
                    Rgb = request.Rgb
                };

                await _context.GraphicsCards.AddAsync(graphicsCard);
                var success = await _context.SaveChangesAsync() > 0;
                if (success) return Unit.Value;

                throw new Exception("Problem occured");
            }
        }
    }
}