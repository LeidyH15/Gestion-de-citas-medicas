using MediatR;
using MicroserviceRecetas.Application.Commands;
using MicroserviceRecetas.Application.Interfaces;
using System.Threading.Tasks;
using System.Threading;

namespace MicroserviceRecetas.Application.Handlers
{
    public class CreateRecetaCommandHandler : IRequestHandler<CreateRecetaCommand, string>
    {
        private readonly IRecetaService _recetaService;

        public CreateRecetaCommandHandler(IRecetaService recetaService)
        {
            _recetaService = recetaService;
        }

        public async Task<string> Handle(CreateRecetaCommand request, CancellationToken cancellationToken)
        {
            return await _recetaService.Create(request.RecetaDto);
        }
    }
}