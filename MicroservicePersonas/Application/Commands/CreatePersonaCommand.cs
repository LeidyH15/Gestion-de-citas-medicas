using MediatR;
using MicroservicePersonas.Application.DTOs;

namespace MicroservicePersonas.Application.Commands
{
    public class CreatePersonaCommand : IRequest<string>
    {
    public int IdTipoPersona { get; set; }
        public PersonaDTO Persona { get; set; }
        public CreatePersonaCommand(int idTipoPersona, PersonaDTO persona)
        {
            IdTipoPersona = idTipoPersona;
            Persona = persona;
        }
    }
}
