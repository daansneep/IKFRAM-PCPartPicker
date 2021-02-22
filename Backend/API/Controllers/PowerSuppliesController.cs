using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.PowerSupplies;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PowerSuppliesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PowerSuppliesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        // GET api/powersupplies
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<PowerSupply>>> List()
        {
            return await _mediator.Send(new List.Query());
        }
        
        // GET api/powersupplies/{id}
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<PowerSupply>> Details(int id)
        {
            return await _mediator.Send(new Details.Query{Id = id});
        }

        // POST api/powersupplies
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await _mediator.Send(command);
        }

        // PUT api/powersupplies/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(int id, Edit.Command command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }

        // DELETE api/powersupplies/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(int id)
        {
            return await _mediator.Send(new Delete.Command {Id = id});
        }
    }
}