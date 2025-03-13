using MediatR;
using MicroserviceCitas.Application.Commands;
using MicroserviceCitas.Application.DTOs;
using MicroserviceCitas.Application.Queries;
using System.Threading.Tasks;
using System.Web.Http;

namespace MicroserviceCitas.Presentation.Controllers
{
    [Authorize]
    [RoutePrefix("api/Citas")]
   public class CitasController : ApiController
   {

        private readonly IMediator _mediator;

        public CitasController(IMediator mediator)
        {
            _mediator = mediator;

        }

        /// <summary>
        /// Obtiene una cita por su ID.
        /// </summary>
        /// <param name="id">ID de la cita.</param>
        /// <returns>La cita si es encontrada, de lo contrario, NotFound.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var cita = await _mediator.Send(new GetCitaByIdQuery(id));
            if (cita == null)
                return NotFound();

            return Ok(cita);
        }


        /// <summary>
        /// Crea una nueva cita.
        /// </summary>
        /// <param name="citaDto">Datos de la cita a crear.</param>
        /// <returns>Mensaje de éxito o error.</returns>
        [HttpPost]
        [Route("Create")]
        public async Task<IHttpActionResult> Create([FromBody] CitaDTO citaDto)
        {
            if (citaDto == null)
                return BadRequest("Los datos de la cita son inválidos.");

            var result = await _mediator.Send(new CreateCitaCommand(citaDto));

            return Ok(new { message = result });
        }


        [HttpPost]
        [Route("Finish/{id}")]
        public async Task<IHttpActionResult> Finish(int id, [FromBody] RecetaDTO recetaDto)
        {
            if (recetaDto == null) return BadRequest("Datos de la receta inválidos.");

            var result = await _mediator.Send(new FinishCitaCommand(id, recetaDto));
            return Ok(result);
        }



        /// <summary>
        /// Actualiza una cita existente.
        /// </summary>
        /// <param name="id">ID de la cita a actualizar.</param>
        /// <param name="citaDto">Nuevos datos de la cita.</param>
        /// <returns>Mensaje de éxito o error.</returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] CitaDTO citaDto)
        {
            if (id <= 0)
                return BadRequest("El ID de la cita debe ser un número positivo.");

            if (citaDto == null)
                return BadRequest("Los datos de la cita son inválidos.");

            var result = await _mediator.Send(new UpdateCitaCommand(id, citaDto));

            return Ok(new { message = result });
        }

        /// <summary>
        /// Elimina una cita por su ID.
        /// </summary>
        /// <param name="id">ID de la cita a eliminar.</param>
        /// <returns>Mensaje de éxito o error.</returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("El ID de la cita debe ser un número positivo.");
            }

            var result = await _mediator.Send(new DeleteCitaCommand(id));

            return Ok(result);
        }

    }
}