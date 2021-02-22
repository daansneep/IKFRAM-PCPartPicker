using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using MediatR;
using Persistence;

namespace Application.CQRS.Sockets
{
    public class Delete
    {
        public class Command : IRequest
        {
            public int SocketId { get; set; }
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
                var socket = await _context.Sockets.FindAsync(request.SocketId);

                if (socket == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { socket = "Not Found"});
                }

                _context.Remove(socket);
                
                var success = await _context.SaveChangesAsync() > 0;
                
                if (success) return Unit.Value;

                throw new Exception("Problem occured while saving changes");
            }
        }
    }
}