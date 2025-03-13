using System.Threading.Tasks;
using AutoMapper;
using MicroserviceRecetas.Application.DTOs;
using MicroserviceRecetas.Application.Interfaces;
using MicroserviceRecetas.Application.Services;
using MicroserviceRecetas.Domain.Entities;
using MicroserviceRecetas.Domain.Interfaces;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace MicroserviceRecetas.Test
{
    [TestFixture]
    public class RecetaServiceTest
    {
        private Mock<IRecetaRepository> _mockRecetaRepository;
        private RecetaService _recetaService;
        private IMapper _mapper;
        private IPersonaService _personaService;
        private ICitaService _citaService;

        [SetUp]
        public void Setup()
        {
            _mockRecetaRepository = new Mock<IRecetaRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Receta, RecetaDTO>().ReverseMap();
                cfg.CreateMap<Medicamento, MedicamentoDTO>().ReverseMap();
            });
            _mapper = config.CreateMapper();

            _recetaService = new RecetaService(_mockRecetaRepository.Object, _personaService, _citaService, _mapper);
        }

        // Prueba para GetById
        [Test]
        public async Task GetById_ShouldReturnRecetaDTO()
        {
            // Arrange
            var receta = new Receta { Id = 1, Paciente = "Stephania Hernandez", Medico = "Brandon Muñoz" };
            _mockRecetaRepository.Setup(repo => repo.GetById(1)).ReturnsAsync(receta);

            // Act
            var result = await _recetaService.GetById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Stephania Hernandez", result.Paciente);
        }

        [Test]
        public async Task GetById_ShouldReturnNull_WhenRecetaNotFound()
        {
            // Arrange
            _mockRecetaRepository.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync((Receta)null);

            // Act
            var result = await _recetaService.GetById(1);

            // Assert
            Assert.IsNull(result);
        }


        //Update
        [Test]
        public async Task Update_ShouldReturnUpdatedMessage()
        {
            // Arrange
            var recetaDto = new RecetaDTO { Paciente = "Daniela Hernandez", Medico = "Maria Varon", Fecha_Emision = System.DateTime.Now };
            _mockRecetaRepository.Setup(repo => repo.Update(It.IsAny<int>(), It.IsAny<Receta>())).ReturnsAsync("Updated");

            // Act
            var result = await _recetaService.Update(1, recetaDto);

            // Assert
            Assert.AreEqual("Updated", result);
            _mockRecetaRepository.Verify(repo => repo.Update(1, It.Is<Receta>(r => r.Paciente == "Daniela Hernandez")), Times.Once);
        }

        // Delete
        [Test]
        public async Task Delete_ShouldReturnDeletedMessage()
        {
            // Arrange
            _mockRecetaRepository.Setup(repo => repo.Delete(It.IsAny<int>())).ReturnsAsync("Deleted");

            // Act
            var result = await _recetaService.Delete(1);

            // Assert
            Assert.AreEqual("Deleted", result);
            _mockRecetaRepository.Verify(repo => repo.Delete(1), Times.Once);
        }
    }
}
