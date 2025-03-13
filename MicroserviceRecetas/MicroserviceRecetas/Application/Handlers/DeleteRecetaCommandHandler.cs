using MediatR;
using MicroserviceRecetas.Application.Commands;
using MicroserviceRecetas.Application.Interfaces;
using System.Threading.Tasks;
using System.Threading;

namespace MicroserviceRecetas.Application.Handlers
{
    public class DeleteRecetaCommandHandler : IRequestHandler<DeleteRecetaCommand, string>
    {
        private readonly IRecetaService _recetaService;

        public DeleteRecetaCommandHandler(IRecetaService recetaService)
        {
            _recetaService = recetaService;
        }

        public async Task<string> Handle(DeleteRecetaCommand request, CancellationToken cancellationToken)
        {
            return await _recetaService.Delete(request.Id);
        }
    }
}