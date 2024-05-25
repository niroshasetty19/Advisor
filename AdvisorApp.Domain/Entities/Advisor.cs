using System.ComponentModel.DataAnnotations;

namespace AdvisorApp.Domain.Entities
{
    public class Advisor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(255, ErrorMessage = "Name cannot exceed 255 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "SIN is required.")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "SIN must be exactly 9 digits.")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "SIN must be exactly 9 digits.")]
        public string SIN { get; set; }

        [StringLength(255, ErrorMessage = "Address cannot exceed 255 characters.")]
        public string Address { get; set; }

        [RegularExpression(@"^\d{8}$", ErrorMessage = "Phone must be exactly 8 digits.")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "Phone must be exactly 8 digits.")]
        public string Phone { get; set; }

        public HealthStatus HealthStatus { get; private set; }

        private Advisor()
        {
        }

        public Advisor(string name, string sin, string address, string phone)
        {
            Name = name;
            SIN = sin;
            Address = address;
            Phone = phone;
            HealthStatus = GenerateRandomHealthStatus();
        }

        private HealthStatus GenerateRandomHealthStatus()
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 101);

            if (randomNumber <= 60)
                return HealthStatus.Green;
            else if (randomNumber <= 80)
                return HealthStatus.Yellow;
            else
                return HealthStatus.Red;
        }
    }

    public enum HealthStatus
    {
        Green,
        Yellow,
        Red
    }
}
