using AutoMapper;
using MediatR;
using TestCase.Application.Common;
using TestCase.Application.Features.CompanyFeatures.Queries.Requests;
using TestCase.Application.Features.CompanyFeatures.Queries.Responses;
using TestCase.Application.Interfaces;

namespace TestCase.Application.Features.CompanyFeatures.Queries.Handlers
{
    public class GetAllCompaniesQueryHandler : IRequestHandler<GetAllCompaniesQueryRequest, Result<List<GetAllCompaniesQueryResponse>>>
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        public GetAllCompaniesQueryHandler(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<List<GetAllCompaniesQueryResponse>>> Handle(GetAllCompaniesQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _companyService.GetCompaniesAsync();

            if (!result.IsSuccess)
                return Result<List<GetAllCompaniesQueryResponse>>.Failure(result.ErrorMessage!, result.StatusCode);

            return _mapper.Map<List<GetAllCompaniesQueryResponse>>(result.Data).ToResult();
        }
    }
}
