using EmployeeManagementAPI.DTOs.Employee;

namespace EmployeeManagementAPI.Services.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDto>> GetAllAsync();
    Task<EmployeeDto?> GetByIdAsync(int id);
    Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto, int addedBy);
    Task<EmployeeDto?> UpdateAsync(int id, CreateEmployeeDto dto);
    Task<bool> DeleteAsync(int id);
}
