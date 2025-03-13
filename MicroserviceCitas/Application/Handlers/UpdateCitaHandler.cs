using MediatR;
using MicroserviceCitas.Application.Commands;
using System.Threading.Tasks;
using System.Threading;
using MicroserviceCitas.Application.Interfaces;

namespace MicroserviceCitas.Application.Handlers
{
    public class UpdateCitaHandler : IRequestHandler<UpdateCitaCommand, string>
    {
        private readonly ICitaService _citaService;
        public UpdateCitaHandler(ICitaService citaService) => _citaService = citaService;
        public async Task<string> Handle(UpdateCitaCommand request, CancellationToken cancellationToken)
        {
            var citaExistente = await _citaService.GetById(request.Id);
            if (citaExistente == null)
            {
                return "Cita no encontrada.";
            }

            await _citaService.Update(request.Id, request.Cita);
            return "Cita actualizada exitosamente.";
        }
    }
}