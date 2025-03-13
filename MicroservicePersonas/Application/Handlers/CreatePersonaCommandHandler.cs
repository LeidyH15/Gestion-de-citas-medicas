using MediatR;
using MicroservicePersonas.Application.Commands;
using System.Threading.Tasks;
using System.Threading;
using MicroservicePersonas.Application.Interfaces;

namespace MicroservicePersonas.Application.Handlers
{
    public class CreatePersonaCommandHandler : IRequestHandler<CreatePersonaCommand, string>
    {
        private readonly IPersonaService _personaService;
        public CreatePersonaCommandHandler(IPersonaService personaService) => _personaService = personaService;
        public async Task<string> Handle(CreatePersonaCommand request, CancellationToken cancellationToken)
            => await _personaService.Create(request.IdTipoPersona, request.Persona);
    }
}