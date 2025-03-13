using MediatR;
using MicroservicePersonas.Application.DTOs;

namespace MicroservicePersonas.Application.Queries
{
    public class GetPersonaByIdentificacionQuery : IRequest<PersonaDTO>
    {
        public int TipoPersona { get; set; }
        public string Identificacion { get; set; }
        public GetPersonaByIdentificacionQuery(int tipoPersona, string identificacion)
        {
            TipoPersona = tipoPersona;
            Identificacion = identificacion;
        }
    }
}