using MediatR;
using MicroserviceCitas.Application.DTOs;

namespace MicroserviceCitas.Application.Commands
{
	public class CreateCitaCommand : IRequest<string>
    {
        public CitaDTO Cita { get; }

        public CreateCitaCommand(CitaDTO cita)
        {
            Cita = cita;
        }
    }
}
