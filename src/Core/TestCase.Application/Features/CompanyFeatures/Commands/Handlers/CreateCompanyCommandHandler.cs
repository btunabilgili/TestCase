using AutoMapper;
using FluentValidation;
using MediatR;
using System.Net;
using TestCase.Application.Common;
using TestCase.Application.Features.CompanyFeatures.Commands.Requests;
using TestCase.Application.Features.CompanyFeatures.Commands.Responses;
using TestCase.Application.Interfaces;
using TestCase.Domain.Entities;

namespace TestCase.Application.Features.CompanyFeatures.Commands.Handlers
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCompanyCommandRequest, Result<CreateCompanyCommandResponse>>
    {
        private readonly IBaseRepository<Company> _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateCompanyCommandRequest> _validator;
        public CreateCustomerCommandHandler(IBaseRepository<Company> repository, 
            IMapper mapper,
            IValidator<CreateCompanyCommandRequest> validator)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<Result<CreateCompanyCommandResponse>> Handle(CreateCompanyCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return Result<CreateCompanyCommandResponse>.Failure(string.Join(",", validationResult.Errors), (int)HttpStatusCode.BadRequest);

            var company = _mapper.Map<Company>(request);

            await _repository.AddAsync(company);

            return _mapper.Map<CreateCompanyCommandResponse>(company).ToResult();
        }
    }
}
