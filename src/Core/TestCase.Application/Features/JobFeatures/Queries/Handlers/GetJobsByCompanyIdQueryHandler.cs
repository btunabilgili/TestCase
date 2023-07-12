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
    public class GetJobsByCompanyIdQueryHandler : IRequestHandler<GetJobsByComapnyIdQueryRequest, Result<List<GetJobsByCompanyIdQueryResponse>>>
    {
        private readonly IBaseRepository<Job> _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<GetJobsByComapnyIdQueryRequest> _validator;
        public GetJobsByCompanyIdQueryHandler(IBaseRepository<Job> repository, 
            IMapper mapper,
            IValidator<GetJobsByComapnyIdQueryRequest> validator)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator)); 
        }

        public async Task<Result<List<GetJobsByCompanyIdQueryResponse>>> Handle(GetJobsByComapnyIdQueryRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return Result<List<GetJobsByCompanyIdQueryResponse>>.Failure(string.Join(",", validationResult.Errors), (int)HttpStatusCode.BadRequest);

            var jobs = await _repository.GetListAsync(predicate: x => x.CompanyId == request.ComapnyId);

            if (jobs is null || !jobs.Any())
                return Result<List<GetJobsByCompanyIdQueryResponse>>.Failure("No jobs found for that criteria", (int)HttpStatusCode.NotFound);

            return _mapper.Map<List<GetJobsByCompanyIdQueryResponse>>(jobs).ToResult();
        }
    }
}
