using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Repositories.Interfaces;

public interface IDepartmentRepository
{
    Task<IEnumerable<Department>> GetAllAsync();
    Task<Department?> GetByIdAsync(int id);
    Task<Department> CreateAsync(Department d);
    Task<Department> UpdateAsync(Department d);
    Task<bool> SoftDeleteAsync(int id);
}
