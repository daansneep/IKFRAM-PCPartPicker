using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Persistence;

namespace Application.CQRS.Cases
{
    public class Edit
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
            public Part Part { get; set; }
            public FormFactor FormFactor { get; set; }
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
                var @case = await _context.Cases.FindAsync(request.Id);

                if (@case == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { @case = "Not Found"});
                }

                @case.Part = await _context.Parts.FindAsync(request.Part.PartId) ?? @case.Part;
                @case.FormFactor = await _context.FormFactors.FindAsync(request.FormFactor.FormFactorId) ?? @case.FormFactor;

                var success = await _context.SaveChangesAsync() > 0;
                
                if (success) return Unit.Value;

                throw new Exception("Problem occured while saving changes");
            }
        }
    }
}