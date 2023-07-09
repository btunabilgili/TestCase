using MediatR;
using TestCase.Application.Common;
using TestCase.Application.Features.CompanyFeatures.Queries.Responses;

namespace TestCase.Application.Features.CompanyFeatures.Queries.Requests
{
    public class GetAllCompaniesQueryRequest : IRequest<Result<List<GetAllCompaniesQueryResponse>>>
    {
    }
}
