using MediatR;


namespace MicroserviceCitas.Application.Commands
{
    public class DeleteCitaCommand : IRequest<string>
    {
        public int Id { get; }

        public DeleteCitaCommand(int id)
        {
            Id = id;
        }
    }
}