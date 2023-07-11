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
        private readonly IUnitOfWork _uow;
        private readonly IJobService _jobService;
        private readonly IMapper _mapper;
        private readonly IValidator<JobUpdateCommandRequest> _validator;
        public JobUpdateCommandHandler(IUnitOfWork uow,
            IJobService jobService,
            IMapper mapper,
            IValidator<JobUpdateCommandRequest> validator)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
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
            await _uow.JobRepository.UpdateAsync(job);

            await _uow.SaveChangesAsync();

            return _mapper.Map<JobUpdateCommandResponse>(job).ToResult();
        }
    }
}
