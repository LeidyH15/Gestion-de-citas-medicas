using MediatR;
using MicroserviceRecetas.Application.DTOs;

namespace MicroserviceRecetas.Application.Commands
{
    public class UpdateRecetaCommand : IRequest<string>
    {
        public int Id { get; }
        public RecetaDTO RecetaDto { get; }

        public UpdateRecetaCommand(int id, RecetaDTO recetaDto)
        {
            Id = id;
            RecetaDto = recetaDto;
        }
    }
}