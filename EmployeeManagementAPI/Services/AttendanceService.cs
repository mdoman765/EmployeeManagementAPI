using AutoMapper;
using EmployeeManagementAPI.DTOs.Attendance;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repositories.Interfaces;
using EmployeeManagementAPI.Services.Interfaces;

namespace EmployeeManagementAPI.Services;

public class AttendanceService : IAttendanceService
{
    private readonly IAttendanceRepository _repo;
    private readonly IMapper _map;

    public AttendanceService(IAttendanceRepository repo, IMapper map) { _repo = repo; _map = map; }

    public async Task<IEnumerable<AttendanceDto>> GetAllAsync()
        => _map.Map<IEnumerable<AttendanceDto>>(await _repo.GetAllAsync());

    public async Task<IEnumerable<AttendanceDto>> GetByEmployeeAsync(int employeeId)
        => _map.Map<IEnumerable<AttendanceDto>>(await _repo.GetByEmployeeAsync(employeeId));

    public async Task<AttendanceDto?> GetByIdAsync(int id)
    {
        var a = await _repo.GetByIdAsync(id);
        return a == null ? null : _map.Map<AttendanceDto>(a);
    }

    public async Task<AttendanceDto> CreateAsync(CreateAttendanceDto dto, int createdBy)
    {
        var a = _map.Map<Attendance>(dto);
        a.CreatedBy = createdBy;
        return _map.Map<AttendanceDto>(await _repo.CreateAsync(a));
    }

    public async Task<AttendanceDto?> UpdateAsync(int id, CreateAttendanceDto dto, int updatedBy)
    {
        var a = await _repo.GetByIdAsync(id);
        if (a == null) return null;
        _map.Map(dto, a);
        a.UpdatedBy = updatedBy;
        return _map.Map<AttendanceDto>(await _repo.UpdateAsync(a));
    }

    public async Task<bool> DeleteAsync(int id) => await _repo.SoftDeleteAsync(id);
}
