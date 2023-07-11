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
    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommandRequest, Result<UpdateCompanyCommandResponse>>
    {
        private readonly IBaseRepository<Company> _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateCompanyCommandRequest> _validator;
        public UpdateCompanyCommandHandler(IBaseRepository<Company> repository, 
            IMapper mapper,
            IValidator<UpdateCompanyCommandRequest> validator)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<Result<UpdateCompanyCommandResponse>> Handle(UpdateCompanyCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return Result<UpdateCompanyCommandResponse>.Failure(string.Join(",", validationResult.Errors), (int)HttpStatusCode.BadRequest);

            var company = _mapper.Map<Company>(request);

            _repository.Update(company);

            return _mapper.Map<UpdateCompanyCommandResponse>(company).ToResult();
        }
    }
}
