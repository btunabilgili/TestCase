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
    public class GetJobByIdQueryHandler : IRequestHandler<GetJobByIdQueryRequest, Result<GetJobByIdQueryResponse>>
    {
        private readonly IJobService _jobService;
        private readonly IMapper _mapper;
        private readonly IValidator<GetJobByIdQueryRequest> _validator;
        public GetJobByIdQueryHandler(IJobService jobService, 
            IMapper mapper,
            IValidator<GetJobByIdQueryRequest> validator)
        {
            _jobService = jobService ?? throw new ArgumentNullException(nameof(jobService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator)); 
        }

        public async Task<Result<GetJobByIdQueryResponse>> Handle(GetJobByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return Result<GetJobByIdQueryResponse>.Failure(string.Join(",", validationResult.Errors), (int)HttpStatusCode.BadRequest);

            var result = await _jobService.GetJobByIdAsync(request.Id);

            if (!result.IsSuccess)
                return Result<GetJobByIdQueryResponse>.Failure(result.ErrorMessage!, result.StatusCode);

            return _mapper.Map<GetJobByIdQueryResponse>(result.Data).ToResult();
        }
    }
}
