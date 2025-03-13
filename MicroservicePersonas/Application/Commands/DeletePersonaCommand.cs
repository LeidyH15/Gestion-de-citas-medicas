using MediatR;


namespace MicroservicePersonas.Application.Commands
{
    public class DeletePersonaCommand : IRequest<string>
    {
        public int Id { get; set; }
        public DeletePersonaCommand(int id) => Id = id;
    }
}