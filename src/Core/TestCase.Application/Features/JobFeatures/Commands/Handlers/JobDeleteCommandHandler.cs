using FluentValidation;
using MediatR;
using System.Net;
using TestCase.Application.Common;
using TestCase.Application.Features.JobFeatures.Commands.Requests;
using TestCase.Application.Features.JobFeatures.Commands.Responses;
using TestCase.Application.Interfaces;
using TestCase.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

            var job = await _uow.JobRepository.GetByIdAsync(request.Id);

            var company = await _uow.CompanyRepository.GetByIdAsync(job.CompanyId);

            if (company is null)
                return Result<bool>.Failure("Company not found", (int)HttpStatusCode.NotFound);

            await _uow.JobRepository.DeleteAsync(request.Id);

            _uow.CompanyRepository.Attach(company);
            company.RemainingJobCount += 1;

            await _uow.SaveChangesAsync();

            return true.ToResult();
        }
    }
}
