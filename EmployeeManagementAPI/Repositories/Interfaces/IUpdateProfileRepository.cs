using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Repositories.Interfaces
{
    public interface IUpdateProfileRepository
    {
        Task<User?> GetUserWithEmployeeAsync(int userId);
        Task<bool> IsUsernameTakenAsync(string username, int excludeUserId);
        Task<bool> IsEmailTakenAsync(string email, int excludeUserId);
        Task<User> UpdateUserAsync(User user);
        Task<Employee> UpdateEmployeeAsync(Employee employee);
    }
}
