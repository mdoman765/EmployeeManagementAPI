using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Repositories.Interfaces
{
    public class UpdateProfileRepository : IUpdateProfileRepository
    {
        private readonly AppDbContext _db;

        public UpdateProfileRepository(AppDbContext db) => _db = db;

        public Task<User?> GetUserWithEmployeeAsync(int userId)
       => _db.Users
             .Include(u => u.Role)
             .Include(u => u.Employee)       // navigation property
             .FirstOrDefaultAsync(u => u.Id == userId);

        public Task<User?> GetByIdAsync(int userId)
            => _db.Users
                  .Include(u => u.Role)
                  .FirstOrDefaultAsync(u => u.Id == userId);

        public Task<bool> IsUsernameTakenAsync(string username, int excludeUserId)
      => _db.Users.AnyAsync(u => u.Username == username && u.Id != excludeUserId);

        public Task<bool> IsEmailTakenAsync(string email, int excludeUserId)
            => _db.Users.AnyAsync(u => u.Email == email && u.Id != excludeUserId);

        public async Task<User> UpdateUserAsync(User user)
        {
            user.UpdatedDate = DateTime.UtcNow;
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            employee.UpdatedDate = DateTime.UtcNow;
            _db.Employees.Update(employee);
            await _db.SaveChangesAsync();
            return employee;
        }
    }

}
