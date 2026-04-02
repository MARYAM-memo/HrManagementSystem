using System;
using Hr.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hr.Infrastructure.Configurations;

public class PayrollConfiguration : IEntityTypeConfiguration<Payroll>
{
      public void Configure(EntityTypeBuilder<Payroll> builder)
      {
            builder
                  .HasOne(a => a.Employee)
                  .WithMany(e => e.Payrolls)
                  .HasForeignKey(a => a.EmployeeId)
                  .OnDelete(DeleteBehavior.Cascade);

            builder
                  .HasIndex(p => new { p.EmployeeId, p.Month, p.Year })
                  .IsUnique();

            builder
                  .Property(p => p.BaseSalary)
                  .HasColumnType("numeric(18,2)");

            builder
                  .Property(p => p.NetSalary)
                  .HasColumnType("numeric(18,2)");
      }
}
