using MediatR;
using MicroserviceCitas.Application.DTOs;

namespace MicroserviceCitas.Application.Commands
{
    public class FinishCitaCommand : IRequest<string>
    {
        public int Id { get; }
        public RecetaDTO Receta { get; }

        public FinishCitaCommand(int id, RecetaDTO receta)
        {
            Id = id;
            Receta = receta;
        }
    }
}