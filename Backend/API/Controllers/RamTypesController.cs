using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.RamTypes;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RamTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RamTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        // GET api/ramTypes
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<RamType>>> List()
        {
            return await _mediator.Send(new List.Query());
        }
        
        // GET api/ramTypes/{id}
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<RamType>> Details(int id)
        {
            return await _mediator.Send(new Details.Query{RamTypeId = id});
        }

        // POST api/ramTypes
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await _mediator.Send(command);
        }

        // PUT api/ramTypes/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(int id, Edit.Command command)
        {
            command.RamTypeId = id;
            return await _mediator.Send(command);
        }

        // DELETE api/ramTypes/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(int id)
        {
            return await _mediator.Send(new Delete.Command {RamTypeId = id});
        }
    }
}