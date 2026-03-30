using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hr.Core.Entities;

public class Payroll
{
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int Id { get; set; }

      public int Month { get; set; }
      
      public int Year { get; set; }

      public int TotalWorkingDays { get; set; }
      
      public int TotalAbsentDays { get; set; }
      
      public decimal TotalAllowance { get; set; }
      
      public decimal TotalDeductions { get; set; }
      
      public decimal TotalBonuses { get; set; }
      
      public decimal BaseSalary { get; set; }
      
      public decimal NetSalary { get; set; }

      //foreign keys
      public int EmployeeId { get; set; }
      public Employee? Employee { get; set; }
}
