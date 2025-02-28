using MicroserviceRecetas.Application.DTOs;
using System.Threading.Tasks;

namespace MicroserviceRecetas.Application.Interfaces
{
    public interface ICitaService
    {
        Task<CitaDTO> GetById(int id);
    }
}
