using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.Sockets;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocketsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SocketsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        // GET api/sockets
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<Socket>>> List()
        {
            return await _mediator.Send(new List.Query());
        }
        
        // GET api/sockets/{id}
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Socket>> Details(int id)
        {
            return await _mediator.Send(new Details.Query{SocketId = id});
        }

        // POST api/sockets
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await _mediator.Send(command);
        }

        // PUT api/sockets/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(int id, Edit.Command command)
        {
            command.SocketId = id;
            return await _mediator.Send(command);
        }

        // DELETE api/sockets/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(int id)
        {
            return await _mediator.Send(new Delete.Command {SocketId = id});
        }
    }
}