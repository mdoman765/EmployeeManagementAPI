using EmployeeManagementAPI.DTOs.Auth;

namespace EmployeeManagementAPI.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto?> LoginAsync(LoginDto dto);
}
