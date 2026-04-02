using System;
using Hr.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hr.Infrastructure.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
      public void Configure(EntityTypeBuilder<Employee> builder)
      {
            builder
                  .HasOne(d => d.Manager)
                  .WithMany(e => e.Subordinates)
                  .HasForeignKey(d => d.ManagerId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder
                  .HasOne(d => d.Department)
                  .WithMany(d => d.Employees)
                  .HasForeignKey(d => d.DepartmentId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder
                  .HasOne(d => d.JobTitle)
                  .WithMany(d => d.Employees)
                  .HasForeignKey(d => d.JobTitleId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder
                  .Property(e => e.HireDate)
                  .HasColumnType("date");

            builder
                  .Property(e => e.BirthDate)
                  .HasColumnType("date");
      }
}
