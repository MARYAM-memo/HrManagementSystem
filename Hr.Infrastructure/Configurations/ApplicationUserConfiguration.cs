using System;
using Hr.Infrastructure.Data.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hr.Infrastructure.Configurations;

public class ApplicationUserConfiguration: IEntityTypeConfiguration<ApplicationUser>
{
      public void Configure(EntityTypeBuilder<ApplicationUser> builder)
      {
          /*   builder
                  .HasIndex(u => u.EmployeeId)
                  .IsUnique();

            builder
                  .HasOne(u => u.Employee)
                  .WithMany()
                  .HasForeignKey(u => u.EmployeeId)
                  .OnDelete(DeleteBehavior.SetNull);
       */}
}
