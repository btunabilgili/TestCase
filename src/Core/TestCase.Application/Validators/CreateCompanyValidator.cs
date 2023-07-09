using FluentValidation;
using TestCase.Application.Features.CompanyFeatures.Commands.Requests;
using TestCase.Application.Interfaces;

namespace TestCase.Application.Validators
{
    public class CreateCompanyValidator : AbstractValidator<CreateCompanyCommandRequest>
    {
        private readonly ICompanyService _companyService;
        public CreateCompanyValidator(ICompanyService companyService)
        {
            _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));

            RuleFor(x => x.CompanyName).NotEmpty().WithMessage("Company Name must not be empty");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address must not be empty");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone must not be empty").MustAsync(BeUniquePhoneNumber).WithMessage("Phone number is already registered");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password must not be empty");
        }

        private async Task<bool> BeUniquePhoneNumber(string phoneNumber, CancellationToken cancellation)
        {
            return (await _companyService.IsPhoneNumberUnique(phoneNumber)).Data;
        }
    }
}
