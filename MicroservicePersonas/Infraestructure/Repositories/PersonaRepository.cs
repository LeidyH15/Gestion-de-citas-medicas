using MicroservicePersonas.Domain.Entities;
using MicroservicePersonas.Domain.Interfaces;
using MicroservicePersonas.Infraestructure.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace MicroservicePersonas.Infraestructure.Repositories
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly PersonasDbContext _context;
        
        public PersonaRepository(PersonasDbContext context)
        {
            _context = context;
        }

        public async Task<List<Persona>> GetAll()
        {
            return await _context.Personas.ToListAsync();
        }

        public async Task<Persona> GetById(int id)
        {
            return await _context.Personas.FindAsync(id);
        }

        public async Task<Persona> GetByIdentificacion(int tipoPersona, string identificacion)
        {
            return await _context.Personas
                .FirstOrDefaultAsync(p => p.IdTipoPersona == tipoPersona && p.Identificacion == identificacion);
        }
        public async Task<string> Create(Persona persona)
        {
            try
            {
                _context.Personas.Add(persona);
                await _context.SaveChangesAsync();
                return "Persona creada exitosamente.";
            }
            catch (Exception ex)
            {
                return $"Error al crear persona: {ex.Message}";
            }
        }

        public async Task<string> Update(int id, Persona persona)
        {
            try
            {
                var existingPersona = await _context.Personas.FindAsync(id);
                if (existingPersona == null)
                    return "Persona no encontrada.";

                existingPersona.Nombre = persona.Nombre;
                existingPersona.Apellido = persona.Apellido;
                existingPersona.TipoPersona = persona.TipoPersona;
                existingPersona.Identificacion = persona.Identificacion;
                existingPersona.Fecha_Nacimiento = persona.Fecha_Nacimiento;
                existingPersona.Genero = persona.Genero;
                existingPersona.Telefono = persona.Telefono;
                existingPersona.Email = persona.Email;
                existingPersona.Especialidad = persona.Especialidad;

                await _context.SaveChangesAsync();
                return "Persona actualizada exitosamente.";
            }
            catch (Exception ex)
            {
                return $"Error al actualizar persona: {ex.Message}";
            }
        }

        public async Task<string> Delete(int id)
        {
            try
            {
                var persona = await _context.Personas.FindAsync(id);
                if (persona == null)
                    return "Persona no encontrada.";

                _context.Personas.Remove(persona);
                await _context.SaveChangesAsync();
                return $"Persona con ID: {id} eliminada correctamente.";
            }
            catch (Exception ex)
            {
                return $"Error al eliminar persona: {ex.Message}";
            }
        }

        public async Task<int> AddAsync(Persona persona)
        {
            _context.Personas.Add(persona);
            return await _context.SaveChangesAsync();
        }


    }

}