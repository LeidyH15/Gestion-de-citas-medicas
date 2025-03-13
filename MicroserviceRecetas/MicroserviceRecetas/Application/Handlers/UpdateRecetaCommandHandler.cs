using MediatR;
using MicroserviceRecetas.Application.Commands;
using MicroserviceRecetas.Application.Interfaces;
using System.Threading.Tasks;
using System.Threading;

namespace MicroserviceRecetas.Application.Handlers
{
    public class UpdateRecetaCommandHandler : IRequestHandler<UpdateRecetaCommand, string>
    {
        private readonly IRecetaService _recetaService;

        public UpdateRecetaCommandHandler(IRecetaService recetaService)
        {
            _recetaService = recetaService;
        }

        public async Task<string> Handle(UpdateRecetaCommand request, CancellationToken cancellationToken)
        {
            return await _recetaService.Update(request.Id, request.RecetaDto);
        }
    }
}