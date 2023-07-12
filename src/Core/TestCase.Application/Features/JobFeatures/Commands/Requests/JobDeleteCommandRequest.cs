using MediatR;
using TestCase.Application.Common;

namespace TestCase.Application.Features.JobFeatures.Commands.Requests
{
    public class JobDeleteCommandRequest : IRequest<Result<bool>>
    {
        public Guid Id { get; set; }
    }
}
