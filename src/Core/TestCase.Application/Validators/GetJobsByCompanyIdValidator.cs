using FluentValidation;
using TestCase.Application.Features.JobFeatures.Queries.Requests;

namespace TestCase.Application.Validators
{
    public class GetJobsByCompanyIdValidator : AbstractValidator<GetJobsByComapnyIdQueryRequest>
    {
        public GetJobsByCompanyIdValidator()
        {
            RuleFor(x => x.ComapnyId).NotEmpty().WithMessage("Comapny Id must not be empty");
        }
    }
}
