using AdvisorApp.Application.DTOs;
using AdvisorApp.Application.Interfaces;
using AdvisorApp.Domain.Entities;
using AdvisorApp.Domain.Interfaces;

namespace AdvisorApp.Application.Services
{
    public class AdvisorService : IAdvisorService
    {
        private readonly IAdvisorRepository _advisorRepository;

        public AdvisorService(IAdvisorRepository advisorRepository)
        {
            _advisorRepository = advisorRepository;
        }

        public async Task<AdvisorDto> GetByIdAsync(int id)
        {
            try
            {
                var advisor = await _advisorRepository.GetByIdAsync(id);
                return MapToAdvisorDto(advisor);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting advisor by ID: {ex.Message}");
            }
        }

        public async Task<IEnumerable<AdvisorDto>> GetAllAsync()
        {
            try
            {
                var advisors = await _advisorRepository.GetAllAsync();
                return MapToAdvisorDtos(advisors);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting all advisors: {ex.Message}");
            }
        }

        public async Task<AdvisorDto> CreateAsync(CreateAdvisorDto createAdvisorDto)
        {
            try
            {
                var existingAdvisor = await _advisorRepository.GetBySinAsync(createAdvisorDto.SIN);
                if (existingAdvisor != null)
                {
                    throw new Exception($"An advisor with SIN '{createAdvisorDto.SIN}' already exists.");
                }

                var advisor = new Advisor(createAdvisorDto.Name, createAdvisorDto.SIN, createAdvisorDto.Address, createAdvisorDto.Phone);
                var createdAdvisor = await _advisorRepository.AddAsync(advisor);
                return MapToAdvisorDto(createdAdvisor);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating advisor: {ex.Message}");
            }
        }

        public async Task UpdateAsync(AdvisorDto advisorDto)
        {
            try
            {
                var advisor = await _advisorRepository.GetByIdAsync(advisorDto.Id);
                if (advisor == null)
                {
                    throw new Exception($"Advisor with ID {advisorDto.Id} not found.");
                }

                var existingAdvisor = await _advisorRepository.GetBySinAsync(advisorDto.SIN);
                if (existingAdvisor != null && existingAdvisor.Id != advisorDto.Id)
                {
                    throw new Exception($"An advisor with SIN '{advisorDto.SIN}' already exists.");
                }

                advisor.Name = advisorDto.Name;
                advisor.SIN = advisorDto.SIN;
                advisor.Address = advisorDto.Address;
                advisor.Phone = advisorDto.Phone;
                await _advisorRepository.UpdateAsync(advisor);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating advisor: {ex.Message}");
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var advisor = await _advisorRepository.GetByIdAsync(id);
                if (advisor == null)
                {
                    throw new Exception($"Advisor with ID {id} not found.");
                }

                await _advisorRepository.DeleteAsync(advisor);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting advisor: {ex.Message}");
            }
        }

        private AdvisorDto MapToAdvisorDto(Advisor advisor)
        {
            return new AdvisorDto
            {
                Id = advisor.Id,
                Name = advisor.Name,
                SIN = advisor.SIN,
                Address = advisor.Address,
                Phone = advisor.Phone,
                HealthStatus = advisor.HealthStatus.ToString()
            };
        }

        private IEnumerable<AdvisorDto> MapToAdvisorDtos(IEnumerable<Advisor> advisors)
        {
            foreach (var advisor in advisors)
            {
                yield return MapToAdvisorDto(advisor);
            }
        }
    }
}
