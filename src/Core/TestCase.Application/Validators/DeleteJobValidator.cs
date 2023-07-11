using FluentValidation;
using TestCase.Application.Features.JobFeatures.Commands.Requests;
using TestCase.Application.Interfaces;
using TestCase.Domain.Entities;

namespace TestCase.Application.Validators
{
    public class DeleteJobValidator : AbstractValidator<JobDeleteCommandRequest>
    {
        private readonly IBaseRepository<Job> _repository;
        public DeleteJobValidator(IBaseRepository<Job> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

            RuleFor(x => x.Id).NotEmpty().WithMessage("Id must not be empty").MustAsync(JobExists).WithMessage("Job not found");
        }

        private async Task<bool> JobExists(Guid jobId, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(jobId) is not null;
        }
    }
}
