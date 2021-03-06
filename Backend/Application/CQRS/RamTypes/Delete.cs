﻿using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using MediatR;
using Persistence;

namespace Application.CQRS.RamTypes
{
    public class Delete
    {
        public class Command : IRequest
        {
            public int RamTypeId { get; set; }
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
                var ramType = await _context.RamTypes.FindAsync(request.RamTypeId);

                if (ramType == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { ramType = "Not Found"});
                }

                _context.Remove(ramType);
                
                var success = await _context.SaveChangesAsync() > 0;
                
                if (success) return Unit.Value;

                throw new Exception("Problem occured while saving changes");
            }
        }
    }
}