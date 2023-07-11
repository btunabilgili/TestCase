using TestCase.Application.Features.AuthFeatures.Commands.Responses;

namespace TestCase.Application.Interfaces
{
    public interface IAuthService
    {
        TokenCreateCommandResponse GenerateJwtToken(Guid companyId);
    }
}
