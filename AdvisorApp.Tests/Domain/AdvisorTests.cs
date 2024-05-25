using AdvisorApp.Domain.Entities;

namespace AdvisorApp.Tests.Domain
{
    [TestFixture]
    public class AdvisorTests
    {
        [Test]
        public void Advisor_Constructor_SetsHealthStatusCorrectly()
        {
            // Arrange
            var name = "John Doe";
            var sin = "123456789";
            var address = "123 Main St.";
            var phone = "12345678";

            // Act
            var advisor = new Advisor(name, sin, address, phone);

            // Assert
            Assert.That(advisor.HealthStatus == HealthStatus.Green || advisor.HealthStatus == HealthStatus.Yellow || advisor.HealthStatus == HealthStatus.Red);
        }
    }
}
