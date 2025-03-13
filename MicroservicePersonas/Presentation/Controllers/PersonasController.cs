using MediatR;
using MicroservicePersonas.Application.Commands;
using MicroservicePersonas.Application.Queries;
using MicroservicePersonas.Application.DTOs;
using System.Threading.Tasks;
using System.Web.Http;

namespace MicroservicePersonas.Presentation.Controllers
{
    [Authorize]
    [RoutePrefix("api/Personas")]
    public class PersonasController : ApiController
    {
        private readonly IMediator _mediator;

        public PersonasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            var personas = await _mediator.Send(new GetAllPersonasQuery());
            if (personas == null || personas.Count == 0)
                return NotFound();

            return Ok(personas);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var persona = await _mediator.Send(new GetPersonaByIdQuery(id));
            if (persona == null)
                return NotFound();

            return Ok(persona);
        }

        [HttpGet]
        [Route("buscar")]
        public async Task<IHttpActionResult> GetByIdentificacion(int tipoPersona, string identificacion)
        {
            if (tipoPersona <= 0 || (tipoPersona != 1 && tipoPersona != 2))
            {
                return BadRequest("El tipo de persona debe ser 1 (Médico) o 2 (Paciente).");
            }

            if (string.IsNullOrEmpty(identificacion))
            {
                return BadRequest("La identificación de la persona no puede ser nula o vacía.");
            }

            var persona = await _mediator.Send(new GetPersonaByIdentificacionQuery(tipoPersona, identificacion));

            if (persona == null)
            {
                return NotFound();
            }

            return Ok(persona);
        }

        [HttpPost]
        [Route("{idTipoPersona}")]
        public async Task<IHttpActionResult> Create(int idTipoPersona, [FromBody] PersonaDTO personaDto)
        {
            if (idTipoPersona != 1 && idTipoPersona != 2) return BadRequest("El tipo de persona debe ser 1 (Médico) o 2 (Paciente).");
            if (personaDto == null) return BadRequest("La información de la persona no puede ser nula.");

            string msj = await _mediator.Send(new CreatePersonaCommand(idTipoPersona, personaDto));
            return Created("api/Personas", msj);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] PersonaDTO personaDTO)
        {
            if (personaDTO == null) return BadRequest("La información de la persona no puede ser nula.");
            string result = await _mediator.Send(new UpdatePersonaCommand(id, personaDTO));
            return Ok(new { message = result });
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest("El ID debe ser un número positivo.");
            string result = await _mediator.Send(new DeletePersonaCommand(id));
            return Ok(new { message = result });
        }
    }
}