using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using MediatR;
using Persistence;

namespace Application.CQRS.FormFactors
{
    public class Edit
    {
        public class Command : IRequest
        {
            public int FormFactorId { get; set; }
            public string FormFactorName { get; set; }
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
                var formFactor = await _context.FormFactors.FindAsync(request.FormFactorId);

                if (formFactor == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { formFactor = "Not Found"});
                }

                formFactor.FormFactorName = request.FormFactorName ?? formFactor.FormFactorName;

                var success = await _context.SaveChangesAsync() > 0;
                
                if (success) return Unit.Value;

                throw new Exception("Problem occured while saving changes");
            }
        }
    }
}