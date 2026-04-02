using System;
using Hr.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hr.Infrastructure.Configurations;

public class LeaveConfiguration : IEntityTypeConfiguration<Leave>
{
      public void Configure(EntityTypeBuilder<Leave> builder)
      {
            builder
                  .HasOne(l => l.Approver)
                  .WithMany()
                  .HasForeignKey(l => l.ApprovedById)
                  .OnDelete(DeleteBehavior.Restrict);

            builder
                  .HasOne(a => a.Employee)
                  .WithMany(e => e.Leaves)
                  .HasForeignKey(a => a.EmployeeId)
                  .OnDelete(DeleteBehavior.Cascade);

            builder
                  .Property(a => a.Status)
                  .HasConversion<string>();

            builder
                  .Property(a => a.Type)
                  .HasConversion<string>();

            builder
                  .Property(e => e.ToDate)
                  .HasColumnType("date");
                 
            builder
                  .Property(e => e.FromDate)
                  .HasColumnType("date");
      }
}
