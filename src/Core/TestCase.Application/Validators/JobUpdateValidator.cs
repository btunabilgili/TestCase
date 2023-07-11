using FluentValidation;
using TestCase.Application.Features.JobFeatures.Commands.Requests;
using TestCase.Application.Interfaces;
using TestCase.Domain.Entities;

namespace TestCase.Application.Validators
{
    public class JobUpdateValidator : AbstractValidator<JobUpdateCommandRequest>
    {
        private readonly IBaseRepository<Job> _repository;
        public JobUpdateValidator(IBaseRepository<Job> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

            RuleFor(x => x.Id).NotEmpty().WithMessage("Id must not be empty").MustAsync(JobExists).WithMessage("Job not found");
            RuleFor(x => x.Position).NotEmpty().WithMessage("Position must not be empty");
            RuleFor(x => x.JobDescription).NotEmpty().WithMessage("Job description must not be empty");
        }

        private async Task<bool> JobExists(Guid jobId, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(jobId) is not null;
        }
    }
}
