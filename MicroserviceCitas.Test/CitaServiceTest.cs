using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using MicroserviceCitas.Application.Commands;
using MicroserviceCitas.Application.DTOs;
using MicroserviceCitas.Application.Queries;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace MicroserviceCitas.Test
{
    [TestFixture]
    public class CitaCQRSHandlerTest
    {
        private Mock<IMediator> _mockMediator;
        private ClaimsPrincipal _user;

        [SetUp]
        public void Setup()
        {
            _mockMediator = new Mock<IMediator>();
            _user = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim("sub", "12345") }));
        }

        [Test]
        public async Task GetById_ShouldReturnCitaDTO()
        {
            // Arrange
            var citaDto = new CitaDTO { IdPaciente = "1", Paciente = "Stephania", Estado = "Pendiente" };
            _mockMediator.Setup(m => m.Send(It.IsAny<GetCitaByIdQuery>(), default)).ReturnsAsync(citaDto);

            // Act
            var result = await _mockMediator.Object.Send(new GetCitaByIdQuery(1));

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Pendiente", result.Estado);
        }

        [Test]
        public async Task GetById_ShouldReturnNull_WhenCitaNotFound()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<GetCitaByIdQuery>(), default)).ReturnsAsync((CitaDTO)null);

            // Act
            var result = await _mockMediator.Object.Send(new GetCitaByIdQuery(1));

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task Create_ShouldReturnSuccessMessage()
        {
            // Arrange
            var citaDto = new CitaDTO { Paciente = "Stephania", Medico = "Brandon" };
            _mockMediator.Setup(m => m.Send(It.IsAny<CreateCitaCommand>(), default)).ReturnsAsync("Created");

            // Act
            var result = await _mockMediator.Object.Send(new CreateCitaCommand(citaDto));

            // Assert
            Assert.AreEqual("Created", result);
        }

        [Test]
        public async Task Update_ShouldReturnUpdatedMessage()
        {
            // Arrange
            var citaDto = new CitaDTO { Paciente = "Stephania", Medico = "Brandon" };
            _mockMediator.Setup(m => m.Send(It.IsAny<UpdateCitaCommand>(), default)).ReturnsAsync("Updated");

            // Act
            var result = await _mockMediator.Object.Send(new UpdateCitaCommand(1, citaDto));

            // Assert
            Assert.AreEqual("Updated", result);
        }

        [Test]
        public async Task Delete_ShouldReturnDeletedMessage()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<DeleteCitaCommand>(), default)).ReturnsAsync("Deleted");

            // Act
            var result = await _mockMediator.Object.Send(new DeleteCitaCommand(1));

            // Assert
            Assert.AreEqual("Deleted", result);
        }

    }
}