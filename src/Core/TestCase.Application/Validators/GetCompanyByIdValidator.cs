using FluentValidation;
using TestCase.Application.Features.CompanyFeatures.Commands.Requests;
using TestCase.Application.Features.CompanyFeatures.Queries.Requests;

namespace TestCase.Application.Validators
{
    public class GetCompanyByIdValidator : AbstractValidator<GetCompanyByIdQueryRequest>
    {
        public GetCompanyByIdValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id must not be empty");
        }
    }
}
