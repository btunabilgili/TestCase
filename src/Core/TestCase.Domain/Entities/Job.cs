using TestCase.Domain.Common;
using TestCase.Domain.Enums;

namespace TestCase.Domain.Entities
{
    public class Job : BaseEntity
    {
        public required string Position { get; set; }
        public required string JobDescription { get; set; }
        public required int ListingDurationInDays { get; set; }
        public int QualityPoint { get; set; }
        public ICollection<SideRights>? SideRights { get; set; }
        public WorkTypes? WorkType { get; set; }
        public int? SalaryInformation { get; set; }

        public required Guid CompanyId { get; set; }
        public required Company Company { get; set; }
    }
}
