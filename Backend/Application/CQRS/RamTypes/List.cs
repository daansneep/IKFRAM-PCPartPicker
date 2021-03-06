﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.RamTypes
{
    public class List
    {
        public class Query : IRequest<List<RamType>> {}

        public class Handler : IRequestHandler<Query, List<RamType>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<RamType>> Handle(Query request, CancellationToken cancellationToken)
            {
                var ramTypes = await _context.RamTypes.ToListAsync();

                return ramTypes;
            }
        }
    }
}