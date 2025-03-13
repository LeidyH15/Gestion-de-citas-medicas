using MediatR;
using MicroserviceRecetas.Application.Interfaces;
using MicroserviceRecetas.Application.Queries;
using System.Threading.Tasks;
using System.Threading;
using MicroserviceRecetas.Application.DTOs;

namespace MicroserviceRecetas.Application.Handlers
{
    public class GetRecetaByIdQueryHandler : IRequestHandler<GetRecetaByIdQuery, RecetaDTO>
    {
        private readonly IRecetaService _recetaService;

        public GetRecetaByIdQueryHandler(IRecetaService recetaService)
        {
            _recetaService = recetaService;
        }

        public async Task<RecetaDTO> Handle(GetRecetaByIdQuery request, CancellationToken cancellationToken)
        {
            return await _recetaService.GetById(request.Id);
        }
    }
}