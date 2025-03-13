using MediatR;
using MicroservicePersonas.Application.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using MicroservicePersonas.Application.DTOs;
using MicroservicePersonas.Application.Interfaces;

namespace MicroservicePersonas.Application.Handlers
{
    public class GetAllPersonasQueryHandler : IRequestHandler<GetAllPersonasQuery, List<PersonaDTO>>
    {
        private readonly IPersonaService _personaService;
        public GetAllPersonasQueryHandler(IPersonaService personaService) => _personaService = personaService;
        public async Task<List<PersonaDTO>> Handle(GetAllPersonasQuery request, CancellationToken cancellationToken) => await _personaService.GetAll();
    }
}