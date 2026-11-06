namespace EntityFC.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Balance { get; set; }

        public string? Email { get; set; }
    }
}