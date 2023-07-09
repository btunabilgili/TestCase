using TestCase.Domain.Common;
using TestCase.Domain.Enums;

namespace TestCase.Domain.Entities
{
    public class SideRights : BaseEntity
    {
        public Guid JobId { get; set; }
        public required SideRightTypes SideRight { get; set; }

        public Job? Job { get; set; }
    }
}
