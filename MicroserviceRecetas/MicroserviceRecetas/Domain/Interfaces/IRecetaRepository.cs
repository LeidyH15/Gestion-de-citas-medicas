using MicroserviceRecetas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceRecetas.Domain.Interfaces
{
    public interface IRecetaRepository
    {
        Task<Receta> GetById(int id);
        Task<string> Create(Receta receta);
        Task<string> Update(int id, Receta receta);
        Task<string> Delete(int id);
    }
}
