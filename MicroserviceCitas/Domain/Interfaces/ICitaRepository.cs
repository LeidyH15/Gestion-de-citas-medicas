using MicroserviceCitas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceCitas.Domain.Interfaces
{
    public interface ICitaRepository
    {
        Task<Cita> GetById(int id);
        Task<string> Create(Cita cita);
        Task<string> Update(int id, Cita cita);
        Task<string> Finish(Cita cita);
        Task<string> Delete(int id);
    }
}
