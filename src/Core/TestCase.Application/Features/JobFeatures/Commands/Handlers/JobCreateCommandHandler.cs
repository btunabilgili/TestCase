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
        private readonly IUnitOfWork _uow;
        private readonly IJobService _jobService;
        private readonly IMapper _mapper;
        private readonly IValidator<JobCreateCommandRequest> _validator;
        public JobCreateCommandHandler(IUnitOfWork uow,
            IBaseRepository<Company> companyRepository,
            IJobService jobService,
            IMapper mapper,
            IValidator<JobCreateCommandRequest> validator)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _jobService = jobService ?? throw new ArgumentNullException(nameof(jobService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<Result<JobCreateCommandResponse>> Handle(JobCreateCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return Result<JobCreateCommandResponse>.Failure(string.Join(",", validationResult.Errors), (int)HttpStatusCode.BadRequest);

            var company = await _uow.CompanyRepository.GetByIdAsync(request.CompanyId);

            if (company is null)
                return Result<JobCreateCommandResponse>.Failure("Company not found", (int)HttpStatusCode.NotFound);

            var job = _mapper.Map<Job>(request);

            job.QualityPoint = _jobService.CalculateJobQualityPoint(job);
            await _uow.JobRepository.AddAsync(job);

            company.RemainingJobCount -= 1;
            await _uow.CompanyRepository.UpdateAsync(company);

            await _uow.SaveChangesAsync();

            return _mapper.Map<JobCreateCommandResponse>(job).ToResult();
        }
    }
}
