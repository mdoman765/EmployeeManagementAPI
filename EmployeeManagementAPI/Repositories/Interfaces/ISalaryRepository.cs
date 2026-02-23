using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Repositories.Interfaces;

public interface ISalaryRepository
{
    Task<IEnumerable<Salary>> GetAllAsync();
    Task<IEnumerable<Salary>> GetByEmployeeAsync(int employeeId);
    Task<Salary?> GetByIdAsync(int id);
    Task<Salary> CreateAsync(Salary s);
}
