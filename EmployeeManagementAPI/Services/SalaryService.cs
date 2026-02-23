using AutoMapper;
using EmployeeManagementAPI.DTOs.Salary;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repositories.Interfaces;
using EmployeeManagementAPI.Services.Interfaces;

namespace EmployeeManagementAPI.Services;

public class SalaryService : ISalaryService
{
    private readonly ISalaryRepository _repo;
    private readonly IMapper _map;

    public SalaryService(ISalaryRepository repo, IMapper map) { _repo = repo; _map = map; }

    public async Task<IEnumerable<SalaryDto>> GetAllAsync()
        => _map.Map<IEnumerable<SalaryDto>>(await _repo.GetAllAsync());

    public async Task<IEnumerable<SalaryDto>> GetByEmployeeAsync(int employeeId)
        => _map.Map<IEnumerable<SalaryDto>>(await _repo.GetByEmployeeAsync(employeeId));

    public async Task<SalaryDto> CreateAsync(CreateSalaryDto dto, int createdBy)
    {
        var s = _map.Map<Salary>(dto);
        s.CreatedBy = createdBy;
        return _map.Map<SalaryDto>(await _repo.CreateAsync(s));
    }
}
