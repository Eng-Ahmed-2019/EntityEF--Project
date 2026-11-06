namespace EntityFC.Entities
{
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}