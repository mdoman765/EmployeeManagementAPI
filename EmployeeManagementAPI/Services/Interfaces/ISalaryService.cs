using EmployeeManagementAPI.DTOs.Salary;

namespace EmployeeManagementAPI.Services.Interfaces;

public interface ISalaryService
{
    Task<IEnumerable<SalaryDto>> GetAllAsync();
    Task<IEnumerable<SalaryDto>> GetByEmployeeAsync(int employeeId);
    Task<SalaryDto> CreateAsync(CreateSalaryDto dto, int createdBy);
}
