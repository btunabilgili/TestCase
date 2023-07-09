using FluentValidation;
using TestCase.Application.Features.CompanyFeatures.Commands.Requests;
using TestCase.Application.Features.CompanyFeatures.Queries.Requests;
using TestCase.Application.Features.JobFeatures.Queries.Requests;

namespace TestCase.Application.Validators
{
    public class GetJobByIdValidator : AbstractValidator<GetJobByIdQueryRequest>
    {
        public GetJobByIdValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id must not be empty");
        }
    }
}
