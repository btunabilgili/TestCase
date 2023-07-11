namespace TestCase.Application.Features.AuthFeatures.Commands.Responses
{
    public class TokenCreateCommandResponse
    {
        public required string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
