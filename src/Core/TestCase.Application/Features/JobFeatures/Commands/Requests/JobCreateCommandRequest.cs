﻿using MediatR;
using TestCase.Application.Common;
using TestCase.Application.Features.JobFeatures.Commands.Responses;
using TestCase.Domain.Entities;
using TestCase.Domain.Enums;

namespace TestCase.Application.Features.JobFeatures.Commands.Requests
{
    public class JobCreateCommandRequest : IRequest<Result<JobCreateCommandResponse>>
    {
        public required string Position { get; set; }
        public required string JobDescription { get; set; }
        public int QualityPoint { get; set; }
        public ICollection<SideRights>? SideRights { get; set; }
        public WorkTypes WorkType { get; set; }
        public int SalaryInformation { get; set; }
        public required Guid CompanyId { get; set; }
    }
}
