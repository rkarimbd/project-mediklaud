using mediklaud.DTOs;
using mediklaud.Repositories;

namespace mediklaud.Services.Imp
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync()
        {
            var doctors = await _doctorRepository.GetAllDoctorsAsync();
            return doctors;
        }

        public async Task<DoctorDto> GetDoctorByIdAsync(int id)
        {
            var doctor = await _doctorRepository.GetDoctorByIdAsync(id);
            return doctor;
        }

        public async Task<DoctorDto> CreateDoctorAsync(DoctorDto doctorDto)
        {
            var doctor = await _doctorRepository.AddDoctorAsync(doctorDto);
            return doctor;
        }

        public async Task<DoctorDto> UpdateDoctorAsync(DoctorDto doctorDto)
        {
            var doctor = await _doctorRepository.UpdateDoctorAsync(doctorDto);
            return doctor;
        }

        public async Task DeleteDoctorAsync(int id)
        {
            await _doctorRepository.DeleteDoctorAsync(id);
        }
    }

}
