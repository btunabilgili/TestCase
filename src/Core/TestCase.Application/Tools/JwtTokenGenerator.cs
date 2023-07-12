using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TestCase.Application.Common;
using TestCase.Application.Features.AuthFeatures.Commands.Responses;
using TestCase.Application.Interfaces;

namespace TestCase.Application.Tools
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtOptions _jwtOptions;
        private readonly ILogger<JwtTokenGenerator> _logger;

        public JwtTokenGenerator(IOptions<JwtOptions> options, ILogger<JwtTokenGenerator> logger)
        {
            _jwtOptions = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public TokenCreateCommandResponse GenerateJwtToken(Guid companyId)
        {
            _logger.LogInformation("JWT Generation");
            _logger.LogInformation(_jwtOptions.Key);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new Dictionary<string, string>
            {
                {
                    "companyId",
                    companyId.ToString()
                }
            };

            var expiresAt = DateTime.Now.AddMinutes(15);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims?.Select(x => new Claim(x.Key, x.Value)),
                expires: expiresAt,
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var serializedToken = tokenHandler.WriteToken(token);

            return new TokenCreateCommandResponse
            {
                Token = serializedToken,
                ExpiresAt = expiresAt
            };
        }
    }
}
