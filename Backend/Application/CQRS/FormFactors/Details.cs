using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Persistence;

namespace Application.CQRS.FormFactors
{
    public class Details
    {
        public class Query : IRequest<FormFactor>
        {
            public int FormFactorId { get; set; }
        }
        
        public class Handler : IRequestHandler<Query, FormFactor>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            
            public async Task<FormFactor> Handle(Query request, CancellationToken cancellationToken)
            {
                
                var formFactor = await _context.FormFactors.FindAsync(request.FormFactorId);
                
                if (formFactor == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { formFactor = "Not Found"});
                }
                
                return formFactor;
            }
        }
    }
}