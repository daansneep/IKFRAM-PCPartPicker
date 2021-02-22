using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.Rams;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RamsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RamsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        // GET api/rams
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<Ram>>> List()
        {
            return await _mediator.Send(new List.Query());
        }
        
        // GET api/rams/{id}
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Ram>> Details(int id)
        {
            return await _mediator.Send(new Details.Query{Id = id});
        }

        // POST api/rams
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await _mediator.Send(command);
        }

        // PUT api/rams/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(int id, Edit.Command command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }

        // DELETE api/rams/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(int id)
        {
            return await _mediator.Send(new Delete.Command {Id = id});
        }
    }
}