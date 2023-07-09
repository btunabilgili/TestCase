using MediatR;
using TestCase.Application.Common;
using TestCase.Application.Features.CompanyFeatures.Commands.Responses;

namespace TestCase.Application.Features.CompanyFeatures.Commands.Requests
{
    public class UpdateCompanyCommandRequest : IRequest<Result<UpdateCompanyCommandResponse>>
    {
        public required Guid Id { get; set; }
        public required string CompanyName { get; set; }
        public required string Address { get; set; }
        public required string Phone { get; set; }
    }
}
