using MicroserviceRecetas.Domain.Entities;
using MicroserviceRecetas.Domain.Interfaces;
using MicroserviceRecetas.Infraestructure.Percistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MicroserviceRecetas.Infraestructure.Repositories
{
    public class RecetaRepository : IRecetaRepository
    {
        private readonly RecetasContext _context;

        public RecetaRepository(RecetasContext context)
        {
            _context = context;
        }

        public async Task<Receta> GetById(int id)
        {
            return await _context.Recetas
                .Include("Medicamentos")
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<string> Create(Receta receta)
        {
            if (receta.Medicamentos != null && receta.Medicamentos.Count > 0)
            {
                foreach (var medicamento in receta.Medicamentos)
                {
                    medicamento.Receta = receta;
                }
            }
            _context.Recetas.Add(receta);
            await _context.SaveChangesAsync();

            return $"Receta creada con ID: {receta.Id}";
        }

        public async Task<string> Update(int id, Receta receta)
        {
            var recetaExistente = await _context.Recetas.Where(w => w.Id == id).FirstOrDefaultAsync();
            if (recetaExistente == null)
            {
                return "Receta no encontrada.";
            }

            // Actualiza los campos
            recetaExistente.Descriptor = receta.Descriptor;
            recetaExistente.Medicamentos = receta.Medicamentos;
            
            await _context.SaveChangesAsync();
            return $"Receta con ID: {id} actualizada correctamente.";
        }

        public async Task<string> Delete(int id)
        {
            var persona = await _context.Recetas.FindAsync(id);
            if (persona == null)
            {
                return "Receta no encontrada.";
            }
            _context.Recetas.Remove(persona);
            await _context.SaveChangesAsync();
            return $"Receta con ID: {id} eliminada correctamente.";
        }
    }
}