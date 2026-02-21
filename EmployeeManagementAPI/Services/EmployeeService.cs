using AutoMapper;
using EmployeeManagementAPI.DTOs.Employee;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repositories.Interfaces;
using EmployeeManagementAPI.Services.Interfaces;

namespace EmployeeManagementAPI.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repo;
    private readonly IMapper _mapper;

    public EmployeeService(IEmployeeRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
    {
        var employees = await _repo.GetAllAsync();
        return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
    }

    public async Task<EmployeeDto?> GetByIdAsync(int id)
    {
        var emp = await _repo.GetByIdAsync(id);
        return emp == null ? null : _mapper.Map<EmployeeDto>(emp);
    }

    public async Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto, int addedBy)
    {
        var employee = _mapper.Map<Employee>(dto);
        employee.AddedBy = addedBy;
        var created = await _repo.CreateAsync(employee);
        return _mapper.Map<EmployeeDto>(created);
    }

    public async Task<EmployeeDto?> UpdateAsync(int id, CreateEmployeeDto dto)
    {
        var emp = await _repo.GetByIdAsync(id);
        if (emp == null) return null;
        _mapper.Map(dto, emp);
        var updated = await _repo.UpdateAsync(emp);
        return _mapper.Map<EmployeeDto>(updated);
    }

    public async Task<bool> DeleteAsync(int id) => await _repo.SoftDeleteAsync(id);
}
