using MediatR;
using System.Threading.Tasks;
using System.Threading;
using MicroserviceCitas.Application.Commands;
using MicroserviceCitas.Application.Interfaces;
using AutoMapper;
using MicroserviceCitas.Domain.Interfaces;
using MicroserviceCitas.Domain.Entities;
using MicroserviceCitas.Application.DTOs;

namespace MicroserviceCitas.Application.Handlers
{
    public class CreateCitaHandler : IRequestHandler<CreateCitaCommand, string>
    {
        private readonly ICitaRepository _citaRepository;
        private readonly IPersonaService _personaService;
        private readonly IMapper _mapper;

        public CreateCitaHandler(ICitaRepository citaRepository,
                                    IPersonaService personaService,
                                    IMapper mapper)
        {
            _citaRepository = citaRepository;
            _personaService = personaService;
            _mapper = mapper;
        }

        //public async Task<string> Handle(CreateCitaCommand request, CancellationToken cancellationToken)
        //=>  await _citaService.Create(request.Cita);

        public async Task<string> Handle(CreateCitaCommand request, CancellationToken cancellationToken)
        {
            var citaDto = request.Cita;
            citaDto.Estado = "Pendiente";

            PersonaDTO medico = await _personaService.GetByIdentificacion(1, citaDto.Medico);
            if (medico == null) return "El médico no existe.";

            PersonaDTO paciente = await _personaService.GetByIdentificacion(2, citaDto.Paciente);
            if (paciente == null) return "El paciente no existe.";

            var cita = _mapper.Map<Cita>(citaDto);
            cita.IdMedico = int.Parse(medico.Identificacion);
            cita.Medico = $"{medico.Nombre} {medico.Apellido}";
            cita.IdPaciente = int.Parse(paciente.Identificacion);
            cita.Paciente = $"{paciente.Nombre} {paciente.Apellido}";

            return await _citaRepository.Create(cita);
        }

    }

}