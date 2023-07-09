using MediatR;
using TestCase.Application.Common;
using TestCase.Application.Features.JobFeatures.Queries.Responses;

namespace TestCase.Application.Features.JobFeatures.Queries.Requests
{
    public class GetJobByIdQueryRequest : IRequest<Result<GetJobByIdQueryResponse>>
    {
        public required Guid Id { get; set; }
    }
}
