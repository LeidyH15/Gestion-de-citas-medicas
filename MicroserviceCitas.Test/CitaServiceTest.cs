﻿using System.Threading.Tasks;
using AutoMapper;
using MicroserviceCitas.Application.DTOs;
using MicroserviceCitas.Application.Interfaces;
using MicroserviceCitas.Application.Services;
using MicroserviceCitas.Domain.Entities;
using MicroserviceCitas.Domain.Interfaces;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace MicroserviceCitas.Test
{
    [TestFixture]
    public class CitaServiceTest
    {
        private Mock<ICitaRepository> _mockCitaRepository;
        private Mock<IPersonaService> _mockPersonaService;
        private Mock<IRabbitMQSender> _mockRabbitMQSender;
        private CitaService _citaService;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mockCitaRepository = new Mock<ICitaRepository>();
            _mockPersonaService = new Mock<IPersonaService>();
            _mockRabbitMQSender = new Mock<IRabbitMQSender>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Cita, CitaDTO>().ReverseMap();
            });
            _mapper = config.CreateMapper();

            // Usar las instancias mockeadas en lugar de las originales
            _citaService = new CitaService(_mockCitaRepository.Object, _mockPersonaService.Object, _mapper, _mockRabbitMQSender.Object);
        }

        // Prueba para GetById
        [Test]
        public async Task GetById_ShouldReturnCitaDTO()
        {
            // Arrange
            var cita = new Cita { Id = 1, IdPaciente = 123, Estado = "Pendiente" };
            _mockCitaRepository.Setup(repo => repo.GetById(1)).ReturnsAsync(cita);

            // Act
            var result = await _citaService.GetById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Pendiente", result.Estado);
        }

        [Test]
        public async Task GetById_ShouldReturnNull_WhenCitaNotFound()
        {
            // Arrange
            _mockCitaRepository.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync((Cita)null);

            // Act
            var result = await _citaService.GetById(1);

            // Assert
            Assert.IsNull(result);
        }


        // Prueba para Update
        [Test]
        public async Task Update_ShouldReturnUpdatedMessage()
        {
            // Arrange
            var citaDto = new CitaDTO { Paciente = "Stephania", Medico = "Brandon" };
            _mockCitaRepository.Setup(repo => repo.Update(It.IsAny<int>(), It.IsAny<Cita>())).ReturnsAsync("Updated");

            // Act
            var result = await _citaService.Update(1, citaDto);

            // Assert
            Assert.AreEqual("Updated", result);
            _mockCitaRepository.Verify(repo => repo.Update(1, It.Is<Cita>(c => c.Paciente == "Stephania")), Times.Once);
        }

        // Prueba para Delete
        [Test]
        public async Task Delete_ShouldReturnDeletedMessage()
        {
            // Arrange
            _mockCitaRepository.Setup(repo => repo.Delete(It.IsAny<int>())).ReturnsAsync("Deleted");

            // Act
            var result = await _citaService.Delete(1);

            // Assert
            Assert.AreEqual("Deleted", result);
            _mockCitaRepository.Verify(repo => repo.Delete(1), Times.Once);
        }
    }
}
