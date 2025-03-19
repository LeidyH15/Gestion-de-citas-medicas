using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using MicroservicePersonas.Application.Commands;
using MicroservicePersonas.Application.DTOs;
using MicroservicePersonas.Application.Queries;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace MicroservicePersonas.Test
{
    [TestFixture]
    public class PersonaCQRSHandlerTest
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
        public async Task GetAllPersonas_ShouldReturnListOfPersonaDTOs()
        {
            // Arrange
            var personas = new List<PersonaDTO>
            {
                new PersonaDTO { Nombre = "Daniela" },
                new PersonaDTO { Nombre = "Maria" }
            };

            _mockMediator.Setup(m => m.Send(It.IsAny<GetAllPersonasQuery>(), default)).ReturnsAsync(personas);

            // Act
            var result = await _mockMediator.Object.Send(new GetAllPersonasQuery());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Daniela", result[0].Nombre);
        }

        [Test]
        public async Task GetPersonaById_ShouldReturnPersonaDTO()
        {
            // Arrange
            var persona = new PersonaDTO { Nombre = "Daniela" };
            _mockMediator.Setup(m => m.Send(It.IsAny<GetPersonaByIdQuery>(), default)).ReturnsAsync(persona);

            // Act
            var result = await _mockMediator.Object.Send(new GetPersonaByIdQuery(1));

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Daniela", result.Nombre);
        }

        [Test]
        public async Task CreatePersona_ShouldReturnSuccessMessage()
        {
            // Arrange
            var personaDto = new PersonaDTO { Nombre = "Lina" };
            _mockMediator.Setup(m => m.Send(It.IsAny<CreatePersonaCommand>(), default)).ReturnsAsync("Success");

            // Act
            var result = await _mockMediator.Object.Send(new CreatePersonaCommand(1, personaDto));

            // Assert
            Assert.AreEqual("Success", result);
        }

        [Test]
        public async Task UpdatePersona_ShouldReturnUpdatedMessage()
        {
            // Arrange
            var personaDto = new PersonaDTO { Nombre = "Luis" };
            _mockMediator.Setup(m => m.Send(It.IsAny<UpdatePersonaCommand>(), default)).ReturnsAsync("Updated");

            // Act
            var result = await _mockMediator.Object.Send(new UpdatePersonaCommand(1, personaDto));

            // Assert
            Assert.AreEqual("Updated", result);
        }

        [Test]
        public async Task DeletePersona_ShouldReturnDeletedMessage()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<DeletePersonaCommand>(), default)).ReturnsAsync("Deleted");

            // Act
            var result = await _mockMediator.Object.Send(new DeletePersonaCommand(1));

            // Assert
            Assert.AreEqual("Deleted", result);
        }
    }
}
