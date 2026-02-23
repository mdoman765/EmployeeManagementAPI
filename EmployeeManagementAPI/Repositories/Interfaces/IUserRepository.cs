using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Repositories.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task<User> CreateAsync(User u);
    Task<User> UpdateAsync(User u);
    Task<bool> SoftDeleteAsync(int id);
}
