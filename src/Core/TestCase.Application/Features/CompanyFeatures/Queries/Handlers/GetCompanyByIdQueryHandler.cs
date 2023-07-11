using AutoMapper;
using FluentValidation;
using MediatR;
using System.Net;
using TestCase.Application.Common;
using TestCase.Application.Features.CompanyFeatures.Queries.Requests;
using TestCase.Application.Features.CompanyFeatures.Queries.Responses;
using TestCase.Application.Interfaces;
using TestCase.Domain.Entities;

namespace TestCase.Application.Features.CompanyFeatures.Queries.Handlers
{
    public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQueryRequest, Result<GetCompanyByIdQueryResponse>>
    {
        private readonly IBaseRepository<Company> _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<GetCompanyByIdQueryRequest> _validator;
        public GetCompanyByIdQueryHandler(IBaseRepository<Company> repository, 
            IMapper mapper,
            IValidator<GetCompanyByIdQueryRequest> validator)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<Result<GetCompanyByIdQueryResponse>> Handle(GetCompanyByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return Result<GetCompanyByIdQueryResponse>.Failure(string.Join(",", validationResult.Errors), (int)HttpStatusCode.BadRequest);

            var company = await _repository.GetByIdAsync(request.Id, x => x.Jobs!);

            if (company is null)
                return Result<GetCompanyByIdQueryResponse>.Failure("Company not found", (int)HttpStatusCode.NotFound);

            return _mapper.Map<GetCompanyByIdQueryResponse>(company).ToResult();
        }
    }
}
