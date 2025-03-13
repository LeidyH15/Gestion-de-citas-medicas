using MediatR;
using MicroserviceRecetas.Application.DTOs;

namespace MicroserviceRecetas.Application.Queries
{
    public class GetRecetaByIdQuery : IRequest<RecetaDTO>
    {
        public int Id { get; }

        public GetRecetaByIdQuery(int id)
        {
            Id = id;
        }
    }
}