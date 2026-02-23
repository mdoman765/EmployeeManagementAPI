using EmployeeManagementAPI.DTOs.Attendance;

namespace EmployeeManagementAPI.Services.Interfaces;

public interface IAttendanceService
{
    Task<IEnumerable<AttendanceDto>> GetAllAsync();
    Task<IEnumerable<AttendanceDto>> GetByEmployeeAsync(int employeeId);
    Task<AttendanceDto?> GetByIdAsync(int id);
    Task<AttendanceDto> CreateAsync(CreateAttendanceDto dto, int createdBy);
    Task<AttendanceDto?> UpdateAsync(int id, CreateAttendanceDto dto, int updatedBy);
    Task<bool> DeleteAsync(int id);
}
