using MediatR;
using TestCase.Application.Common;

namespace TestCase.Application.Features.CompanyFeatures.Commands.Requests
{
    public class DeleteCompanyCommandRequest : IRequest<Result<bool>>
    {
        public required Guid Id { get; set; }
    }
}
