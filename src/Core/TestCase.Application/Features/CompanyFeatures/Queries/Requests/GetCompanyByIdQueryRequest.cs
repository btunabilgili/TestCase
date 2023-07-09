using MediatR;
using TestCase.Application.Common;
using TestCase.Application.Features.CompanyFeatures.Queries.Responses;

namespace TestCase.Application.Features.CompanyFeatures.Queries.Requests
{
    public class GetCompanyByIdQueryRequest : IRequest<Result<GetCompanyByIdQueryResponse>>
    {
        public Guid Id { get; set; }
    }
}
