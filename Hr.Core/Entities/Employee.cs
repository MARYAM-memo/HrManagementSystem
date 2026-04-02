using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hr.Core.Entities;

public class Employee
{
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int Id { get; set; }

      public required string FullName { get; set; }

      public string? Email { get; set; }

      public string? Phone { get; set; }

      public DateOnly HireDate { get; set; }

      public DateOnly BirthDate { get; set; }

      public bool IsActive { get; set; }


      //foreign keys
      public int DepartmentId { get; set; }
      public Department? Department { get; set; }

      public int JobTitleId { get; set; }
      public JobTitle? JobTitle { get; set; }

      public int? ManagerId { get; set; } //nullable
      public Employee? Manager { get; set; }

      //navigation عكسية
      public ICollection<Employee> Subordinates { get; set; } = [];
      public ICollection<Attendance> Attendances { get; set; } = [];
      public ICollection<Department> ManagedDepartments { get; set; } = [];
      public ICollection<Leave> Leaves { get; set; } = [];
      public ICollection<Payroll> Payrolls { get; set; } = [];
      public ICollection<Adjustment> Adjustments { get; set; } = [];

}
