using FluentValidation;
using MediatR;
using System.Net;
using TestCase.Application.Common;
using TestCase.Application.Features.JobFeatures.Commands.Requests;
using TestCase.Application.Interfaces;
using TestCase.Domain.Entities;

namespace TestCase.Application.Features.JobFeatures.Commands.Handlers
{
    public class JobDeleteCommandHandler : IRequestHandler<JobDeleteCommandRequest, Result<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<JobDeleteCommandRequest> _validator;
        public JobDeleteCommandHandler(IUnitOfWork uow,
            IValidator<JobDeleteCommandRequest> validator)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<Result<bool>> Handle(JobDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return Result<bool>.Failure(string.Join(",", validationResult.Errors), (int)HttpStatusCode.BadRequest);

            await _uow.JobRepository.DeleteAsync(request.Id);
            await _uow.SaveChangesAsync();

            return true.ToResult();
        }
    }
}
