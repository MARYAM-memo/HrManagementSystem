using Hr.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hr.Infrastructure.Configurations;

public class AdjustmentConfiguration : IEntityTypeConfiguration<Adjustment>
{
      public void Configure(EntityTypeBuilder<Adjustment> builder)
      {
            builder
                  .HasOne(a => a.Payroll)
                  .WithMany()
                  .HasForeignKey(a => a.PayrollId)
                  .OnDelete(DeleteBehavior.SetNull);

            builder
                  .HasOne(a => a.Employee)
                  .WithMany(e => e.Adjustments)
                  .HasForeignKey(a => a.EmployeeId)
                  .OnDelete(DeleteBehavior.Cascade);

            builder
                  .Property(a => a.Type)
                  .HasConversion<string>();

            builder
                  .Property(e => e.Date)
                  .HasColumnType("date");
      }
}
