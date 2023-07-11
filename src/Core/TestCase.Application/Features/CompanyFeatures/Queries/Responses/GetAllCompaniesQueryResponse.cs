using TestCase.Application.Features.JobFeatures.Commands.Responses;

namespace TestCase.Application.Features.CompanyFeatures.Queries.Responses
{
    public class GetAllCompaniesQueryResponse
    {
        public Guid Id { get; set; }
        public required string CompanyName { get; set; }
        public required string Address { get; set; }
        public required string Phone { get; set; }
        public required int MyProperty { get; set; }
        public List<JobCreateCommandResponse>? Jobs { get; set; }
    }
}
