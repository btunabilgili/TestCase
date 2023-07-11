using MediatR;
using TestCase.Application.Common;
using TestCase.Application.Features.AuthFeatures.Commands.Responses;

namespace TestCase.Application.Features.AuthFeatures.Commands.Requests
{
    public class TokenCreateCommandRequest : IRequest<Result<TokenCreateCommandResponse>>
    {
        public required string Phone { get; set; }
        public required string Password { get; set; }
    }
}
