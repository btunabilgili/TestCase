using AutoMapper;
using MediatR;
using System.Net;
using TestCase.Application.Common;
using TestCase.Application.Features.CompanyFeatures.Queries.Requests;
using TestCase.Application.Features.CompanyFeatures.Queries.Responses;
using TestCase.Application.Interfaces;
using TestCase.Domain.Entities;

namespace TestCase.Application.Features.CompanyFeatures.Queries.Handlers
{
    public class GetAllCompaniesQueryHandler : IRequestHandler<GetAllCompaniesQueryRequest, Result<List<GetAllCompaniesQueryResponse>>>
    {
        private readonly IBaseRepository<Company> _repository;
        private readonly IMapper _mapper;
        public GetAllCompaniesQueryHandler(IBaseRepository<Company> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<List<GetAllCompaniesQueryResponse>>> Handle(GetAllCompaniesQueryRequest request, CancellationToken cancellationToken)
        {
            var companies = await _repository.GetListAsync(includeExpression: x => x.Jobs!);

            if (companies is null || !companies.Any())
                return Result<List<GetAllCompaniesQueryResponse>>.Failure("Data not found.", (int)HttpStatusCode.NotFound);

            return _mapper.Map<List<GetAllCompaniesQueryResponse>>(companies).ToResult();
        }
    }
}
