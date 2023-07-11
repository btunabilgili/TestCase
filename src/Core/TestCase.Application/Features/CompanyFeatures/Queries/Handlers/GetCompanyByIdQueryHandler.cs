using AutoMapper;
using FluentValidation;
using MediatR;
using System.Net;
using TestCase.Application.Common;
using TestCase.Application.Features.CompanyFeatures.Queries.Requests;
using TestCase.Application.Features.CompanyFeatures.Queries.Responses;
using TestCase.Application.Interfaces;

namespace TestCase.Application.Features.CompanyFeatures.Queries.Handlers
{
    public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQueryRequest, Result<GetCompanyByIdQueryResponse>>
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        private readonly IValidator<GetCompanyByIdQueryRequest> _validator;
        public GetCompanyByIdQueryHandler(ICompanyService companyService, 
            IMapper mapper,
            IValidator<GetCompanyByIdQueryRequest> validator)
        {
            _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<Result<GetCompanyByIdQueryResponse>> Handle(GetCompanyByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return Result<GetCompanyByIdQueryResponse>.Failure(string.Join(",", validationResult.Errors), (int)HttpStatusCode.BadRequest);

            var result = await _companyService.GetCompanyAsync(x => x.Id == request.Id);

            if (!result.IsSuccess)
                return Result<GetCompanyByIdQueryResponse>.Failure(result.ErrorMessage!, result.StatusCode);

            return _mapper.Map<GetCompanyByIdQueryResponse>(result.Data).ToResult();
        }
    }
}
