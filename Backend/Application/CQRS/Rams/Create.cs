using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.CQRS.Rams
{
    public class Create
    {
        public class Command : IRequest
        {
            public Part Part { get; set; }
            public RamType RamType { get; set; }
            public int Gb { get; set; }
            public int StickCount { get; set; }
            public int ClockFreq {get; set; }
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
                var ram = new Ram
                {
                    Part = await _context.Parts.FindAsync(request.Part.PartId),
                    RamType = await _context.RamTypes.FindAsync(request.RamType.RamTypeId),
                    Gb = request.Gb,
                    StickCount = request.StickCount,
                    ClockFreq = request.ClockFreq,
                    Rgb = request.Rgb
                };

                await _context.Rams.AddAsync(ram);
                var success = await _context.SaveChangesAsync() > 0;
                if (success) return Unit.Value;

                throw new Exception("Problem occured");
            }
        }
    }
}