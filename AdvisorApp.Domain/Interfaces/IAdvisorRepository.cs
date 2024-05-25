using AdvisorApp.Domain.Entities;

namespace AdvisorApp.Domain.Interfaces
{
    public interface IAdvisorRepository
    {
        Task<Advisor> GetByIdAsync(int id);
        Task<IEnumerable<Advisor>> GetAllAsync();
        Task<Advisor> AddAsync(Advisor advisor);
        Task UpdateAsync(Advisor advisor);
        Task DeleteAsync(Advisor advisor);
        Task<Advisor> GetBySinAsync(string sin);

    }
}
