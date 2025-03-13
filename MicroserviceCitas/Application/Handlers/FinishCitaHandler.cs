using MediatR;
using MicroserviceCitas.Application.Commands;
using System.Threading.Tasks;
using System.Threading;
using MicroserviceCitas.Application.Interfaces;

namespace MicroserviceCitas.Application.Handlers
{
    public class FinishCitaHandler : IRequestHandler<FinishCitaCommand, string>
    {
        private readonly ICitaService _citaService;
        public FinishCitaHandler(ICitaService citaService) => _citaService = citaService;

        public async Task<string> Handle(FinishCitaCommand request, CancellationToken cancellationToken)
        {
            await _citaService.Finish(request.Id, request.Receta);
            return "Cita finalizada exitosamente.";
        }
    }
}