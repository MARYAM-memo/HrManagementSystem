using Hr.Core.Entities;

namespace Hr.Application.interfaces;

public interface IUnitOfWork : IDisposable
{
      IRepository<Employee> Employees { get;}
      IRepository<Department> Departments { get;}
      IRepository<JobTitle> JobTitles { get;}
      IRepository<Attendance> Attendances { get;}
      IRepository<Adjustment> Adjustments { get;}
      IRepository<Leave> Leaves { get;}
      IRepository<Payroll> Payrolls { get;}
      IRepository<Permission> Permissions { get;}
      IRepository<RolePermission> RolePermissions { get;}
      Task<int> SaveChangesAsync();
}
