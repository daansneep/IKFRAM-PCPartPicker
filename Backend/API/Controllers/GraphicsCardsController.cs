using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.GraphicsCards;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GraphicsCardsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GraphicsCardsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        // GET api/graphicscards
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<GraphicsCard>>> List()
        {
            return await _mediator.Send(new List.Query());
        }
        
        // GET api/graphicscards/{id}
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<GraphicsCard>> Details(int id)
        {
            return await _mediator.Send(new Details.Query{Id = id});
        }

        // POST api/graphicscards
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await _mediator.Send(command);
        }

        // PUT api/graphicscards/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(int id, Edit.Command command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }

        // DELETE api/graphicscards/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(int id)
        {
            return await _mediator.Send(new Delete.Command {Id = id});
        }
    }
}