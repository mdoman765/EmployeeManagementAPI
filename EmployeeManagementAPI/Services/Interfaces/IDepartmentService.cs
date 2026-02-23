using EmployeeManagementAPI.DTOs.Department;

namespace EmployeeManagementAPI.Services.Interfaces;

public interface IDepartmentService
{
    Task<IEnumerable<DepartmentDto>> GetAllAsync();
    Task<DepartmentDto?> GetByIdAsync(int id);
    Task<DepartmentDto> CreateAsync(CreateDepartmentDto dto, int createdBy);
    Task<DepartmentDto?> UpdateAsync(int id, CreateDepartmentDto dto);
    Task<bool> DeleteAsync(int id);
}
