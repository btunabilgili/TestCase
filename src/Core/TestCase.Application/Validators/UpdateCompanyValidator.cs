using FluentValidation;
using TestCase.Application.Features.CompanyFeatures.Commands.Requests;

namespace TestCase.Application.Validators
{
    public class UpdateCompanyValidator : AbstractValidator<UpdateCompanyCommandRequest>
    {
        public UpdateCompanyValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id must not be empty");
            RuleFor(x => x.CompanyName).NotEmpty().WithMessage("Company Name must not be empty");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address must not be empty");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone must not be empty");
        }
    }
}
