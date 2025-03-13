using MediatR;
using MicroserviceCitas.Application.DTOs;

namespace MicroserviceCitas.Application.Queries
{
    public class GetCitaByIdQuery : IRequest<CitaDTO>
    {
        public int Id { get; set; }
        public GetCitaByIdQuery(int id) => Id = id;
    }
}