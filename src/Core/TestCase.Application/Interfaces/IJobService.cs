using TestCase.Domain.Entities;

namespace TestCase.Application.Interfaces
{
    public interface IJobService
    {
        int CalculateJobQualityPoint(Job job);
    }
}
