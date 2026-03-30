using System;

namespace Hr.Infrastructure.Data.IdentityEntities;

public class RolePermission
{
      public int RoleId { get; set; }
      public ApplicationRole? Role { get; set; }

      public int PermissionId { get; set; }
      public Permission? Permission { get; set; }
}
