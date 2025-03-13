using MicroservicePersonas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicePersonas.Domain.Interfaces
{
    public interface IPersonaRepository
    {
        Task<List<Persona>> GetAll();
        Task<Persona> GetById(int id);
        Task<Persona> GetByIdentificacion(int TipoPersona, string Identificacion);
        Task<string> Create(Persona persona);
        Task<string> Update(int id, Persona persona);
        Task<string> Delete(int id);
        Task<int> AddAsync(Persona persona);
    }
}
