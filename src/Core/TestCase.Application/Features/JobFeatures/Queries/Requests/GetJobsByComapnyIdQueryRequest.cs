using MediatR;
using TestCase.Application.Common;
using TestCase.Application.Features.JobFeatures.Queries.Responses;

namespace TestCase.Application.Features.JobFeatures.Queries.Requests
{
    public class GetJobsByComapnyIdQueryRequest : IRequest<Result<List<GetJobsByCompanyIdQueryResponse>>>
    {
        public required Guid ComapnyId { get; set; }
    }
}
