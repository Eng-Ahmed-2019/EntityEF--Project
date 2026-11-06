using EntityFC.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityFC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }


        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<EmployeeProfile> EmployeeProfiles => Set<EmployeeProfile>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Student> Students => Set<Student>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Department>(e =>
            {
                e.ToTable("Departments");

                e.HasIndex(e => e.Name);

                e.Property(e => e.Id)
                .UseIdentityColumn(seed: 1, increment: 1);
            });

            modelBuilder.Entity<Employee>(e =>
            {
                e.ToTable("Employees");

                //e.HasKey(e => e.Id);

                e.Property(e => e.Id)
                .UseIdentityColumn(seed: 1, increment: 1);

                e.Property(e => e.Age)
                .IsRequired();

                e.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

                e.Property(e => e.Salary)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

                e.HasIndex(e => e.Email)
                .IsUnique();

                e.HasOne(e => e.Department)
                .WithMany(e => e.Employees)
                .HasForeignKey(e => e.DepartmentId)
                //.IsRequired(false)
                //.IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(e => e.EmployeeProfile)
                .WithOne(e => e.Employee)
                .HasForeignKey<EmployeeProfile>(e => e.EmployeeId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

                e.HasMany(e => e.Projects)
                .WithMany(e => e.Employees);
            });

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Department)
                .WithMany(d => d.Students)
                .HasForeignKey(s => s.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}