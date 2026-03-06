using EmployeeManagementAPI.DTOs.UpdateProfile;
using EmployeeManagementAPI.DTOs.User;

namespace EmployeeManagementAPI.Services.Interfaces
{
    public interface IUpdateProfileService
    {
    
       Task<UserDto> GetProfileAsync(int userId);
       Task<(bool Ok, string? Error, UserDto? Data)> UpdateProfileAsync(int userId, UpdateProfileDto dto);
        
    }
}
