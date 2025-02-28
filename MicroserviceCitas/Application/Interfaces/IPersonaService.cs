using MicroserviceCitas.Application.DTOs;
using System.Threading.Tasks;

namespace MicroserviceCitas.Application.Interfaces
{
   public interface IPersonaService
    {
        Task<PersonaDTO> GetByIdentificacion(int TipoPersona, string Identificacion);

    }
}
