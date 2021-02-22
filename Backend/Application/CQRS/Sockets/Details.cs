using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Persistence;

namespace Application.CQRS.Sockets
{
    public class Details
    {
        public class Query : IRequest<Socket>
        {
            public int SocketId { get; set; }
        }
        
        public class Handler : IRequestHandler<Query, Socket>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            
            public async Task<Socket> Handle(Query request, CancellationToken cancellationToken)
            {
                
                var socket = await _context.Sockets.FindAsync(request.SocketId);
                
                if (socket == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { socket = "Not Found"});
                }
                
                return socket;
            }
        }
    }
}