using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.CQRS.FormFactors
{
    public class Create
    {
        public class Command : IRequest
        {
            public string FormFactorName { get; set; }
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
                var formFactor = new FormFactor
                {
                    FormFactorName = request.FormFactorName
                };

                await _context.FormFactors.AddAsync(formFactor);
                var success = await _context.SaveChangesAsync() > 0;
                if (success) return Unit.Value;

                throw new Exception("Problem occured");
            }
        }
        
    }
}