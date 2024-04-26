using System;
using System.Linq;
using System.Threading.Tasks;
using mediklaud.Context;
using mediklaud.Models;

using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Mediklaud.Tests
    {
        public class DbIntegrationTest : IDisposable
        {
            private readonly ApplicationDbContext _dbContext;

            public DbIntegrationTest()
            {
                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlServer("Data Source=CTOPC;Initial Catalog=mediklauddb;User ID=sa;Password=123456 ;Trusted_Connection=True;integrated security=true; trustservercertificate=true")
                    .Options;

                _dbContext = new ApplicationDbContext(options);
            }

            [Fact]
            public async Task Can_Add_Doctor_To_Database()
            {
                // Arrange
                var doctor = new Doctor
                {
                    Name = "Test Doctor",
                    MobileNo = "1234567890",
                    Specialization = "Cardiology",
                    ConsultationFee = 100.00m
                };

                // Act
                _dbContext.Doctors.Add(doctor);
                await _dbContext.SaveChangesAsync();

                // Assert
                var savedDoctor = await _dbContext.Doctors.FirstOrDefaultAsync(d => d.DoctorId == doctor.DoctorId);
                Assert.NotNull(savedDoctor);
                Assert.Equal(doctor.Name, savedDoctor.Name);
                Assert.Equal(doctor.MobileNo, savedDoctor.MobileNo);
                Assert.Equal(doctor.Specialization, savedDoctor.Specialization);
                Assert.Equal(doctor.ConsultationFee, savedDoctor.ConsultationFee);
            }

            public void Dispose()
            {
                _dbContext.Dispose();
            }
        }
    }
