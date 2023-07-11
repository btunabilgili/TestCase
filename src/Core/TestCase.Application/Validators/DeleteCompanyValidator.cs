using FluentValidation;
using TestCase.Application.Features.CompanyFeatures.Commands.Requests;
using TestCase.Application.Interfaces;
using TestCase.Domain.Entities;

namespace TestCase.Application.Validators
{
    public class DeleteCompanyValidator : AbstractValidator<DeleteCompanyCommandRequest>
    {
        private readonly IBaseRepository<Company> _repository;
        public DeleteCompanyValidator(IBaseRepository<Company> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

            RuleFor(x => x.Id).NotEmpty().WithMessage("Id must not be empty").MustAsync(CompanyExists).WithMessage("Company not found");
        }

        private async Task<bool> CompanyExists(Guid companyId, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(companyId) is not null;
        }
    }
}
