using AutoMapper;
using FluentValidation;
using MediatR;
using System.Net;
using TestCase.Application.Common;
using TestCase.Application.Features.JobFeatures.Queries.Requests;
using TestCase.Application.Features.JobFeatures.Queries.Responses;
using TestCase.Application.Interfaces;

namespace TestCase.Application.Features.JobFeatures.Queries.Handlers
{
    public class GetJobsByListingDurationQueryHandler : IRequestHandler<GetJobsByListingDurationQueryRequest, Result<GetJobsByListingDurationQueryResponse>>
    {
        private readonly IJobService _jobService;
        private readonly IMapper _mapper;
        private readonly IValidator<GetJobsByListingDurationQueryRequest> _validator;
        public GetJobsByListingDurationQueryHandler(IJobService jobService, 
            IMapper mapper,
            IValidator<GetJobsByListingDurationQueryRequest> validator)
        {
            _jobService = jobService ?? throw new ArgumentNullException(nameof(jobService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator)); 
        }

        public async Task<Result<GetJobsByListingDurationQueryResponse>> Handle(GetJobsByListingDurationQueryRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return Result<GetJobsByListingDurationQueryResponse>.Failure(string.Join(",", validationResult.Errors), (int)HttpStatusCode.BadRequest);

            var result = await _jobService.GetJobsAsync(x => x.ListingDurationInDays == request.ListingDurationInDays);

            if (!result.IsSuccess)
                return Result<GetJobsByListingDurationQueryResponse>.Failure(result.ErrorMessage!, result.StatusCode);

            return _mapper.Map<GetJobsByListingDurationQueryResponse>(result.Data).ToResult();
        }
    }
}
