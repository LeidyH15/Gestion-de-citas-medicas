﻿using MicroserviceCitas.Domain.Entities;
using MicroserviceCitas.Domain.Interfaces;
using MicroserviceCitas.Infraestructure.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MicroserviceCitas.Infraestructure.Repositories
{
    public class CitaRepository : ICitaRepository
    {
        private readonly CitasContext _context;

        public CitaRepository(CitasContext context)
        {
            _context = context;
        }

        public async Task<Cita> GetById(int id)
        {
            return await _context.Citas
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<string> Create(Cita cita)
        {
            _context.Citas.Add(cita);
            await _context.SaveChangesAsync();
            return $"Cita creada con ID: {cita.Id}";
        }

        public async Task<string> Update(int id, Cita cita)
        {
            var citaExistente = await _context.Citas.Where(w => w.Id == id).FirstOrDefaultAsync();
            if (citaExistente == null)
            {
                return "Cita no encontrada.";
            }

            // Actualizar los campos
          //  citaExistente.IdPaciente = cita.IdPaciente;
          // citaExistente.Paciente = cita.Paciente;
          //  citaExistente.IdMedico = cita.IdMedico;
         //  citaExistente.Medico = cita.Medico;
            citaExistente.Fecha_Hora = cita.Fecha_Hora;
            citaExistente.Motivo = cita.Motivo;
            citaExistente.Lugar = cita.Lugar;
            citaExistente.Estado = cita.Estado;

            await _context.SaveChangesAsync();
            return $"Cita con ID: {id} actualizada correctamente.";
        }

        public async Task<string> Finish(Cita cita)
        {
            cita.Estado = "Finalizada";
            await _context.SaveChangesAsync();
            return $"Cita con ID: {cita.Id} Finalizada correctamente.";
        }

        public async Task<string> Delete(int id)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita == null)
            {
                return "Cita no encontrada.";
            }
            _context.Citas.Remove(cita);
            await _context.SaveChangesAsync();
            return $"Cita con ID: {id} eliminada correctamente.";
        }
    }
}