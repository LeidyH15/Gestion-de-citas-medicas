﻿using MediatR;
using MicroserviceRecetas.Application.Commands;
using MicroserviceRecetas.Application.DTOs;
using MicroserviceRecetas.Application.Queries;
using System.Threading.Tasks;
using System.Web.Http;

namespace MicroserviceRecetas.Presentation.Controllers
{
    [Authorize]
    [RoutePrefix("api/Receta")]
    public class RecetaController : ApiController
    {
        private readonly IMediator _mediator;

        public RecetaController(IMediator mediator)
        {
            _mediator = mediator;

        }

        /// <summary>
        /// Obtiene una receta por su ID.
        /// </summary>
        /// <param name="id">ID de la receta.</param>
        /// <returns>Receta si es encontrada, de lo contrario NotFound.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            if (id <= 0) return BadRequest("El ID de la receta debe ser un número positivo.");

            var receta = await _mediator.Send(new GetRecetaByIdQuery(id));
            if (receta == null) return NotFound();

            return Ok(receta);
        }

        /// <summary>
        /// Crea una nueva receta.
        /// </summary>
        /// <param name="recetaDto">Datos de la receta a crear.</param>
        /// <returns>Mensaje de éxito o error.</returns>
        [HttpPost]
        public async Task<IHttpActionResult> Create([FromBody] RecetaDTO recetaDto)
        {
            if (recetaDto == null) return BadRequest("La información de la receta no puede ser nula.");

            string msj = await _mediator.Send(new CreateRecetaCommand(recetaDto));
            return Created("api/Recetas/", msj);
        }

        /// <summary>
        /// Actualiza una receta existente.
        /// </summary>
        /// <param name="id">ID de la receta a actualizar.</param>
        /// <param name="recetaDto">Nuevos datos de la receta.</param>
        /// <returns>Mensaje de éxito o error.</returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] RecetaDTO recetaDto)
        {
            if (id <= 0) return BadRequest("El ID de la receta debe ser un número positivo.");
            if (recetaDto == null) return BadRequest("La información de la receta no puede ser nula.");

            string msj = await _mediator.Send(new UpdateRecetaCommand(id, recetaDto));
            return Ok(msj);
        }


        /// <summary>
        /// Elimina una receta por su ID.
        /// </summary>
        /// <param name="id">ID de la receta a eliminar.</param>
        /// <returns>Mensaje de éxito o error.</returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest("El ID de la receta debe ser un número positivo.");

            string msj = await _mediator.Send(new DeleteRecetaCommand(id));
            return Ok(msj);
        }
    }
}