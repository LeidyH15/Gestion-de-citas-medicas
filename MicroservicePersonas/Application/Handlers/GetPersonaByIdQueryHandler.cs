using MediatR;
using MicroservicePersonas.Application.Queries;
using System.Threading.Tasks;
using System.Threading;
using MicroservicePersonas.Application.DTOs;
using MicroservicePersonas.Application.Interfaces;

namespace MicroservicePersonas.Application.Handlers
{
    public class GetPersonaByIdQueryHandler : IRequestHandler<GetPersonaByIdQuery, PersonaDTO>
    {
        private readonly IPersonaService _personaService;
        public GetPersonaByIdQueryHandler(IPersonaService personaService) => _personaService = personaService;
        public async Task<PersonaDTO> Handle(GetPersonaByIdQuery request, CancellationToken cancellationToken) => await _personaService.GetById(request.Id);
    }
}