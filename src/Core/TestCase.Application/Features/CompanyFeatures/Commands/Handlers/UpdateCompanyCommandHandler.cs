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
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateCompanyCommandRequest> _validator;
        public UpdateCompanyCommandHandler(IUnitOfWork uow, 
            IMapper mapper,
            IValidator<UpdateCompanyCommandRequest> validator)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<Result<UpdateCompanyCommandResponse>> Handle(UpdateCompanyCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return Result<UpdateCompanyCommandResponse>.Failure(string.Join(",", validationResult.Errors), (int)HttpStatusCode.BadRequest);

            var company = _mapper.Map<Company>(request);

            await _uow.CompanyRepository.UpdateAsync(company);
            await _uow.SaveChangesAsync();

            return _mapper.Map<UpdateCompanyCommandResponse>(company).ToResult();
        }
    }
}
