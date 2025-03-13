using MediatR;
using MicroservicePersonas.Application.DTOs;

namespace MicroservicePersonas.Application.Commands
{
    public class UpdatePersonaCommand : IRequest<string>
    {
        public int Id { get; set; }
        public PersonaDTO Persona { get; set; }
        public UpdatePersonaCommand(int id, PersonaDTO persona)
        {
            Id = id;
            Persona = persona;
        }
    }
}