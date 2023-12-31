﻿using MediatR;
using TestCase.Application.Common;
using TestCase.Application.Features.JobFeatures.Commands.Responses;
using TestCase.Domain.Enums;

namespace TestCase.Application.Features.JobFeatures.Commands.Requests
{
    public class JobCreateCommandRequest : IRequest<Result<JobCreateCommandResponse>>
    {
        public required string Position { get; set; }
        public required string JobDescription { get; set; }
        public required int ListingDurationInDays { get; set; }
        public List<SideRightTypes>? SideRights { get; set; }
        public WorkTypes? WorkType { get; set; }
        public int? SalaryInformation { get; set; }
        public required Guid CompanyId { get; set; }
    }
}
