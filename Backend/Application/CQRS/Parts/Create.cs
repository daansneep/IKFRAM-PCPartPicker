using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.CodeAnalysis;
using Persistence;

namespace Application.CQRS.Parts
{
    public class Create
    {
        public class Command : IRequest
        {
            public string Name { get; set; }
            public string Brand { get; set; }
            public string Image { get; set; }
            public double PurchasePrice { get; set; }
            public double RetailPrice { get; set; }
            public double Margin { get; set; }
            public DateTime  CreationDate { get; set; }
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
                var part = new Part
                {
                    Name = request.Name,
                    Brand = request.Brand,
                    Image = request.Image,
                    PurchasePrice = request.PurchasePrice,
                    RetailPrice = request.RetailPrice,
                    Margin = request.Margin,
                    CreationDate = request.CreationDate
                };

                await _context.Parts.AddAsync(part);
                var success = await _context.SaveChangesAsync() > 0;
                if (success) return Unit.Value;

                throw new Exception("Problem occured");
            }
        }
        
    }
}