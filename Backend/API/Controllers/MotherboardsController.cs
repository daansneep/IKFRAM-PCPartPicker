using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.Motherboards;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotherboardsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MotherboardsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        // GET api/motherboards
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<Motherboard>>> List()
        {
            return await _mediator.Send(new List.Query());
        }
        
        // GET api/motherboards/{id}
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Motherboard>> Details(int id)
        {
            return await _mediator.Send(new Details.Query{Id = id});
        }

        // POST api/motherboards
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await _mediator.Send(command);
        }

        // PUT api/motherboards/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(int id, Edit.Command command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }

        // DELETE api/motherboards/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(int id)
        {
            return await _mediator.Send(new Delete.Command {Id = id});
        }
    }
}