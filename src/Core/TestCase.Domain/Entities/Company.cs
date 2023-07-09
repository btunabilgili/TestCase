using TestCase.Domain.Common;

namespace TestCase.Domain.Entities
{
    public class Company : BaseEntity
    {
        public required string PasswordHash { get; set; }
        public required string CompanyName { get; set; }
        public required string Address { get; set; }
        public required string Phone { get; set; }
        public int RemainingJobCount { get; set; } = 2;

        public ICollection<Job>? Jobs { get; set; }
    }
}
