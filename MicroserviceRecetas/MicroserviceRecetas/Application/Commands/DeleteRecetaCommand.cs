using MediatR;

namespace MicroserviceRecetas.Application.Commands
{
    public class DeleteRecetaCommand : IRequest<string>
    {
        public int Id { get; }

        public DeleteRecetaCommand(int id)
        {
            Id = id;
        }
    }
}