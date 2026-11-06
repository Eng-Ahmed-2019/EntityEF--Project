namespace EntityFC.Entities
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;


        public ICollection<Employee> Employees = new List<Employee>();
    }
}