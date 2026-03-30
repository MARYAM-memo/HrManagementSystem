using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hr.Core.Entities;

public enum AttendanceStatus { Present, Absent, Late }

public class Attendance
{
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int Id { get; set; }

      public DateOnly Date { get; set; }

      public TimeOnly CheckInTime { get; set; }

      public TimeOnly CheckOutTime { get; set; }

      public TimeSpan WorkingHours => CheckOutTime - CheckInTime;

      public decimal OverTimeHours { get; set; }

      public AttendanceStatus Status { get; set; }
      
      //foreign keys
      public int EmployeeId { get; set; }
      public Employee? Employee { get; set; }
}

