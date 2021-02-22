using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.ProcessorCoolers;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessorCoolersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProcessorCoolersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        // GET api/processorcoolers
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<ProcessorCooler>>> List()
        {
            return await _mediator.Send(new List.Query());
        }
        
        // GET api/processorcoolers/{id}
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ProcessorCooler>> Details(int id)
        {
            return await _mediator.Send(new Details.Query{Id = id});
        }

        // POST api/processorcoolers
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await _mediator.Send(command);
        }

        // PUT api/processorcoolers/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(int id, Edit.Command command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }

        // DELETE api/processorcoolers/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(int id)
        {
            return await _mediator.Send(new Delete.Command {Id = id});
        }
    }
}