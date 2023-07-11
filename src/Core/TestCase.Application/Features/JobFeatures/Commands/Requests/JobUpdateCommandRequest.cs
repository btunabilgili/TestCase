using MediatR;
using TestCase.Application.Common;
using TestCase.Application.Features.JobFeatures.Commands.Responses;
using TestCase.Domain.Entities;
using TestCase.Domain.Enums;

namespace TestCase.Application.Features.JobFeatures.Commands.Requests
{
    public class JobUpdateCommandRequest : IRequest<Result<JobUpdateCommandResponse>>
    {
        public required Guid Id { get; set; }
        public required string Position { get; set; }
        public required string JobDescription { get; set; }
        public List<SideRightTypes>? SideRights { get; set; }
        public WorkTypes? WorkType { get; set; }
        public int? SalaryInformation { get; set; }
    }
}
