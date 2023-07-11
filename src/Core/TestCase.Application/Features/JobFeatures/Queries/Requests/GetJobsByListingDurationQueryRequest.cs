using MediatR;
using TestCase.Application.Common;
using TestCase.Application.Features.JobFeatures.Queries.Responses;

namespace TestCase.Application.Features.JobFeatures.Queries.Requests
{
    public class GetJobsByListingDurationQueryRequest : IRequest<Result<GetJobsByListingDurationQueryResponse>>
    {
        public required int ListingDurationInDays { get; set; }
    }
}
