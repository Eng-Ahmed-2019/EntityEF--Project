using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFC.Entities
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;



        // Navigation Property many-to-one
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}