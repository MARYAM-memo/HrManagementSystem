using System;
using Hr.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hr.Infrastructure.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
      public void Configure(EntityTypeBuilder<Department> builder)
      {
            builder
                 .HasOne(d => d.Manager)
                 .WithMany(e => e.ManagedDepartments)
                 .HasForeignKey(d => d.ManagerId)
                 .OnDelete(DeleteBehavior.Restrict);
      }
}
