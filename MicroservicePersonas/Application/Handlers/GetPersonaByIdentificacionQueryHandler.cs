using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MicroservicePersonas.Application.DTOs;
using MicroservicePersonas.Application.Interfaces;
using MicroservicePersonas.Application.Queries;

namespace MicroservicePersonas.Application.Handlers
{
    public class GetPersonaByIdentificacionQueryHandler : IRequestHandler<GetPersonaByIdentificacionQuery, PersonaDTO>
    {
        private readonly IPersonaService _personaService;
        private readonly IMapper _mapper;

        public GetPersonaByIdentificacionQueryHandler(IPersonaService personaService, IMapper mapper)
        {
            _personaService = personaService;
            _mapper = mapper;
        }

        public async Task<PersonaDTO> Handle(GetPersonaByIdentificacionQuery request, CancellationToken cancellationToken)
        {
            var persona = await _personaService.GetByIdentificacion(request.TipoPersona, request.Identificacion);

            if (persona == null)
            {
                return null;
            }

            return _mapper.Map<PersonaDTO>(persona);
        }
    }
}