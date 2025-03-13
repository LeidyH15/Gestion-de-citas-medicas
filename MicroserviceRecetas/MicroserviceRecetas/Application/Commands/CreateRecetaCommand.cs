using MediatR;
using MicroserviceRecetas.Application.DTOs;


namespace MicroserviceRecetas.Application.Commands
{
    public class CreateRecetaCommand : IRequest<string>
    {
        public RecetaDTO RecetaDto { get; }

        public CreateRecetaCommand(RecetaDTO recetaDto)
        {
            RecetaDto = recetaDto;
        }
    }
}