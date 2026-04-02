using Hr.Core.Entities;
using Hr.Infrastructure.Configurations;
using Hr.Infrastructure.Data.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hr.Infrastructure.Data;

public class DatabaseContext(DbContextOptions<DatabaseContext> options)
      : IdentityDbContext<ApplicationUser, ApplicationRole, int, IdentityUserClaim<int>,
            ApplicationUserRole, IdentityUserLogin<int>,
            IdentityRoleClaim<int>, IdentityUserToken<int>>(options)
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<JobTitle> JobTitles { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<Adjustment> Adjustments { get; set; }
    public DbSet<Leave> Leaves { get; set; }
    public DbSet<Payroll> Payrolls { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new EmployeeConfiguration());
        builder.ApplyConfiguration(new DepartmentConfiguration());
        builder.ApplyConfiguration(new LeaveConfiguration());
        builder.ApplyConfiguration(new AdjustmentConfiguration());
        builder.ApplyConfiguration(new PayrollConfiguration());
        builder.ApplyConfiguration(new AttendanceConfiguration());
        builder.ApplyConfiguration(new ApplicationUserConfiguration());
        builder.ApplyConfiguration(new ApplicationUserRoleConfiguration());
        builder.ApplyConfiguration(new RolePermissionConfiguration());

        builder.Entity<Permission>()
            .HasIndex(p => p.Name)
            .IsUnique();
    }
}
