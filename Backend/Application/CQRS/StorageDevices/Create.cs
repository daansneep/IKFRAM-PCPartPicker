using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.CQRS.StorageDevices
{
    public class Create
    {
        public class Command : IRequest
        {
            public Part Part { get; set; }
            public int Gb { get; set; }
            public int Tb { get; set; }
            public bool Ssd { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                // TODO VALIDATION
            }
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
                var storageDevice = new StorageDevice
                {
                    Part = await _context.Parts.FindAsync(request.Part.PartId),
                    Gb = request.Gb,
                    Tb = request.Tb,
                    Ssd = request.Ssd
                };

                await _context.StorageDevices.AddAsync(storageDevice);
                var success = await _context.SaveChangesAsync() > 0;
                if (success) return Unit.Value;

                throw new Exception("Problem occured");
            }
        }
    }
}