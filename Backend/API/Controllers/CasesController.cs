using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.Cases;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CasesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CasesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        // GET api/cases
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<Case>>> List()
        {
            return await _mediator.Send(new List.Query());
        }
        
        // GET api/cases/{id}
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Case>> Details(int id)
        {
            return await _mediator.Send(new Details.Query{Id = id});
        }
        
        // POST api/cases
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await _mediator.Send(command);
        }

        // PUT api/cases/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(int id, Edit.Command command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }

        // DELETE api/cases/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(int id)
        {
            return await _mediator.Send(new Delete.Command {Id = id});
        }
    }
}