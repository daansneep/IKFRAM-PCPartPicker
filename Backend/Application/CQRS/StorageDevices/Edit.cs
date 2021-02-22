using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Persistence;

namespace Application.CQRS.StorageDevices
{
    public class Edit
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
            public Part Part { get; set; }
            public int? Gb { get; set; }
            public int? Tb { get; set; }
            public bool? Ssd { get; set; }
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
                var storageDevice = await _context.StorageDevices.FindAsync(request.Id);

                if (storageDevice == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { storageDevice = "Not Found"});
                }

                storageDevice.Part = await _context.Parts.FindAsync(request.Part.PartId) ?? storageDevice.Part;
                storageDevice.Gb = request.Gb ?? storageDevice.Gb;
                storageDevice.Tb = request.Tb ?? storageDevice.Tb;
                storageDevice.Ssd = request.Ssd ?? storageDevice.Ssd;

                var success = await _context.SaveChangesAsync() > 0;
                
                if (success) return Unit.Value;

                throw new Exception("Problem occured while saving changes");
            }
        }
    }
}