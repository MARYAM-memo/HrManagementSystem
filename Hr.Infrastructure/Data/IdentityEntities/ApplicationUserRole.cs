using Hr.Infrastructure.Data.IdentityEntities;
using Microsoft.AspNetCore.Identity;

namespace Hr.Infrastructure.Data.IdentityEntities;

public class ApplicationUserRole : IdentityUserRole<int>
{
      public virtual ApplicationUser? User { get; set; }
      public virtual ApplicationRole? Role { get; set; }

      public override string ToString()
      {
            return $"{User?.FullName ?? ""} [{Role?.Name ?? ""}]";
      }
}
