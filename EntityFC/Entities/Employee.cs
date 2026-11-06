using System.ComponentModel.DataAnnotations;

namespace EntityFC.Entities
{
    //[Table("Employees")]
    //[Index(nameof(Email), IsUnique = true)]
    public class Employee
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //[Required]
        [Display(Name = "Age")]
        public int Age { get; set; }

        //[Required]
        //[MaxLength(100)]
        [Display(Name = "UserName")]
        //[StringLength(100)]
        //[Column("user_name")] // for change column in db...
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress]
        [MaxLength(500)]
        public string Email { get; set; } = null!;

        // Navigation Prpperty one-to-one
        // optional
        [Required]
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }

        /*
        // Navigation Prpperty one-to-one
        // not optional
        [Required]
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;
        */

        //[Required]
        [Range(0, 999999)]
        [Display(Name = "Salary")]
        //[Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; } = decimal.Zero;

        public EmployeeProfile EmployeeProfile { get; set; } = null!; // ont to one

        public ICollection<Project> Projects = new List<Project>();
    }
}