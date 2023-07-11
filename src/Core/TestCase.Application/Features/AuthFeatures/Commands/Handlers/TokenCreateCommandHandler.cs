using MediatR;
using System.Net;
using TestCase.Application.Common;
using TestCase.Application.Features.AuthFeatures.Commands.Requests;
using TestCase.Application.Features.AuthFeatures.Commands.Responses;
using TestCase.Application.Interfaces;
using TestCase.Application.Tools;

namespace TestCase.Application.Features.AuthFeatures.Commands.Handlers
{
    public class TokenCreateCommandHandler : IRequestHandler<TokenCreateCommandRequest, Result<TokenCreateCommandResponse>>
    {
        private readonly IAuthService _authService;
        private readonly ICompanyService _companyService;
        public TokenCreateCommandHandler(IAuthService authService, ICompanyService companyService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
        }

        public async Task<Result<TokenCreateCommandResponse>> Handle(TokenCreateCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _companyService.GetCompanyAsync(x => x.Phone == request.Phone && x.PasswordHash == HashPasswordHelper.HashPassword(request.Password));

            if (result.StatusCode == (int)HttpStatusCode.NotFound)
                return Result<TokenCreateCommandResponse>.Failure("Phone number or password is wrong", (int)HttpStatusCode.BadRequest);
            else if(!result.IsSuccess)
                return Result<TokenCreateCommandResponse>.Failure(result.ErrorMessage!, result.StatusCode);

            var response = _authService.GenerateJwtToken(result!.Data!.Id);

            return response.ToResult();
        }
    }
}
