using Hr.Application.interfaces;
using Hr.Core.Entities;
using Hr.Infrastructure.Data;

namespace Hr.Infrastructure.DataAccess;

public class UnitOfWork : IUnitOfWork
{
      readonly DatabaseContext context;

      public UnitOfWork(DatabaseContext ctx)
      {
            context = ctx;
            Employees = new Repository<Employee>(context);
            Departments = new Repository<Department>(context);
            JobTitles = new Repository<JobTitle>(context);
            Attendances = new Repository<Attendance>(context);
            Adjustments = new Repository<Adjustment>(context);
            Leaves = new Repository<Leave>(context);
            Payrolls = new Repository<Payroll>(context);
            Permissions = new Repository<Permission>(context);
            RolePermissions = new Repository<RolePermission>(context);
      }

      public IRepository<Employee> Employees { get; private set; }

      public IRepository<Department> Departments { get; }

      public IRepository<JobTitle> JobTitles { get; }

      public IRepository<Attendance> Attendances { get; }

      public IRepository<Adjustment> Adjustments { get; }

      public IRepository<Leave> Leaves { get; }

      public IRepository<Payroll> Payrolls { get; }

      public IRepository<Permission> Permissions { get; }

      public IRepository<RolePermission> RolePermissions { get; }

      public async Task<int> SaveChangesAsync()
      {
            return await context.SaveChangesAsync();
      }

      public void Dispose()
      {
            context.Dispose();
            GC.SuppressFinalize(this);
      }
}
