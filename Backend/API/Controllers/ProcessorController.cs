using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.Processors;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProcessorsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        // GET api/processors
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<Processor>>> List()
        {
            return await _mediator.Send(new List.Query());
        }
        
        // GET api/processors/{id}
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Processor>> Details(int id)
        {
            return await _mediator.Send(new Details.Query{Id = id});
        }

        // POST api/processors
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await _mediator.Send(command);
        }

        // PUT api/processors/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(int id, Edit.Command command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }

        // DELETE api/processors/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(int id)
        {
            return await _mediator.Send(new Delete.Command {Id = id});
        }
    }
}