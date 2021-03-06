﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.OperatingSystems;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperatingSystemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OperatingSystemsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        // GET api/operatingsystems
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<OperatingSystem>>> List()
        {
            return await _mediator.Send(new List.Query());
        }
        
        // GET api/operatingsystems/{id}
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<OperatingSystem>> Details(int id)
        {
            return await _mediator.Send(new Details.Query{Id = id});
        }

        // POST api/operatingsystems
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await _mediator.Send(command);
        }

        // PUT api/operatingsystems/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(int id, Edit.Command command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }

        // DELETE api/operatingsystems/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(int id)
        {
            return await _mediator.Send(new Delete.Command {Id = id});
        }
    }
}