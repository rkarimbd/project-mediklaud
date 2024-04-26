using mediklaud.DTOs;

namespace mediklaud.Repositories
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync();
        Task<DoctorDto> GetDoctorByIdAsync(int id);
        Task<DoctorDto> AddDoctorAsync(DoctorDto doctorDto);
        Task<DoctorDto> UpdateDoctorAsync(DoctorDto doctorDto);
        Task DeleteDoctorAsync(int id);
    }

}
