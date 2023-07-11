using FluentValidation;
using TestCase.Application.Features.JobFeatures.Commands.Requests;
using TestCase.Application.Interfaces;

namespace TestCase.Application.Validators
{
    public class JobCreateValidator : AbstractValidator<JobCreateCommandRequest>
    {
        private readonly ICompanyService _companyService;
        public JobCreateValidator(ICompanyService companyService)
        {
            _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));

            RuleFor(x => x.Position).NotEmpty().WithMessage("Position must not be empty");
            RuleFor(x => x.JobDescription).NotEmpty().WithMessage("Job description must not be empty");
            RuleFor(x => x.CompanyId).NotEmpty().WithMessage("Company Id must not be empty").MustAsync(CheckForRemaningJobPost).WithMessage("You don't have enough job posts remaining");
        }

        private async Task<bool> CheckForRemaningJobPost(Guid companyId, CancellationToken cancellation)
        {
            var result = await _companyService.GetCompanyAsync(x => x.Id == companyId);

            if (result is null || result.IsSuccess == false)
                return false;

            return result.Data!.RemainingJobCount > 0;
        }
    }
}
