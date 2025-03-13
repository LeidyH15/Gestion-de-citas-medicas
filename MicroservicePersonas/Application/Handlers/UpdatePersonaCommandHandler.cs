using MediatR;
using MicroservicePersonas.Application.Commands;
using System.Threading.Tasks;
using System.Threading;
using MicroservicePersonas.Application.Interfaces;

namespace MicroservicePersonas.Application.Handlers
{
    public class UpdatePersonaCommandHandler : IRequestHandler<UpdatePersonaCommand, string>
    {
        private readonly IPersonaService _personaService;
        public UpdatePersonaCommandHandler(IPersonaService personaService) => _personaService = personaService;
        public async Task<string> Handle(UpdatePersonaCommand request, CancellationToken cancellationToken)
            => await _personaService.Update(request.Id, request.Persona);
    }
}