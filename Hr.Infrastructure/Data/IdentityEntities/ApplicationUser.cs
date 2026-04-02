using Hr.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Hr.Infrastructure.Data.IdentityEntities;

public class ApplicationUser : IdentityUser<int>
{
      public required string FullName { get; set; }
      public bool IsActive { get; set; } = true;

      public DateTime? LastLoginDate { get; set; }

      public string? ProfilePicture { get; set; }

      public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.Now);
}
