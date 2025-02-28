using MicroservicePersonas.Application.DTOs;
using MicroservicePersonas.Application.Interfaces;
using MicroservicePersonas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MicroservicePersonas.Presentation.Controllers
{
    [RoutePrefix("api/Personas")]
    public class PersonasController : ApiController
    {
        private readonly IPersonaService _personaService;

        public PersonasController(IPersonaService personaService)
        {
            _personaService = personaService;
        }

        /// <summary>
        /// Obtiene todas las personas.
        /// </summary>
        /// <returns>Lista de personas.</returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            var personas = await _personaService.GetAll();
            if (personas == null || personas.Count == 0)
            {
                return NotFound();
            }

            return Ok(personas);
        }

        /// <summary>
        /// Obtiene una persona por su ID.
        /// </summary>/// <param name="id">ID de la persona.</param>
        /// <returns>Persona si es encontrada, de lo contrario NotFound.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var persona = await _personaService.GetById(id);
            if (persona == null) return NotFound();

            return Ok(persona);
        }

        /// <summary>
        /// Obtiene una persona por tipo y número de identificación.
        /// </summary>
        /// /// <param name="TipoPersona">Tipo de la persona</param>
        /// <param name="Identificacion">Indentificacion de la persona</param>
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
                return BadRequest("La Identificacion de la persona no puede ser nula o vacia.");
            }

            var persona = await _personaService.GetByIdentificacion(tipoPersona, identificacion);
            if (persona == null)
            {
                return NotFound();
            }

            return Ok(persona);
        }

        /// <summary>
        /// Crea una nueva persona.
        /// </summary>
        /// /// <param name="idTipoPersona">ID del tipo de persona (1 para Médico, 2 para Paciente).</param>
        /// <param name="personaDto">Datos de la persona a crear.</param>
        [HttpPost]
        [Route("{idTipoPersona}")]
        public async Task<IHttpActionResult> Create(int idTipoPersona, [FromBody] PersonaDTO personaDto)
        {
            if (idTipoPersona <= 0 || (idTipoPersona != 1 && idTipoPersona != 2))
            {
                return BadRequest("El tipo de persona debe ser 1 (Médico) o 2 (Paciente).");
            }

            if (personaDto == null)
            {
                return BadRequest("La información de la persona no puede ser nula.");
            }

            string msj = await _personaService.Create(idTipoPersona, personaDto);
            return Created("api/Personas", msj);
        }

        /// <summary>
        /// Actualiza una persona existente.
        /// </summary>
        /// <param name="id">ID de la persona a actualizar.</param>
        /// <param name="personaDTO">Nuevos datos de la persona.</param>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] PersonaDTO personaDTO)
        {
            if (personaDTO == null)
            {
                return BadRequest("La información de la persona no puede ser nula.");
            }

            var result = await _personaService.Update(id, personaDTO);
            return Ok(new { message = result });
        }

        /// <summary>
        /// Elimina una persona por su ID.
        /// </summary>
        /// <param name="id">ID de la persona a eliminar.</param>
        /// <returns>Mensaje de éxito o error.</returns>
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            if(id <= 0)
            {
                return BadRequest("El ID de la persona debe ser un número positivo.");
            }
            var result = await _personaService.Delete(id);
            return Ok(new { message = result });
        }
    }
}