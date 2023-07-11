using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TestCase.Domain.Entities;

namespace TestCase.Infrastructure.Contexts.Configurations
{
    public class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.HasKey(j => j.Id);
            builder.Property(j => j.Position).IsRequired();
            builder.Property(j => j.JobDescription).IsRequired();
            builder.Property(j => j.ListingDurationInDays).IsRequired();
            builder.Property(j => j.QualityPoint).IsRequired();
            builder.Property(j => j.SalaryInformation);
            builder.Property(j => j.WorkType);

            builder.HasOne(j => j.Company)
                .WithMany(c => c.Jobs)
                .HasForeignKey(j => j.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(j => j.SideRights)
                .WithOne(sr => sr.Job)
                .HasForeignKey(sr => sr.JobId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
