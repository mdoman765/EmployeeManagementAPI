using EmployeeManagementAPI.DTOs.User;

namespace EmployeeManagementAPI.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task<UserDto?> GetByIdAsync(int id);
    Task<UserDto> CreateAsync(CreateUserDto dto, int addedBy);
    Task<UserDto?> UpdateAsync(int id, UpdateUserDto dto);
    Task<bool> DeleteAsync(int id);
}
