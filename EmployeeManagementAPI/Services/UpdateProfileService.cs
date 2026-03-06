using EmployeeManagementAPI.DTOs.UpdateProfile;
using EmployeeManagementAPI.DTOs.User;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repositories.Interfaces;
using EmployeeManagementAPI.Services.Interfaces;

namespace EmployeeManagementAPI.Services
{
    public class UpdateProfileService : IUpdateProfileService
    {
        private readonly IUpdateProfileRepository _repo;
        public UpdateProfileService(IUpdateProfileRepository repo) => _repo = repo;

        public async Task<UserDto> GetProfileAsync(int userId)
        {
            var user = await _repo.GetUserWithEmployeeAsync(userId)
                ?? throw new KeyNotFoundException("User not found");

            return MapToDto(user);
        }

        public async Task<(bool Ok, string? Error, UserDto? Data)> UpdateProfileAsync(
            int userId, UpdateProfileDto dto)
        {
            var user = await _repo.GetUserWithEmployeeAsync(userId);
            if (user == null)
                return (false, "User not found", null);

            // Uniqueness checks
            if (await _repo.IsUsernameTakenAsync(dto.Username.Trim(), userId))
                return (false, "Username is already taken", null);

            if (await _repo.IsEmailTakenAsync(dto.Email.Trim(), userId))
                return (false, "Email is already in use", null);

            // ── Update User table ──────────────────────────────
            user.Username = dto.Username.Trim();
            user.Email = dto.Email.Trim();

            if (!string.IsNullOrWhiteSpace(dto.Password))
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            await _repo.UpdateUserAsync(user);

            // ── Update Employee table ──────────────────────────
            if (user.Employee != null)
            {
                user.Employee.Name = dto.Name.Trim();
                user.Employee.Email = dto.Email.Trim();   // keep email in sync
                user.Employee.Phone = dto.Phone?.Trim();

                await _repo.UpdateEmployeeAsync(user.Employee);
            }

            return (true, null, MapToDto(user));
        }

        private static UserDto MapToDto(User u) => new()
        {
            Id = u.Id,
            Username = u.Username,
            Email = u.Email,
            RoleId = u.RoleId,
            RoleName = u.Role?.Name,
            IsActive = u.IsActive,
            EmployeeId = u.EmployeeId,
            Name = u.Employee?.Name,
            Phone = u.Employee?.Phone
        };
    }
}
