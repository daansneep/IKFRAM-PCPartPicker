using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.Parts;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PartsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        // GET api/parts
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<Part>>> List()
        {
            return await _mediator.Send(new List.Query());
        }
        
        // GET api/parts/{id}
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Part>> Details(int id)
        {
            return await _mediator.Send(new Details.Query{PartId = id});
        }

        // POST api/parts
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await _mediator.Send(command);
        }

        // PUT api/parts/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(int id, Edit.Command command)
        {
            command.PartId = id;
            return await _mediator.Send(command);
        }

        // DELETE api/parts/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(int id)
        {
            return await _mediator.Send(new Delete.Command {PartId = id});
        }
    }
}