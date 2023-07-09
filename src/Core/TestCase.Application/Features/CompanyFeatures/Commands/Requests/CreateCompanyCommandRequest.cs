using MediatR;
using TestCase.Application.Common;
using TestCase.Application.Features.CompanyFeatures.Commands.Responses;
using TestCase.Domain.Entities;

namespace TestCase.Application.Features.CompanyFeatures.Commands.Requests
{
    public class CreateCompanyCommandRequest : IRequest<Result<CreateCompanyCommandResponse>>
    {
        public required string Password { get; set; }
        public required string CompanyName { get; set; }
        public required string Address { get; set; }
        public required string Phone { get; set; }
    }
}
