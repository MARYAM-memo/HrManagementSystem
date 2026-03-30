using Hr.Core.Entities;
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

            //Fluent API
            builder.Entity<Employee>()
            .HasOne(d => d.Manager)
            .WithMany(e => e.Subordinates)
            .HasForeignKey(d => d.ManagerId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Department>()
                .HasOne(d => d.Manager)
                .WithMany(e => e.ManagedDepartments)
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Employee>()
            .HasOne(d => d.Department)
            .WithMany(d => d.Employees)
            .HasForeignKey(d => d.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Employee>()
            .HasOne(d => d.JobTitle)
            .WithMany(d => d.Employees)
            .HasForeignKey(d => d.JobTitleId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Leave>()
            .HasOne(l => l.Approver)
            .WithMany()
            .HasForeignKey(l => l.ApprovedById)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Adjustment>()
                .HasOne(a => a.Payroll)
                .WithMany()
                .HasForeignKey(a => a.PayrollId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Adjustment>()
                .HasOne(a => a.Employee)
                .WithMany(e => e.Adjustments)
                .HasForeignKey(a => a.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Payroll>()
                .HasOne(a => a.Employee)
                .WithMany(e => e.Payrolls)
                .HasForeignKey(a => a.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Leave>()
                .HasOne(a => a.Employee)
                .WithMany(e => e.Leaves)
                .HasForeignKey(a => a.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Attendance>()
                .HasOne(a => a.Employee)
                .WithMany(e => e.Attendances)
                .HasForeignKey(a => a.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Payroll>()
           .HasIndex(p => new { p.EmployeeId, p.Month, p.Year })
           .IsUnique();

            builder.Entity<Payroll>()
           .Property(p=>p.BaseSalary)
           .HasColumnType("numeric(18,2)");
           
            builder.Entity<Payroll>()
           .Property(p=>p.NetSalary)
           .HasColumnType("numeric(18,2)");

            builder.Entity<Attendance>()
            .HasIndex(a => new { a.EmployeeId, a.Date })
            .IsUnique();

            builder.Entity<Attendance>()
           .Property(a => a.Status)
           .HasConversion<string>();

            builder.Entity<Adjustment>()
           .Property(a => a.Type)
           .HasConversion<string>();

            builder.Entity<Leave>()
           .Property(a => a.Status)
           .HasConversion<string>();

            builder.Entity<Leave>()
           .Property(a => a.Type)
           .HasConversion<string>();

            builder.Entity<ApplicationUser>()
           .HasIndex(u => u.EmployeeId)
           .IsUnique();

            builder.Entity<ApplicationUser>()
            .HasOne(u => u.Employee)
            .WithMany()
            .HasForeignKey(u => u.EmployeeId)
            .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Employee>()
            .Property(e => e.HireDate)
            .HasColumnType("date");

            builder.Entity<Employee>()
            .Property(e => e.BirthDate)
            .HasColumnType("date");

            builder.Entity<Adjustment>()
            .Property(e => e.Date)
            .HasColumnType("date");

            builder.Entity<Attendance>()
            .Property(e => e.Date)
            .HasColumnType("date");

            builder.Entity<Leave>()
            .Property(e => e.ToDate)
            .HasColumnType("date");

            builder.Entity<Leave>()
            .Property(e => e.FromDate)
            .HasColumnType("date");

            builder.Entity<Attendance>()
                .Property(a => a.CheckInTime)
                .HasColumnType("time");

            builder.Entity<Attendance>()
                .Property(a => a.CheckOutTime)
                .HasColumnType("time");

            builder.Entity<ApplicationUserRole>()
            .HasOne(ur => ur.User)
            .WithMany()
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();

            builder.Entity<ApplicationUserRole>()
            .HasOne(ur => ur.Role)
            .WithMany()
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired();

            builder.Entity<RolePermission>()
            .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            builder.Entity<RolePermission>()
            .HasOne(rp => rp.Role)
            .WithMany(r => r.RolePermissions)
            .HasForeignKey(rp => rp.RoleId);

            builder.Entity<RolePermission>()
            .HasOne(rp => rp.Permission)
            .WithMany(p => p.RolePermissions)
            .HasForeignKey(rp => rp.PermissionId);

            builder.Entity<Permission>()
            .HasIndex(p => p.Name)
            .IsUnique();

      }
}
