using AdvisorApp.Domain.Entities;
using AdvisorApp.Domain.Interfaces;
using AdvisorApp.Infrastructure.Caching;
using AdvisorApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdvisorApp.Infrastructure.Repositories
{
    public class CachedAdvisorRepository : IAdvisorRepository
    {
        private readonly AdvisorDbContext _dbContext;
        private readonly MRUCache<int, Advisor> _cache;

        public CachedAdvisorRepository(AdvisorDbContext dbContext)
        {
            _dbContext = dbContext;
            _cache = new MRUCache<int, Advisor>();
        }

        public async Task<Advisor> GetByIdAsync(int id)
        {
            var advisor = _cache.Get(id);
            if (advisor == null)
            {
                advisor = await _dbContext.Advisors.FindAsync(id);
                if (advisor != null)
                {
                    _cache.Put(id, advisor);
                }
            }

            return advisor;
        }

        public async Task<IEnumerable<Advisor>> GetAllAsync()
        {
            return await _dbContext.Advisors.ToListAsync();
        }

        public async Task<Advisor> AddAsync(Advisor advisor)
        {
            var entry = await _dbContext.Advisors.AddAsync(advisor);
            await _dbContext.SaveChangesAsync();
            _cache.Put(entry.Entity.Id, entry.Entity);
            return entry.Entity;
        }

        public async Task UpdateAsync(Advisor advisor)
        {
            _dbContext.Advisors.Update(advisor);
            await _dbContext.SaveChangesAsync();
            _cache.Put(advisor.Id, advisor);
        }

        public async Task DeleteAsync(Advisor advisor)
        {
            _dbContext.Advisors.Remove(advisor);
            await _dbContext.SaveChangesAsync();
            _cache.Delete(advisor.Id);
        }
        public async Task<Advisor> GetBySinAsync(string sin)
        {
            return await _dbContext.Advisors.FirstOrDefaultAsync(a => a.SIN == sin);
        }
    }
}
