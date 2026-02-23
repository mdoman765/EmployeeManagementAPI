using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Repositories.Interfaces;

public interface IAttendanceRepository
{
    Task<IEnumerable<Attendance>> GetAllAsync();
    Task<IEnumerable<Attendance>> GetByEmployeeAsync(int employeeId);
    Task<Attendance?> GetByIdAsync(int id);
    Task<Attendance> CreateAsync(Attendance a);
    Task<Attendance> UpdateAsync(Attendance a);
    Task<bool> SoftDeleteAsync(int id);
}
