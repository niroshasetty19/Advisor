using AdvisorApp.Application.Services;
using AdvisorApp.Domain.Entities;
using AdvisorApp.Domain.Interfaces;
using Moq;

namespace AdvisorApp.Tests.Application
{
    [TestFixture]
    public class AdvisorServiceTests
    {
        private Mock<IAdvisorRepository> _mockRepository;
        private AdvisorService _advisorService;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IAdvisorRepository>();
            _advisorService = new AdvisorService(_mockRepository.Object);
        }

        [Test]
        public async Task GetAllAsync_ReturnsAdvisorDtos()
        {
            // Arrange
            var advisors = new List<Advisor>
            {
                new Advisor("John Doe", "123456789", "123 Main St.", "12345678"),
                new Advisor("Jane Smith", "987654321", "456 Oak Ave.", "87654321")
            };
            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(advisors);

            // Act
            var result = await _advisorService.GetAllAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
        }
    }
}
