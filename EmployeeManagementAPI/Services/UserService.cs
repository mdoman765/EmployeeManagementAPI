using AutoMapper;
using EmployeeManagementAPI.DTOs.User;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repositories.Interfaces;
using EmployeeManagementAPI.Services.Interfaces;

namespace EmployeeManagementAPI.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repo;
    private readonly IMapper _map;

    public UserService(IUserRepository repo, IMapper map) { _repo = repo; _map = map; }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
        => _map.Map<IEnumerable<UserDto>>(await _repo.GetAllAsync());

    public async Task<UserDto?> GetByIdAsync(int id)
    {
        var u = await _repo.GetByIdAsync(id);
        return u == null ? null : _map.Map<UserDto>(u);
    }

    public async Task<UserDto> CreateAsync(CreateUserDto dto, int addedBy)
    {
        var u = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            RoleId = dto.RoleId,
            AddedBy = addedBy
        };
        return _map.Map<UserDto>(await _repo.CreateAsync(u));
    }

    public async Task<UserDto?> UpdateAsync(int id, UpdateUserDto dto)
    {
        var u = await _repo.GetByIdAsync(id);
        if (u == null) return null;

        if (dto.Username != null) u.Username = dto.Username;
        if (dto.Email != null) u.Email = dto.Email;
        if (dto.RoleId.HasValue) u.RoleId = dto.RoleId.Value;
        if (dto.IsActive.HasValue) u.IsActive = dto.IsActive.Value;

        return _map.Map<UserDto>(await _repo.UpdateAsync(u));
    }

    public async Task<bool> DeleteAsync(int id) => await _repo.SoftDeleteAsync(id);
}
