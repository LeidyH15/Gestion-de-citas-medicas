using MediatR;
using MicroservicePersonas.Application.Commands;
using System.Threading.Tasks;
using System.Threading;
using MicroservicePersonas.Application.Interfaces;

namespace MicroservicePersonas.Application.Handlers
{
    public class DeletePersonaCommandHandler : IRequestHandler<DeletePersonaCommand, string>
    {
        private readonly IPersonaService _personaService;
        public DeletePersonaCommandHandler(IPersonaService personaService) => _personaService = personaService;
        public async Task<string> Handle(DeletePersonaCommand request, CancellationToken cancellationToken)
            => await _personaService.Delete(request.Id);
    }
}