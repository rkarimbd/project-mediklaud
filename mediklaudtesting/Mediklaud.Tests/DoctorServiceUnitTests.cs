using mediklaud.Context;
using mediklaud.DTOs;
using mediklaud.Models;
using mediklaud.Repositories;
using mediklaud.Services.Imp;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Mediklaud.Tests
{
    public class DoctorServiceTests
    {
        private readonly Mock<IDoctorRepository> _mockRepository;
     
        public DoctorServiceTests()
        {
            _mockRepository = new Mock<IDoctorRepository>();
            


        }


        [Fact]
        public async Task GetAllDoctorsAsync_ShouldReturnListOfDoctors()
        {
            // Arrange
                         var expectedDoctors = new List<DoctorDto>() // Use DoctorDto for expected results
                  {
                    new DoctorDto { Name = "Test Doctor", MobileNo = "1234567890", Specialization = "Cardiology", ConsultationFee = 100.00m }
                  };


            _mockRepository.Setup(repo => repo.GetAllDoctorsAsync()).ReturnsAsync(expectedDoctors);
            var doctorService = new DoctorService(_mockRepository.Object);

            // Act
            var result = await doctorService.GetAllDoctorsAsync();

            // Assert
            Assert.Equal(expectedDoctors, result);
        }


        [Fact]
        public async Task CreateDoctorAsync_ShouldCallRepositoryAndReturnDoctor()
        {
            // Arrange
            var doctorDto = new DoctorDto { Name = "Test Doctor", MobileNo = "1234567890", Specialization = "Cardiology", ConsultationFee = 100.00m };
            _mockRepository.Setup(x => x.AddDoctorAsync(doctorDto)).Returns(Task.FromResult(doctorDto));
            var doctorService = new DoctorService(_mockRepository.Object);

            // Act
            var result = await doctorService.CreateDoctorAsync(doctorDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(doctorDto.Name, result.Name);
            Assert.Equal(doctorDto.MobileNo, result.MobileNo);
            Assert.Equal(doctorDto.Specialization, result.Specialization);
            Assert.Equal(doctorDto.ConsultationFee, result.ConsultationFee);
            _mockRepository.Verify(x => x.AddDoctorAsync(doctorDto), Times.Once);
        }






    }


}
