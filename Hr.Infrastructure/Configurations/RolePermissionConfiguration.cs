using System;
using Hr.Core.Entities;
using Hr.Infrastructure.Data.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hr.Infrastructure.Configurations;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
      public void Configure(EntityTypeBuilder<RolePermission> builder)
      {
            builder
                  .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            builder
                  .HasOne<ApplicationRole>()
                  .WithMany()
                  .HasForeignKey(rp => rp.RoleId)
                  .OnDelete(DeleteBehavior.Cascade);

            builder
                  .HasOne(rp => rp.Permission)
                  .WithMany(p => p.RolePermissions)
                  .HasForeignKey(rp => rp.PermissionId)
                  .OnDelete(DeleteBehavior.Cascade);

      }
}
