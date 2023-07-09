
using AutoMapper;
using FluentValidation;
using MediatR;
using System.Net;
using TestCase.Application.Common;
using TestCase.Application.Features.JobFeatures.Commands.Requests;
using TestCase.Application.Features.JobFeatures.Commands.Responses;
using TestCase.Application.Interfaces;
using TestCase.Domain.Entities;

namespace TestCase.Application.Features.JobFeatures.Commands.Handlers
{
    public class JobCreateCommandHandler : IRequestHandler<JobCreateCommandRequest, Result<JobCreateCommandResponse>>
    {
        private readonly IJobService _jobService;
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        private readonly IValidator<JobCreateCommandRequest> _validator;
        public JobCreateCommandHandler(IJobService jobService,
            ICompanyService companyService,
            IMapper mapper,
            IValidator<JobCreateCommandRequest> validator)
        {
            _jobService = jobService ?? throw new ArgumentNullException(nameof(jobService));
            _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<Result<JobCreateCommandResponse>> Handle(JobCreateCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return Result<JobCreateCommandResponse>.Failure(string.Join(",", validationResult.Errors), (int)HttpStatusCode.BadRequest);

            var job = _mapper.Map<Job>(request);

            var result = await _jobService.CreateJobAsync(job);

            if (!result.IsSuccess)
                return Result<JobCreateCommandResponse>.Failure(result.ErrorMessage!, result.StatusCode);

            //TODO: transaction between company and job!
            var companyResult = await _companyService.GetCompanyByIdAsync(job.CompanyId);
            var company = companyResult.Data!;
            company.RemainingJobCount -= 1;

            await _companyService.UpdateCompanyAsync(company);
            

            return _mapper.Map<JobCreateCommandResponse>(result.Data).ToResult();
        }
    }
}
