using mediklaud.Context;
using mediklaud.DTOs;
using mediklaud.Models;
using Microsoft.EntityFrameworkCore;

namespace mediklaud.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ApplicationDbContext _context;

        public DoctorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync()
        {
            var doctors = await _context.Doctors.Select(d => new DoctorDto
            {
                DoctorId = d.DoctorId,
                Name = d.Name,
                MobileNo = d.MobileNo,
                Email = d.Email,
                Specialization = d.Specialization,
                ConsultationFee = d.ConsultationFee
            }).ToListAsync();
            return doctors;
        }

        public async Task<DoctorDto> GetDoctorByIdAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return null; // Handle case where doctor is not found
            }
            return new DoctorDto
            {
                DoctorId = doctor.DoctorId,
                Name = doctor.Name,
                MobileNo = doctor.MobileNo,
                Email = doctor.Email,
                Specialization = doctor.Specialization,
                ConsultationFee = doctor.ConsultationFee
            };
        }

        public async Task<DoctorDto> AddDoctorAsync(DoctorDto doctorDto)
        {
            var doctor = new Doctor
            {
                Name = doctorDto.Name,
                MobileNo = doctorDto.MobileNo,
                Email = doctorDto.Email,
                Specialization = doctorDto.Specialization,
                ConsultationFee = doctorDto.ConsultationFee
            };
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();

            return new DoctorDto
            {
                DoctorId = doctor.DoctorId,
                Name = doctor.Name,
                MobileNo = doctor.MobileNo,
                Email = doctor.Email,
                Specialization = doctor.Specialization,
                ConsultationFee = doctor.ConsultationFee
            };
        }

        public async Task<DoctorDto> UpdateDoctorAsync(DoctorDto doctorDto)
        {
            var existingDoctor = await _context.Doctors.FindAsync(doctorDto.DoctorId);
            if (existingDoctor == null)
            {
                return null; // Handle case where doctor is not found
            }

            existingDoctor.Name = doctorDto.Name;
            existingDoctor.MobileNo = doctorDto.MobileNo;
            existingDoctor.Email = doctorDto.Email;
            existingDoctor.Specialization = doctorDto.Specialization;
            existingDoctor.ConsultationFee = doctorDto.ConsultationFee;

            _context.Doctors.Update(existingDoctor);
            await _context.SaveChangesAsync();

            return new DoctorDto
            {
                DoctorId = existingDoctor.DoctorId,
                Name = existingDoctor.Name,
                MobileNo = existingDoctor.MobileNo,
                Email = existingDoctor.Email,
                Specialization = existingDoctor.Specialization,
                ConsultationFee = existingDoctor.ConsultationFee
            };
        }

        public async Task DeleteDoctorAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                // Handle case where doctor is not found (optional)
                return;
            }

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
        }
    }

}
