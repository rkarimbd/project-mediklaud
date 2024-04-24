using mediklaud.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace mediklaud.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //Database.EnsureCreated(); // Ensure the database is created

            // Seed initial data
            //SeedData();


            var dbCreater = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            if (dbCreater != null)
            {
                // Create Database 
                if (!dbCreater.CanConnect())
                {
                    dbCreater.Create();
                }

                // Create Tables
                if (!dbCreater.HasTables())
                {
                    dbCreater.CreateTables();
                }
            }
        }

        // DbSet properties for all models
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Billing> Billings { get; set; }
        public DbSet<HospitalService> HospitalServices { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entity relationships
           ConfigureEntityRelationships(modelBuilder);
        }

        private void ConfigureEntityRelationships(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Billing>()
                .HasOne(b => b.Appointment)
                .WithOne(a => a.Billing)
                .HasForeignKey<Billing>(b => b.AppointmentId)
                .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Billing>()
                .HasOne(b => b.Service)
                .WithMany(s => s.Billings)
                .HasForeignKey(b => b.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UserInfo>()
                .HasOne(ui => ui.Role)
                .WithMany()
                .HasForeignKey(ui => ui.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

         // Use DeleteBehavior.Restrict or other appropriate behavior


    }

    //private void SeedData()
    //{
    //    // Seed data for Patients
    //    Patients.AddRange(
    //        new Patient
    //        {
    //            PatientId = 1,
    //            FirstName = "Rezaul",
    //            LastName = "Khan",
    //            MobileNo = "641-233-9385",
    //            Address = "123 Main St, City",
    //            Email = "rezaul@mediklaud.com",
    //            DateOfBirth = new DateTime(1983, 4, 1)

    //        },
    //        new Patient
    //        {
    //            PatientId = 2,
    //            FirstName = "Tania",
    //            LastName = "Afroj",
    //            MobileNo = "641-233-9385",
    //            Address = "456 Elm St, Town",
    //            Email = "tania@mediklaud.com",
    //            DateOfBirth = new DateTime(1987, 5, 10)

    //        }
    //    );

    //    // Seed data for Doctors
    //    Doctors.AddRange(
    //        new Doctor
    //        {
    //            DoctorId = 1,
    //            Name = "Dr. John Doe",
    //            MobileNo = "123-456-7890",
    //            Email = "johndoe@mediklaud.com",
    //            Specialization = "Cardiology",
    //            ConsultationFee = 100.00m

    //        },
    //        new Doctor
    //        {
    //            DoctorId = 2,
    //            Name = "Dr. Jane Smith",
    //            MobileNo = "987-654-3210",
    //            Email = "janesmith@mediklaud.com",
    //            Specialization = "Pediatrics",
    //            ConsultationFee = 80.00m
    //        }

    //    );

    //    // Seed data for Appointments
    //    Appointments.AddRange(
    //        new Appointment
    //        {
    //            AppointmentId = 1,
    //            DoctorId = 1,
    //            PatientId = 1,
    //            AppointmentDateTime = new DateTime(2024, 4, 30, 10, 0, 0),
    //            ConsultationFee = 100,
    //            Status = true,
    //            Comments = "Regular checkup",

    //        },
    //        new Appointment
    //        {
    //            AppointmentId = 2,
    //            DoctorId = 2,
    //            PatientId = 2,
    //            AppointmentDateTime = new DateTime(2024, 5, 15, 14, 30, 0),
    //            ConsultationFee = 80.00,
    //            Status = true,
    //            Comments = "Follow-up visit",

    //        }
    //    );

    //    // Seed data for Billings
    //    Billings.AddRange(
    //        new Billing
    //        {
    //            BillNo = 1,
    //            BillDate = new DateTime(2024, 5, 1),
    //            AppointmentId = 1,
    //            ServiceId = 1,
    //            BillAmount = 100.00m,
    //            IsPaid = false,
    //            Comments = "Pending payment"

    //        },
    //        new Billing
    //        {
    //            BillNo = 2,
    //            BillDate = new DateTime(2024, 5, 16),
    //            AppointmentId = 2,
    //            ServiceId = 2,
    //            BillAmount = 80.00m,
    //            IsPaid = true,
    //            Comments = "Paid in full",

    //        }
    //    );

    //    // Seed data for HospitalServices
    //    HospitalServices.AddRange(
    //        new HospitalService
    //        {
    //            ServiceId = 1,
    //            ServiceName = "Cardiology Consultation",
    //            HospitalId = 1,
    //            Status = true

    //        },
    //        new HospitalService
    //        {
    //            ServiceId = 2,
    //            ServiceName = "Pediatrics Consultation",
    //            HospitalId = 1,
    //            Status = true

    //        }
    //    );

    //    // Seed data for UserInfos
    //    UserInfos.AddRange(
    //        new UserInfo
    //        {
    //            UserId = 1,
    //            DisplayName = "Rezaul Khan",
    //            UserName = "rezaul",
    //            LoginId = "rezaul",
    //            Password = "password1",
    //            RoleId = 1
    //        },
    //        new UserInfo
    //        {
    //            UserId = 2,
    //            DisplayName = "Tania Afroj",
    //            UserName = "tania",
    //            LoginId = "tania",
    //            Password = "password2",
    //            RoleId = 1
    //        },
    //        new UserInfo
    //        {
    //            UserId = 3,
    //            DisplayName = "Dr. John Doe",
    //            UserName = "johndoe",
    //            LoginId = "johndoe",
    //            Password = "password3",
    //            RoleId = 2
    //        },
    //        new UserInfo
    //        {
    //            UserId = 4,
    //            DisplayName = "Dr. Jane Smith",
    //            UserName = "janesmith",
    //            LoginId = "janesmith",
    //            Password = "password4",
    //            RoleId = 2
    //        },
    //        new UserInfo
    //        {
    //            UserId = 5,
    //            DisplayName = "Appointment User 1",
    //            UserName = "appointmentuser1",
    //            LoginId = "appoint1",
    //            Password = "password5",
    //            RoleId = 3
    //        },
    //        new UserInfo
    //        {
    //            UserId = 6,
    //            DisplayName = "Appointment User 2",
    //            UserName = "appointmentuser2",
    //            LoginId = "appoint2",
    //            Password = "password6",
    //            RoleId = 3
    //        },
    //        new UserInfo
    //        {
    //            UserId = 7,
    //            DisplayName = "Billing User 1",
    //            UserName = "billinguser1",
    //            LoginId = "bill1",
    //            Password = "password7",
    //            RoleId = 4
    //        },
    //        new UserInfo
    //        {
    //            UserId = 8,
    //            DisplayName = "Billing User 2",
    //            UserName = "billinguser2",
    //            LoginId = "bill2",
    //            Password = "password8",
    //            RoleId = 4
    //        },
    //        new UserInfo
    //        {
    //            UserId = 9,
    //            DisplayName = "Hospital Service User 1",
    //            UserName = "hospitalserviceuser1",
    //            LoginId = "hosp1",
    //            Password = "password9",
    //            RoleId = 5
    //        },
    //        new UserInfo
    //        {
    //            UserId = 10,
    //            DisplayName = "Hospital Service User 2",
    //            UserName = "hospitalserviceuser2",
    //            LoginId = "hosp2",
    //            Password = "password10",
    //            RoleId = 5
    //        }
    //    );

    //    SaveChanges(); // Save changes to the database after seeding
    //}
}
}
