using System;
using Hr.Infrastructure.Data.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hr.Infrastructure.Configurations;

public class ApplicationUserRoleConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
{
      public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
      {
            builder
                  .HasOne(ur => ur.User)
                  .WithMany()
                  .HasForeignKey(ur => ur.UserId)
                  .IsRequired();

            builder
                  .HasOne(ur => ur.Role)
                  .WithMany()
                  .HasForeignKey(ur => ur.RoleId)
                  .IsRequired();
      }
}
