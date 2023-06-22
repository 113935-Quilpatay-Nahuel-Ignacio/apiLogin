using ApiLogin.DTOs;
using ApiLogin.Services.PersonaService.Commands;
using ApiLogin.Services.PersonaService.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiLogin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("postPersona")]
        public async Task<ActionResult> PostPersona(PostPersonaCommand command)
        {
            try
            {
                var postPersona = await _mediator.Send(command);
                return CreatedAtAction(nameof(PostPersona), postPersona);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getPersonas")]
        public async Task<List<PersonaDTO>> GetPersonas()
        {
            return await _mediator.Send(new GetPersonasQuery());
        }

        [HttpPost]
        [Route("loginPersona")]
        public async Task<ActionResult> LoginPersona(LoginPersonaCommand cmd)
        {
            try
            {
                var postP = await _mediator.Send(cmd);
                return CreatedAtAction(nameof(LoginPersona), postP);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
