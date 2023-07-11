using MediatR;
using TestCase.Application.Common;
using TestCase.Application.Features.JobFeatures.Commands.Responses;
using TestCase.Domain.Entities;
using TestCase.Domain.Enums;

namespace TestCase.Application.Features.JobFeatures.Commands.Requests
{
    public class JobDeleteCommandRequest : IRequest<Result<bool>>
    {
        public Guid Id { get; set; }
    }
}
