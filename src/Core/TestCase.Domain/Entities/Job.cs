using TestCase.Domain.Common;
using TestCase.Domain.Enums;

namespace TestCase.Domain.Entities
{
    public class Job : BaseEntity
    {
        public required string Position { get; set; }
        public required string JobDescription { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int QualityPoint { get; set; }
        public ICollection<SideRights>? SideRights { get; set; }
        public WorkTypes WorkType { get; set; }
        public int SalaryInformation { get; set; }
    }
}
