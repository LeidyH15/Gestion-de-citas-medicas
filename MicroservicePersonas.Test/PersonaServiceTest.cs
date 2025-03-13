using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MicroservicePersonas.Application.DTOs;
using MicroservicePersonas.Application.Mappings;
using MicroservicePersonas.Application.Services;
using MicroservicePersonas.Domain.Entities;
using MicroservicePersonas.Domain.Interfaces;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace MicroservicePersonas.Test
{
    [TestFixture]
    public class PersonaServiceTest
    {
        private Mock<IPersonaRepository> _mockPersonaRepository;
        private PersonaService _personaService;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mockPersonaRepository = new Mock<IPersonaRepository>();

            var config = new MapperConfiguration(cfg => cfg.AddProfile(new PersonaProfile()));
            _mapper = config.CreateMapper();

            _personaService = new PersonaService(_mockPersonaRepository.Object, _mapper);
        }

        // Prueba para el método GetAll
        [Test]
        public async Task GetAll_ShouldReturnListOfPersonaDTOs()
        {
            // Arrange
            var personas = new List<Persona>
            {
                new Persona { Id = 1, Nombre = "Daniela", IdTipoPersona = 1 },
                new Persona { Id = 2, Nombre = "Maria", IdTipoPersona = 2 }
            };

            _mockPersonaRepository.Setup(repo => repo.GetAll()).ReturnsAsync(personas);

            // Act
            var result = await _personaService.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Daniela", result[0].Nombre);
        }

        // Prueba para GetById
        [Test]
        public async Task GetById_ShouldReturnPersonaDTO()
        {
            // Arrange
            var persona = new Persona { Id = 1, Nombre = "Daniela", IdTipoPersona = 1 };
            _mockPersonaRepository.Setup(repo => repo.GetById(1)).ReturnsAsync(persona);

            // Act
            var result = await _personaService.GetById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Daniela", result.Nombre);
        }

        [Test]
        public async Task GetById_ShouldReturnNull_WhenPersonaNotFound()
        {
            // Arrange
            _mockPersonaRepository.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync((Persona)null);

            // Act
            var result = await _personaService.GetById(1);

            // Assert
            Assert.IsNull(result);
        }

        // Prueba para Create
        [Test]
        public async Task Create_ShouldCallRepositoryWithMappedPersona()
        {
            // Arrange
            var personaDto = new PersonaDTO { Nombre = "Lina", Fecha_Nacimiento = System.DateTime.Now };
            //var persona = new Persona { Id = 0, Nombre = "Pedro", IdTipoPersona = 1 };

            _mockPersonaRepository.Setup(repo => repo.Create(It.IsAny<Persona>())).ReturnsAsync("Success");

            // Act
            var result = await _personaService.Create(1, personaDto);

            // Assert
            Assert.AreEqual("Success", result);
            _mockPersonaRepository.Verify(repo => repo.Create(It.Is<Persona>(p => p.Nombre == "Lina" && p.IdTipoPersona == 1)), Times.Once);
        }

        // Prueba para Update
        [Test]
        public async Task Update_ShouldCallRepositoryWithUpdatedPersona()
        {
            // Arrange
            var personaDto = new PersonaDTO { Nombre = "Luis", Fecha_Nacimiento = System.DateTime.Now };
            _mockPersonaRepository.Setup(repo => repo.Update(It.IsAny<int>(), It.IsAny<Persona>())).ReturnsAsync("Updated");

            // Act
            var result = await _personaService.Update(1, personaDto);

            // Assert
            Assert.AreEqual("Updated", result);
            _mockPersonaRepository.Verify(repo => repo.Update(1, It.Is<Persona>(p => p.Nombre == "Luis")), Times.Once);
        }

        // Prueba para Delete
        [Test]
        public async Task Delete_ShouldCallRepositoryDelete()
        {
            // Arrange
            _mockPersonaRepository.Setup(repo => repo.Delete(It.IsAny<int>())).ReturnsAsync("Deleted");

            // Act
            var result = await _personaService.Delete(1);

            // Assert
            Assert.AreEqual("Deleted", result);
            _mockPersonaRepository.Verify(repo => repo.Delete(1), Times.Once);
        }
    }
}
