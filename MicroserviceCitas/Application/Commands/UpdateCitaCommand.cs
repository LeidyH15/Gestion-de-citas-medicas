using MediatR;
using MicroserviceCitas.Application.DTOs;


namespace MicroserviceCitas.Application.Commands
{
    public class UpdateCitaCommand : IRequest<string>
    {
        public int Id { get; }
        public CitaDTO Cita { get; }

        public UpdateCitaCommand(int id, CitaDTO cita)
        {
            Id = id;
            Cita = cita;
        }
    }
}