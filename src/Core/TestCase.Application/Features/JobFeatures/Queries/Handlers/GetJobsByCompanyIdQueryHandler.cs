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
    public class GetJobsByCompanyIdQueryHandler : IRequestHandler<GetJobsByComapnyIdQueryRequest, Result<List<GetJobsByCompanyIdQueryResponse>>>
    {
        private readonly IJobService _jobService;
        private readonly IMapper _mapper;
        private readonly IValidator<GetJobsByComapnyIdQueryRequest> _validator;
        public GetJobsByCompanyIdQueryHandler(IJobService jobService, 
            IMapper mapper,
            IValidator<GetJobsByComapnyIdQueryRequest> validator)
        {
            _jobService = jobService ?? throw new ArgumentNullException(nameof(jobService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator)); 
        }

        public async Task<Result<List<GetJobsByCompanyIdQueryResponse>>> Handle(GetJobsByComapnyIdQueryRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return Result<List<GetJobsByCompanyIdQueryResponse>>.Failure(string.Join(",", validationResult.Errors), (int)HttpStatusCode.BadRequest);

            var result = await _jobService.GetJobsByCompanyId(request.ComapnyId);

            if (!result.IsSuccess)
                return Result<List<GetJobsByCompanyIdQueryResponse>>.Failure(result.ErrorMessage!, result.StatusCode);

            return _mapper.Map<List<GetJobsByCompanyIdQueryResponse>>(result.Data).ToResult();
        }
    }
}
