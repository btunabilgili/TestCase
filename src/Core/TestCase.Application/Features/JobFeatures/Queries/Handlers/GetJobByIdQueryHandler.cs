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
    public class GetJobByIdQueryHandler : IRequestHandler<GetJobByIdQueryRequest, Result<GetJobByIdQueryResponse>>
    {
        private readonly IBaseRepository<Job> _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<GetJobByIdQueryRequest> _validator;
        public GetJobByIdQueryHandler(IBaseRepository<Job> repository, 
            IMapper mapper,
            IValidator<GetJobByIdQueryRequest> validator)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator)); 
        }

        public async Task<Result<GetJobByIdQueryResponse>> Handle(GetJobByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return Result<GetJobByIdQueryResponse>.Failure(string.Join(",", validationResult.Errors), (int)HttpStatusCode.BadRequest);

            var job = await _repository.GetByIdAsync(request.Id);

            if (job is null)
                return Result<GetJobByIdQueryResponse>.Failure("Job not found", (int)HttpStatusCode.NotFound);

            return _mapper.Map<GetJobByIdQueryResponse>(job).ToResult();
        }
    }
}
