﻿using AutoMapper;
using MicroserviceRecetas.Application.DTOs;
using MicroserviceRecetas.Application.Interfaces;
using MicroserviceRecetas.Domain.Entities;
using MicroserviceRecetas.Domain.Interfaces;
using System.Threading.Tasks;

namespace MicroserviceRecetas.Application.Services
{
    public class RecetaService : IRecetaService
    {
        private readonly IRecetaRepository _recetaRepository;
        private readonly IPersonaService _personaService;
        private readonly ICitaService _citaService;
        private readonly IMapper _mapper;

        public RecetaService(IRecetaRepository recetaRepository, IPersonaService personaService, ICitaService citaService, IMapper mapper)
        {
            _recetaRepository = recetaRepository;
            _personaService = personaService;
            _citaService = citaService;
            _mapper = mapper;
        }

        public async Task<RecetaDTO> GetById(int id)
        {
            var receta = await _recetaRepository.GetById(id);
            if (receta == null) return null;
            return _mapper.Map<RecetaDTO>(receta);
        }

        public async Task<string> Create(RecetaDTO recetaDto)
        {
            CitaDTO cita = await _citaService.GetById(recetaDto.IdCita);

            if (cita == null)
            {
                return "La cita no exite";
            }

            PersonaDTO medico = await _personaService.GetByIdentificacion(1, recetaDto.Medico);
            if (medico == null)
            {
                return "El médico no existe.";
            }

            if (medico.Identificacion != cita.IdMedico.ToString())
            {
                return "El médico no pertenece a la cita.";
            }

            PersonaDTO paciente = await _personaService.GetByIdentificacion(2, recetaDto.Paciente);
            if (paciente == null)
            {
                return "El paciente no existe.";
            }

            if (paciente.Identificacion != cita.IdPaciente.ToString())
            {
                return "El paciente no pertenece a la cita.";
            }
            var receta = _mapper.Map<Receta>(recetaDto);
            receta.IdMedico = cita.IdMedico;
            receta.Medico = cita.Medico;
            receta.IdPaciente = cita.IdPaciente;
            receta.Paciente = cita.Paciente;

            return await _recetaRepository.Create(receta);
        }

        public async Task<string> Update(int id, RecetaDTO recetaDto)
        {
            var receta = _mapper.Map<Receta>(recetaDto);
            return await _recetaRepository.Update(id, receta);
        }

        public async Task<string> Delete(int id)
        {
            return await _recetaRepository.Delete(id);
        }
    }
}