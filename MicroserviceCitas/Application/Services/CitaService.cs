﻿using AutoMapper;
using MicroserviceCitas.Application.DTOs;
using MicroserviceCitas.Application.Interfaces;
using MicroserviceCitas.Domain.Entities;
using MicroserviceCitas.Domain.Interfaces;
using System.Threading.Tasks;

namespace MicroserviceCitas.Application.Services
{
	public class CitaService : ICitaService
    {
        private readonly ICitaRepository _citaRepository;
        private readonly IPersonaService _personaService;
        private readonly IMapper _mapper;
        private readonly IRabbitMQSender _rabbitMqSender;

        public CitaService(ICitaRepository citaRepository, IPersonaService personaService, IMapper mapper, IRabbitMQSender rabbitMqSender)
        {
            _citaRepository = citaRepository;
            _personaService = personaService;
            _mapper = mapper;
            _rabbitMqSender = rabbitMqSender;
        }

        public async Task<CitaDTO> GetById(int id)
        {
            var cita = await _citaRepository.GetById(id);
            if (cita == null) return null;
            return _mapper.Map<CitaDTO>(cita);
        }

        public async Task<string> Create(CitaDTO citaDto)
        {
            citaDto.Estado = "Pendiente";

            PersonaDTO medico = await _personaService.GetByIdentificacion(1, citaDto.Medico);
            if (medico == null)
            {
                return "El médico no existe.";
            }

            PersonaDTO paciente = await _personaService.GetByIdentificacion(2, citaDto.Paciente);
            if (paciente == null)
            {
                return "El paciente no existe.";
            }

            var cita = _mapper.Map<Cita>(citaDto);

            cita.IdMedico = medico.Id;
            cita.Medico = $"{medico.Nombre} {medico.Apellido}";
            cita.IdPaciente = paciente.Id;
            cita.Paciente = $"{paciente.Nombre} {paciente.Apellido}";

            return await _citaRepository.Create(cita);
        }

        
        public async Task<string> Finish(int id, RecetaDTO recetaDto)
        {
            var cita = await _citaRepository.GetById(id);
            if (cita == null)
            {
                return "Cita no encontrada.";
            }
            // Enviar mensaje a RabbitMQ
            _rabbitMqSender.SendMessage(recetaDto);
            var finish = await _citaRepository.Finish(cita);
            return finish;
        }

        public async Task<string> Update(int id, CitaDTO citaDto)
        {
            var cita = _mapper.Map<Cita>(citaDto);
            return await _citaRepository.Update(id, cita);
        }

        public async Task<string> Delete(int id)
        {
            return await _citaRepository.Delete(id);
        }
    }
}