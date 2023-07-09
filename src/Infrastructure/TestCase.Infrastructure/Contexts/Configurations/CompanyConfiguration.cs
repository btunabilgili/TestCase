using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TestCase.Domain.Entities;

namespace TestCase.Infrastructure.Contexts.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.PasswordHash).IsRequired();
            builder.Property(c => c.CompanyName).IsRequired();
            builder.Property(c => c.Address).IsRequired();
            builder.Property(c => c.Phone).IsRequired();
            builder.Property(c => c.RemainingJobCount).IsRequired();

            builder.HasMany(c => c.Jobs)
                .WithOne(j => j.Company)
                .HasForeignKey(j => j.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
