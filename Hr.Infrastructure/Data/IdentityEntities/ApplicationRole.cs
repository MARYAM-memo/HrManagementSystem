using Microsoft.AspNetCore.Identity;

namespace Hr.Infrastructure.Data.IdentityEntities;

public class ApplicationRole : IdentityRole<int>
{
      public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.Now);

      //Permissions
      public ICollection<RolePermission> RolePermissions { get; set; } = [];
}
