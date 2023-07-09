using FluentValidation;
using TestCase.Application.Features.JobFeatures.Commands.Requests;

namespace TestCase.Application.Validators
{
    public class JobUpdateValidator : AbstractValidator<JobUpdateCommandRequest>
    {
        public JobUpdateValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id must not be empty");
            RuleFor(x => x.Position).NotEmpty().WithMessage("Position must not be empty");
            RuleFor(x => x.JobDescription).NotEmpty().WithMessage("Job description must not be empty");
            RuleFor(x => x.CompanyId).NotEmpty().WithMessage("Company Id must not be empty");
        }
    }
}
