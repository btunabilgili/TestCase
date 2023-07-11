using AutoMapper;
using FluentValidation;
using MediatR;
using System.Net;
using TestCase.Application.Common;
using TestCase.Application.Features.JobFeatures.Queries.Requests;
using TestCase.Application.Features.JobFeatures.Queries.Responses;
using TestCase.Application.Interfaces;
using TestCase.Domain.Entities;

namespace TestCase.Application.Features.JobFeatures.Queries.Handlers
{
    public class GetJobsByListingDurationQueryHandler : IRequestHandler<GetJobsByListingDurationQueryRequest, Result<GetJobsByListingDurationQueryResponse>>
    {
        private readonly IBaseRepository<Job> _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<GetJobsByListingDurationQueryRequest> _validator;
        public GetJobsByListingDurationQueryHandler(IBaseRepository<Job> repository, 
            IMapper mapper,
            IValidator<GetJobsByListingDurationQueryRequest> validator)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator)); 
        }

        public async Task<Result<GetJobsByListingDurationQueryResponse>> Handle(GetJobsByListingDurationQueryRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return Result<GetJobsByListingDurationQueryResponse>.Failure(string.Join(",", validationResult.Errors), (int)HttpStatusCode.BadRequest);

            var jobs = await _repository.GetListAsync(x => x.ListingDurationInDays == request.ListingDurationInDays);

            if (jobs is null || !jobs.Any())
                return Result<GetJobsByListingDurationQueryResponse>.Failure("No jobs found for that criteria", (int)HttpStatusCode.NotFound);

            return _mapper.Map<GetJobsByListingDurationQueryResponse>(jobs).ToResult();
        }
    }
}
