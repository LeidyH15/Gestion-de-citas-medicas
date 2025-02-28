using MicroserviceRecetas.Application.DTOs;
using System.Threading.Tasks;

namespace MicroserviceRecetas.Application.Interfaces
{
    public interface IPersonaService
    {
        Task<PersonaDTO> GetByIdentificacion(int TipoPersona, string Identificacion);
    }
}