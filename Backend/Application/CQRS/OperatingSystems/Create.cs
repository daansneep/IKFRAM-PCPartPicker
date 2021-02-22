using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;
using OperatingSystem = Domain.OperatingSystem;

namespace Application.CQRS.OperatingSystems
{
    public class Create
    {
        public class Command : IRequest
        {
            public Part Part { get; set; }
            public double Size { get; set; }
            public bool OpenSource { get; set; }
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
                var operatingSystem = new OperatingSystem
                {
                    Part = await _context.Parts.FindAsync(request.Part.PartId),
                    Size = request.Size,
                    OpenSource = request.OpenSource,
                };

                await _context.OperatingSystems.AddAsync(operatingSystem);
                var success = await _context.SaveChangesAsync() > 0;
                if (success) return Unit.Value;

                throw new Exception("Problem occured");
            }
        }
    }
}