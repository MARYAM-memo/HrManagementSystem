using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hr.Core.Entities;

public class Department
{
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int Id { get; set; }

      public required string Name { get; set; }

      public required string Location { get; set; } //floor2 ..etc

      //foreign keys
      public int? ManagerId { get; set; }
      public Employee? Manager { get; set; }

      //navigation عكسية
      public ICollection<Employee> Employees { get; set; } = [];
}
