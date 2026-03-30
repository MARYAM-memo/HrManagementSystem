using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hr.Core.Entities;

public enum AdjustmentType { Bonus, Deduction }

public class Adjustment
{
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int Id { get; set; }

      public decimal Amount { get; set; }

      public DateOnly Date { get; set; }

      public string? Reason { get; set; }

      public AdjustmentType Type { get; set; }

      //foreign keys
      public int EmployeeId { get; set; }
      public Employee? Employee { get; set; }

      public int? PayrollId { get; set; }
      public Payroll? Payroll { get; set; }
}
