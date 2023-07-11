using MediatR;
using System.Net;
using TestCase.Application.Common;
using TestCase.Application.Features.AuthFeatures.Commands.Requests;
using TestCase.Application.Features.AuthFeatures.Commands.Responses;
using TestCase.Application.Interfaces;
using TestCase.Application.Tools;
using TestCase.Domain.Entities;

namespace TestCase.Application.Features.AuthFeatures.Commands.Handlers
{
    public class TokenCreateCommandHandler : IRequestHandler<TokenCreateCommandRequest, Result<TokenCreateCommandResponse>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IBaseRepository<Company> _repository;
        public TokenCreateCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IBaseRepository<Company> repository)
        {
            _jwtTokenGenerator = jwtTokenGenerator ?? throw new ArgumentNullException(nameof(jwtTokenGenerator));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Result<TokenCreateCommandResponse>> Handle(TokenCreateCommandRequest request, CancellationToken cancellationToken)
        {
            var company = await _repository.FirstOrDefaultAsync(x => x.Phone == request.Phone && x.PasswordHash == HashPasswordHelper.HashPassword(request.Password));

            if (company is null)
                return Result<TokenCreateCommandResponse>.Failure("Phone number or password is wrong", (int)HttpStatusCode.BadRequest);

            var response = _jwtTokenGenerator.GenerateJwtToken(company.Id);

            return response.ToResult();
        }
    }
}
