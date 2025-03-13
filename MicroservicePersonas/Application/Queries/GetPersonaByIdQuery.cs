using MediatR;
using MicroservicePersonas.Application.DTOs;

namespace MicroservicePersonas.Application.Queries
{
    public class GetPersonaByIdQuery : IRequest<PersonaDTO>
    {
        public int Id { get; set; }
        public GetPersonaByIdQuery(int id) => Id = id;
    }
}