namespace EntityFC.Entities
{
    public class EmployeeProfile
    {
        public int Id { get; set; }

        public string Address { get; set; } = null!;

        public string Nationality { get; set; } = null!;

        public DateTime BirthDate { get; set; }


        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;
    }
}