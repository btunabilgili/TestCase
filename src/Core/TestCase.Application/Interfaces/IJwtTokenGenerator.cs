using TestCase.Application.Features.AuthFeatures.Commands.Responses;

namespace TestCase.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        TokenCreateCommandResponse GenerateJwtToken(Guid companyId);
    }
}
