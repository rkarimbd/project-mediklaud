using mediklaud.DTOs;

namespace mediklaud.Services
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync();
        Task<DoctorDto> GetDoctorByIdAsync(int id);
        Task<DoctorDto> CreateDoctorAsync(DoctorDto doctorDto);
        Task<DoctorDto> UpdateDoctorAsync(DoctorDto doctorDto);
        Task DeleteDoctorAsync(int id);
    }

}
