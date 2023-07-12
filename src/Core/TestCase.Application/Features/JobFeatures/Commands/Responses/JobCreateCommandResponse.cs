﻿using TestCase.Domain.Entities;
using TestCase.Domain.Enums;

namespace TestCase.Application.Features.JobFeatures.Commands.Responses
{
    public class JobCreateCommandResponse
    {
        public required Guid Id { get; set; }
        public required string Position { get; set; }
        public required string JobDescription { get; set; }
        public required int ListingDurationInDays { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int QualityPoint { get; set; }
        public string? SideRights { get; set; }
        public WorkTypes WorkType { get; set; }
        public int SalaryInformation { get; set; }

        public required Guid CompanyId { get; set; }
    }
}
