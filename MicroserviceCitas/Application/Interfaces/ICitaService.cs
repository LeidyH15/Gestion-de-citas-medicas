using MicroserviceCitas.Application.DTOs;
using MicroserviceCitas.Domain.Entities;
using System.Threading.Tasks;

namespace MicroserviceCitas.Application.Interfaces
{
    public interface ICitaService
    {
    Task<Cita> GetById(int id);
    Task<string> Create(CitaDTO citaDto);
    Task<string> Finish(int id, RecetaDTO recetaDto);
    Task<string> Update(int id, CitaDTO citaDto);
    Task<string> Delete(int id);
    
    }
}
