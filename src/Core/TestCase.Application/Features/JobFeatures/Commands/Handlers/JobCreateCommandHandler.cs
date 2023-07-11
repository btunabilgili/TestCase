using AutoMapper;
using FluentValidation;
using MediatR;
using System.Net;
using System.Text.RegularExpressions;
using TestCase.Application.Common;
using TestCase.Application.Features.JobFeatures.Commands.Requests;
using TestCase.Application.Features.JobFeatures.Commands.Responses;
using TestCase.Application.Interfaces;
using TestCase.Domain.Entities;

namespace TestCase.Application.Features.JobFeatures.Commands.Handlers
{
    public class JobCreateCommandHandler : IRequestHandler<JobCreateCommandRequest, Result<JobCreateCommandResponse>>
    {
        private readonly IBaseRepository<Job> _jobRepository;
        private readonly IBaseRepository<Company> _companyRepository;
        private readonly IJobService _jobService;
        private readonly IMapper _mapper;
        private readonly IValidator<JobCreateCommandRequest> _validator;
        public JobCreateCommandHandler(IBaseRepository<Job> jobRepository,
            IBaseRepository<Company> companyRepository,
            IJobService jobService,
            IMapper mapper,
            IValidator<JobCreateCommandRequest> validator)
        {
            _jobRepository = jobRepository ?? throw new ArgumentNullException(nameof(jobRepository));
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _jobService = jobService ?? throw new ArgumentNullException(nameof(jobService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<Result<JobCreateCommandResponse>> Handle(JobCreateCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return Result<JobCreateCommandResponse>.Failure(string.Join(",", validationResult.Errors), (int)HttpStatusCode.BadRequest);

            var company = await _companyRepository.GetByIdAsync(request.CompanyId);

            if (company is null)
                return Result<JobCreateCommandResponse>.Failure("Company not found", (int)HttpStatusCode.NotFound);

            var job = _mapper.Map<Job>(request);

            job.QualityPoint = _jobService.CalculateJobQualityPoint(job);

            await _jobRepository.AddAsync(job);

            //TODO: transaction between company and job!
            company.RemainingJobCount -= 1;

            _companyRepository.Update(company);
            

            return _mapper.Map<JobCreateCommandResponse>(job).ToResult();
        }
    }
}
