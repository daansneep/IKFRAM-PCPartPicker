using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.CQRS.PowerSupplies
{
    public class Create
    {
        public class Command : IRequest
        {
            public Part Part { get; set; }
            public int Power { get; set; }
            public bool Modular { get; set; }
            public string PowerRating { get; set; }
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
                var powerSupply = new PowerSupply
                {
                    Part = await _context.Parts.FindAsync(request.Part.PartId),
                    Power = request.Power,
                    Modular = request.Modular,
                    PowerRating = request.PowerRating
                };

                await _context.PowerSupplies.AddAsync(powerSupply);
                var success = await _context.SaveChangesAsync() > 0;
                if (success) return Unit.Value;

                throw new Exception("Problem occured");
            }
        }
    }
}