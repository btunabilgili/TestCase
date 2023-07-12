using FluentValidation;
using TestCase.Application.Features.JobFeatures.Commands.Requests;
using TestCase.Application.Interfaces;
using TestCase.Domain.Entities;

namespace TestCase.Application.Validators
{
    public class JobCreateValidator : AbstractValidator<JobCreateCommandRequest>
    {
        private readonly IBaseRepository<Company> _repository;
        public JobCreateValidator(IBaseRepository<Company> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

            RuleFor(x => x.Position).NotEmpty().WithMessage("Position must not be empty");
            RuleFor(x => x.JobDescription).NotEmpty().WithMessage("Job description must not be empty");
            RuleFor(x => x.CompanyId).NotEmpty().WithMessage("Company Id must not be empty").MustAsync(CheckForRemaningJobPost).WithMessage("You don't have enough job posts remaining");
            RuleFor(x => x.ListingDurationInDays).NotEmpty().WithMessage("Listing duration must not be empty").MustAsync(BeValidListingDurationInDays).WithMessage("Listing duration must be greater than zero");
        }

        private async Task<bool> CheckForRemaningJobPost(Guid companyId, CancellationToken cancellation)
        {
            var company = await _repository.GetByIdAsync(companyId);

            if (company is null)
                return false;

            return company.RemainingJobCount > 0;
        }

        private Task<bool> BeValidListingDurationInDays(int listingDurationInDays, CancellationToken cancellation)
        {
            return Task.FromResult(listingDurationInDays > 0);
        }
    }
}
