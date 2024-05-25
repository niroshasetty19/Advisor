using AdvisorApp.Domain.Entities;
using AdvisorApp.Infrastructure.Persistence;
using AdvisorApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AdvisorApp.Tests.Infrastructure
{
    [TestFixture]
    public class AdvisorRepositoryTests
    {
        private AdvisorDbContext _dbContext;
        private CachedAdvisorRepository _advisorRepository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AdvisorDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _dbContext = new AdvisorDbContext(options);
            _advisorRepository = new CachedAdvisorRepository(_dbContext);
        }

        [Test]
        public async Task AddAsync_AddsAdvisorToDatabase()
        {
            // Arrange
            var advisor = new Advisor("John Doe", "123456789", "123 Main St.", "12345678");

            // Act
            var result = await _advisorRepository.AddAsync(advisor);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(advisor.Name, Is.EqualTo(result.Name), "Name does not match");
            Assert.That(advisor.SIN, Is.EqualTo(result.SIN), "SIN does not match");
            Assert.That(advisor.Address, Is.EqualTo(result.Address), "Address does not match");
            Assert.That(advisor.Phone, Is.EqualTo(result.Phone), "Phone does not match");
        }
        [TearDown]
        public void TearDown()
        {
            _dbContext.Dispose();
        }
    }
}
