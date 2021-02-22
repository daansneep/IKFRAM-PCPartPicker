using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.CQRS.Cases
{
    public class Create
    {
        public class Command : IRequest
        {
            public Part Part { get; set; }
            public FormFactor FormFactor { get; set; }
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
                var @case = new Case
                {
                    Part = await _context.Parts.FindAsync(request.Part.PartId),
                    FormFactor = await _context.FormFactors.FindAsync(request.FormFactor.FormFactorId)
                };

                await _context.Cases.AddAsync(@case);
                var success = await _context.SaveChangesAsync() > 0;
                if (success) return Unit.Value;

                throw new Exception("Problem occured");
            }
        }
    }
}