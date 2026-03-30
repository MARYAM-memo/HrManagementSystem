using System;
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

            // Seed Roles
            await SeedRolesAsync(roleManager);

            // Seed Admin User
            await SeedAdminUserAsync(userManager, roleManager);
      }

      static async Task SeedRolesAsync(RoleManager<ApplicationRole> roleManager)
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

                        // Set permissions based on role
                        SetRolePermissions(role, roleName);

                        await roleManager.CreateAsync(role);
                  }
            }
      }

      static void SetRolePermissions(ApplicationRole role, string roleName)
      {     // Const Module: [Module].[Action]
            var allPermissions = new List<Permission>
            {
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
            };
            List<Permission> rolePermissions = roleName switch
            {
                  Constants.AdminRole => allPermissions,
                  Constants.ManagerRole => [.. allPermissions
                      .Where(p => p.Name.StartsWith("Employees") ||
                                  p.Name.StartsWith("Departments") ||
                                  p.Name.StartsWith("Leaves") ||
                                  p.Name.StartsWith("Attendance") ||
                                  p.Name.StartsWith("Payroll"))],
                  Constants.HrRole => [.. allPermissions
                      .Where(p => p.Name.StartsWith("Employees") ||
                                  p.Name.StartsWith("Leaves") ||
                                  p.Name.StartsWith("Payroll") ||
                                  p.Name.StartsWith("Adjustments"))],
                  Constants.EmployeeRole => [.. allPermissions.Where(p => p.Name.EndsWith(".View") || p.Name.StartsWith("Attendance"))],
                  Constants.UserRole => [.. allPermissions.Where(p => p.Name.EndsWith(".View"))],
                  _ => []
            };

            role.RolePermissions = [.. rolePermissions
            .Select(p => new RolePermission
            {
                  Permission = p,
                  Role = role
            })];

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
