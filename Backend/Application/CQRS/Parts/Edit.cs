using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using MediatR;
using Persistence;

namespace Application.CQRS.Parts
{
    public class Edit
    {
        public class Command : IRequest
        {
            public int PartId { get; set; }
            public string Name { get; set; }
            public string Brand { get; set; }
            public string Image { get; set; }
            public double? PurchasePrice { get; set; }
            public double? RetailPrice { get; set; }
            public double? Margin { get; set; }
            public DateTime?  CreationDate { get; set; }
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
                var part = await _context.Parts.FindAsync(request.PartId);

                if (part == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { part = "Not Found"});
                }

                part.Name = request.Name ?? part.Name;
                part.Brand = request.Brand ?? part.Brand;
                part.Image = request.Image ?? part.Image;
                part.PurchasePrice = request.PurchasePrice ?? part.PurchasePrice;
                part.RetailPrice = request.RetailPrice ?? part.RetailPrice;
                part.Margin = request.Margin ?? part.Margin;
                part.CreationDate = request.CreationDate ?? part.CreationDate;

                var success = await _context.SaveChangesAsync() > 0;
                
                if (success) return Unit.Value;

                throw new Exception("Problem occured while saving changes");
            }
        }
    }
}