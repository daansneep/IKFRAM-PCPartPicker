using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.StorageDevices;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageDevicesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StorageDevicesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        // GET api/storagedevices
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<StorageDevice>>> List()
        {
            return await _mediator.Send(new List.Query());
        }
        
        // GET api/storagedevices/{id}
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<StorageDevice>> Details(int id)
        {
            return await _mediator.Send(new Details.Query{Id = id});
        }

        // POST api/storagedevices
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await _mediator.Send(command);
        }

        // PUT api/storagedevices/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(int id, Edit.Command command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }

        // DELETE api/storagedevices/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(int id)
        {
            return await _mediator.Send(new Delete.Command {Id = id});
        }
    }
}