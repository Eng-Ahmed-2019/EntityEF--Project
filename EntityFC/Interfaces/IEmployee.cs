using EntityFC.Entities;

namespace EntityFC.Interfaces
{
    public interface IEmployee
    {
        Task<bool> DeleteAsync(int id);
        Task<List<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int id);
        Task<int> CreateAsync(Employee employee);
        Task<bool> UpdateSalary(int id, decimal salary);
    }
}