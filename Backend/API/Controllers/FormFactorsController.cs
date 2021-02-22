using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.FormFactors;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormFactorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FormFactorsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        // GET api/formFactors
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<FormFactor>>> List()
        {
            return await _mediator.Send(new List.Query());
        }
        
        // GET api/formFactors/{id}
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<FormFactor>> Details(int id)
        {
            return await _mediator.Send(new Details.Query{FormFactorId = id});
        }

        // POST api/formFactors
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await _mediator.Send(command);
        }

        // PUT api/formFactors/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(int id, Edit.Command command)
        {
            command.FormFactorId = id;
            return await _mediator.Send(command);
        }

        // DELETE api/formFactors/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(int id)
        {
            return await _mediator.Send(new Delete.Command {FormFactorId = id});
        }
    }
}