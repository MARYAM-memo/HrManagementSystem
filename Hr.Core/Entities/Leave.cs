using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hr.Core.Entities;

public enum LeaveType { Annual, Sick, Unpaid }

public enum LeaveStatus { Pending, Approved, Rejected }

public class Leave
{
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int Id { get; set; }

      public DateOnly FromDate { get; set; }

      public DateOnly ToDate { get; set; }

      public LeaveType Type { get; set; }
      public LeaveStatus Status { get; set; }

      //foreign keys
      public int EmployeeId { get; set; }
      public Employee? Employee { get; set; }

      public int? ApprovedById { get; set; }
      public Employee? Approver { get; set; }
}
