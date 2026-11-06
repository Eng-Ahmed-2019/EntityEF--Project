using EntityFC.Data;
using EntityFC.Entities;
using EntityFC.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EntityFC.Services
{
    public class EmployeeServices : IEmployee
    {
        private readonly AppDbContext _dbContext;

        public EmployeeServices(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _dbContext.Employees
                .AsNoTracking()
                .OrderBy(e => e.Name)
                .ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _dbContext.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<int> CreateAsync(Employee employee)
        {
            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
            return employee.Id;
        }

        public async Task<bool> UpdateSalary(int id, decimal salary)
        {
            var emp = await _dbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (emp is null) return false;

            emp.Salary = salary;
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var emp = await _dbContext.Employees
                .FirstOrDefaultAsync(e => e.Id == id);
            if (emp is null) return false;

            _dbContext.Employees.Remove(emp);

            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}