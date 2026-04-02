using Hr.Core.Entities;
using Hr.Infrastructure.Data.IdentityEntities;
using Hr.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hr.Infrastructure.Data;

public class SeedData
{
      public static async Task Initialize(IServiceProvider serviceProvider)
      {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            // Create database if not exists
            await context.Database.MigrateAsync();

            // Seed Permissions FIRST
            await SeedPermissionsAsync(context);

            // Seed Roles
            await SeedRolesAsync(roleManager, context);

            // Seed Admin User
            await SeedAdminUserAsync(userManager, roleManager);
      }

      static async Task SeedPermissionsAsync(DatabaseContext context)
      {
            var allPermissions = GetAllPermissions();

            foreach (var permission in allPermissions)
            {
                  // تحقق إذا كانت الصلاحية موجودة بالفعل
                  var existingPermission = await context.Permissions
                      .FirstOrDefaultAsync(p => p.Name == permission.Name);

                  if (existingPermission == null)
                  {
                        await context.Permissions.AddAsync(permission);
                  }
            }

            await context.SaveChangesAsync();
      }

      static List<Permission> GetAllPermissions()
      {
            return
            [
                // Employees
                new() { Name = "Employees.View" },
                new() { Name = "Employees.Create" },
                new() { Name = "Employees.Edit" },
                new() { Name = "Employees.Delete" },
            
                // Departments
                new() { Name = "Departments.View" },
                new() { Name = "Departments.Create" },
                new() { Name = "Departments.Edit" },
                new() { Name = "Departments.Delete" },
            
                // JobTitles
                new() { Name = "JobTitles.View" },
                new() { Name = "JobTitles.Create" },
                new() { Name = "JobTitles.Edit" },
                new() { Name = "JobTitles.Delete" },
            
                // Attendance
                new() { Name = "Attendance.View" },
                new() { Name = "Attendance.CheckIn" },
                new() { Name = "Attendance.CheckOut" },
                new() { Name = "Attendance.Edit" },
                new() { Name = "Attendance.Delete" },
            
                // Leaves
                new() { Name = "Leaves.View" },
                new() { Name = "Leaves.Create" },
                new() { Name = "Leaves.Edit" },
                new() { Name = "Leaves.Delete" },
                new() { Name = "Leaves.Approve" },
                new() { Name = "Leaves.Reject" },
            
                // Payroll
                new() { Name = "Payroll.View" },
                new() { Name = "Payroll.Generate" },
                new() { Name = "Payroll.Edit" },
                new() { Name = "Payroll.Delete" },
            
                // Adjustments
                new() { Name = "Adjustments.View" },
                new() { Name = "Adjustments.Create" },
                new() { Name = "Adjustments.Edit" },
                new() { Name = "Adjustments.Delete" },
            
                // Users
                new() { Name = "Users.View" },
                new() { Name = "Users.Create" },
                new() { Name = "Users.Edit" },
                new() { Name = "Users.Delete" },
                new() { Name = "Users.AssignRoles" },
            
                // Roles
                new() { Name = "Roles.View" },
                new() { Name = "Roles.Create" },
                new() { Name = "Roles.Edit" },
                new() { Name = "Roles.Delete" },
                new() { Name = "Roles.ManagePermissions" },
        ];
      }

      static async Task SeedRolesAsync(RoleManager<ApplicationRole> roleManager, DatabaseContext context)
      {
            string[] roleNames = [Constants.AdminRole, Constants.ManagerRole, Constants.HrRole, Constants.EmployeeRole, Constants.UserRole];

            foreach (var roleName in roleNames)
            {
                  var roleExist = await roleManager.RoleExistsAsync(roleName);
                  if (!roleExist)
                  {
                        var role = new ApplicationRole
                        {
                              Name = roleName,
                              NormalizedName = roleName.ToLower(),
                        };

                        await roleManager.CreateAsync(role);

                        await AddPermissionsToRole(role, roleName, context); //بعد ما أنشئت الدور هانسبله الصلاحيات
                  }
            }
      }

      static async Task AddPermissionsToRole(ApplicationRole role, string roleName, DatabaseContext context)
      {
            var allPermissions = GetAllPermissions();
            List<string> permissionNames = roleName switch
            {
                  Constants.AdminRole => [.. allPermissions.Select(p => p.Name)],
                  Constants.ManagerRole => [.. allPermissions
                      .Where(p => p.Name.StartsWith("Employees") ||
                                 p.Name.StartsWith("Departments") ||
                                 p.Name.StartsWith("Leaves") ||
                                 p.Name.StartsWith("Attendance") ||
                                 p.Name.StartsWith("Payroll"))
                      .Select(p => p.Name)],
                  Constants.HrRole => [.. allPermissions
                      .Where(p => p.Name.StartsWith("Employees") ||
                                 p.Name.StartsWith("Leaves") ||
                                 p.Name.StartsWith("Payroll") ||
                                 p.Name.StartsWith("Adjustments"))
                      .Select(p => p.Name)],
                  Constants.EmployeeRole => [.. allPermissions
                      .Where(p => p.Name.EndsWith(".View") || p.Name.StartsWith("Attendance"))
                      .Select(p => p.Name)],
                  Constants.UserRole => [.. allPermissions
                      .Where(p => p.Name.EndsWith(".View"))
                      .Select(p => p.Name)],
                  _ => []
            };

            // من قاعدة البيانات
            var permissions = await context.Permissions
                .Where(p => permissionNames.Contains(p.Name))
                .ToListAsync();

            // إنشاء RolePermissions
            var rolePermissions = permissions.Select(p => new RolePermission
            {
                  RoleId = role.Id,
                  PermissionId = p.Id
            }).ToList();

            // إضافة RolePermissions للسياق
            await context.RolePermissions.AddRangeAsync(rolePermissions);
            await context.SaveChangesAsync();
      }

      static async Task SeedAdminUserAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
      {
            var adminUser = await userManager.FindByEmailAsync(Constants.AdminEmail);

            if (adminUser == null)
            {
                  adminUser = new ApplicationUser
                  {
                        UserName = Constants.AdminEmail,
                        Email = Constants.AdminEmail,
                        FullName = "Admin Full Name",
                        EmailConfirmed = true,
                        PhoneNumber = "+1234567890",
                        IsActive = true,
                  };

                  var result = await userManager.CreateAsync(adminUser, Constants.AdminPassword);

                  if (result.Succeeded)
                  {
                        if (await roleManager.RoleExistsAsync(Constants.AdminRole))
                              await userManager.AddToRoleAsync(adminUser, Constants.AdminRole);
                        await userManager.UpdateAsync(adminUser);
                  }
            }
      }

}
