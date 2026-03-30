using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hr.Core.Entities;

public class JobTitle
{
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int Id { get; set; }

      public required string Title { get; set; }

      public decimal? Allowance { get; set; }

      //navigation عكسية
      public ICollection<Employee> Employees { get; set; } = [];
}
