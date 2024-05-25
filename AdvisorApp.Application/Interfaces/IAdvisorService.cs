using AdvisorApp.Application.DTOs;

namespace AdvisorApp.Application.Interfaces
{
    public interface IAdvisorService
    {
        Task<AdvisorDto> GetByIdAsync(int id);
        Task<IEnumerable<AdvisorDto>> GetAllAsync();
        Task<AdvisorDto> CreateAsync(CreateAdvisorDto createAdvisorDto);
        Task UpdateAsync(AdvisorDto advisorDto);
        Task DeleteAsync(int id);
    }
}
