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
    public class JobUpdateCommandHandler : IRequestHandler<JobUpdateCommandRequest, Result<JobUpdateCommandResponse>>
    {
        private readonly IBaseRepository<Job> _repository;
        private readonly IJobService _jobService;
        private readonly IMapper _mapper;
        private readonly IValidator<JobUpdateCommandRequest> _validator;
        public JobUpdateCommandHandler(IBaseRepository<Job> repository,
            IJobService jobService,
            IMapper mapper,
            IValidator<JobUpdateCommandRequest> validator)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _jobService = jobService ?? throw new ArgumentNullException(nameof(jobService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<Result<JobUpdateCommandResponse>> Handle(JobUpdateCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return Result<JobUpdateCommandResponse>.Failure(string.Join(",", validationResult.Errors), (int)HttpStatusCode.BadRequest);

            var job = _mapper.Map<Job>(request);

            job.QualityPoint = _jobService.CalculateJobQualityPoint(job);

            _repository.Update(job);

            return _mapper.Map<JobUpdateCommandResponse>(job).ToResult();
        }
    }
}
