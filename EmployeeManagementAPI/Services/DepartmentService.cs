using AutoMapper;
using EmployeeManagementAPI.DTOs.Department;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repositories.Interfaces;
using EmployeeManagementAPI.Services.Interfaces;

namespace EmployeeManagementAPI.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _repo;
    private readonly IMapper _map;

    public DepartmentService(IDepartmentRepository repo, IMapper map) { _repo = repo; _map = map; }

    public async Task<IEnumerable<DepartmentDto>> GetAllAsync()
        => _map.Map<IEnumerable<DepartmentDto>>(await _repo.GetAllAsync());

    public async Task<DepartmentDto?> GetByIdAsync(int id)
    {
        var d = await _repo.GetByIdAsync(id);
        return d == null ? null : _map.Map<DepartmentDto>(d);
    }

    public async Task<DepartmentDto> CreateAsync(CreateDepartmentDto dto, int createdBy)
    {
        var d = _map.Map<Department>(dto);
        d.CreatedBy = createdBy;
        return _map.Map<DepartmentDto>(await _repo.CreateAsync(d));
    }

    public async Task<DepartmentDto?> UpdateAsync(int id, CreateDepartmentDto dto)
    {
        var d = await _repo.GetByIdAsync(id);
        if (d == null) return null;
        _map.Map(dto, d);
        return _map.Map<DepartmentDto>(await _repo.UpdateAsync(d));
    }

    public async Task<bool> DeleteAsync(int id) => await _repo.SoftDeleteAsync(id);
}
