using MicroservicePersonas.Application.DTOs;
using MicroservicePersonas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicePersonas.Application.Interfaces
{
    public interface IPersonaService
    {
        Task<List<PersonaDTO>> GetAll();
        Task<PersonaDTO> GetById(int id);
        Task<Persona> GetByIdentificacion(int TipoPersona, string Identificacion);
        Task<string> Create(int idTipoPersona, PersonaDTO personaDto);
        Task<string> Update(int id, PersonaDTO personaDto);
        Task<string> Delete(int id);
    }
}
