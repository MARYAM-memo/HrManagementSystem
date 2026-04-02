using System;
using Hr.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hr.Infrastructure.Configurations;

public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
{
      public void Configure(EntityTypeBuilder<Attendance> builder)
      {
            builder
                  .HasOne(a => a.Employee)
                  .WithMany(e => e.Attendances)
                  .HasForeignKey(a => a.EmployeeId)
                  .OnDelete(DeleteBehavior.Cascade);

            builder
                  .HasIndex(a => new { a.EmployeeId, a.Date })
                  .IsUnique();

            builder
                  .Property(a => a.Status)
                  .HasConversion<string>();

            builder
                  .Property(e => e.Date)
                  .HasColumnType("date");

            builder
                  .Property(a => a.CheckInTime)
                  .HasColumnType("time");

            builder
                  .Property(a => a.CheckOutTime)
                  .HasColumnType("time");
      }
}
