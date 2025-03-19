using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using MicroserviceRecetas.Application.Commands;
using MicroserviceRecetas.Application.DTOs;
using MicroserviceRecetas.Application.Queries;
using Moq;
using NUnit.Framework;

namespace MicroserviceCitas.Test
{
    [TestFixture]
    public class RecetaServiceTest
    {
        private Mock<IMediator> _mockMediator;
        private ClaimsPrincipal _user;

        [SetUp]
        public void Setup()        {
            
            _mockMediator = new Mock<IMediator>();
            _user = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim("sub", "12345") }));
        }

        [Test]
        public async Task GetById_ShouldReturnRecetaDTO()
        {
            // Arrange
            var recetaDto = new RecetaDTO
            {
                IdCita = 1,
                Paciente = "Stephania",
                Descriptor = "Cada 8 horas",
                Medicamentos = new List<MedicamentoDTO>
                {
                    new MedicamentoDTO { NombreMedicamento = "Paracetamol", Dosis = "500mg", Frecuencia = "Cada 8 horas" }
                }
            };

            _mockMediator.Setup(m => m.Send(It.IsAny<GetRecetaByIdQuery>(), default))
                         .ReturnsAsync(recetaDto);

            // Act
            var result = await _mockMediator.Object.Send(new GetRecetaByIdQuery(1));

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Medicamentos);
            Assert.IsTrue(result.Medicamentos.Any());
            Assert.AreEqual("Paracetamol", result.Medicamentos.First().NombreMedicamento);
        }

        [Test]
        public async Task GetById_ShouldReturnNull_WhenRecetaNotFound()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<GetRecetaByIdQuery>(), default))
                         .ReturnsAsync((RecetaDTO)null);

            // Act
            var result = await _mockMediator.Object.Send(new GetRecetaByIdQuery(1));

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task Create_ShouldReturnSuccessMessage()
        {
            // Arrange
            var recetaDto = new RecetaDTO { Descriptor = "Ibuprofeno", Paciente = "Daniela" };
            _mockMediator.Setup(m => m.Send(It.IsAny<CreateRecetaCommand>(), default))
                         .ReturnsAsync("Created");

            // Act
            var result = await _mockMediator.Object.Send(new CreateRecetaCommand(recetaDto));

            // Assert
            Assert.AreEqual("Created", result);
        }

        [Test]
        public async Task Update_ShouldReturnUpdatedMessage()
        {
            // Arrange
            var recetaDto = new RecetaDTO { Descriptor = "Amoxicilina", Paciente = "Maria" };
            _mockMediator.Setup(m => m.Send(It.IsAny<UpdateRecetaCommand>(), default))
                         .ReturnsAsync("Updated");

            // Act
            var result = await _mockMediator.Object.Send(new UpdateRecetaCommand(1, recetaDto));

            // Assert
            Assert.AreEqual("Updated", result);
        }

        [Test]
        public async Task Delete_ShouldReturnDeletedMessage()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<DeleteRecetaCommand>(), default))
                         .ReturnsAsync("Deleted");

            // Act
            var result = await _mockMediator.Object.Send(new DeleteRecetaCommand(1));

            // Assert
            Assert.AreEqual("Deleted", result);
        }
    }
}
