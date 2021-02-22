using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.FormFactors
{
    public class List
    {
        public class Query : IRequest<List<FormFactor>> {}

        public class Handler : IRequestHandler<Query, List<FormFactor>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<FormFactor>> Handle(Query request, CancellationToken cancellationToken)
            {
                var formFactors = await _context.FormFactors.ToListAsync();

                return formFactors;
            }
        }
    }
}