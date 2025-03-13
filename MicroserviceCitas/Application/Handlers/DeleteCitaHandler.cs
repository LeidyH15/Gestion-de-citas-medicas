using MediatR;
using MicroserviceCitas.Application.Commands;
using MicroserviceCitas.Domain.Interfaces;
using System.Threading.Tasks;
using System.Threading;


namespace MicroserviceCitas.Application.Handlers
{
    public class DeleteCitaHandler : IRequestHandler<DeleteCitaCommand, string>
    {
        private readonly ICitaRepository _citaRepository;

        public DeleteCitaHandler(ICitaRepository citaRepository)
        {
            _citaRepository = citaRepository;
        }

        public async Task<string> Handle(DeleteCitaCommand request, CancellationToken cancellationToken)
        {
            var cita = await _citaRepository.GetById(request.Id);
            if (cita == null)
            {
                return "Cita no encontrada.";
            }

            await _citaRepository.Delete(request.Id);
            return "Cita eliminada exitosamente.";
        }
    }
}