using MediatR;
using TestCase.Application.Common;
using TestCase.Application.Features.CompanyFeatures.Commands.Responses;

namespace TestCase.Application.Features.CompanyFeatures.Commands.Requests
{
    public class UpdateCompanyCommandRequest : IRequest<Result<UpdateCompanyCommandResponse>>
    {
        public required Guid Id { get; set; }
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}
