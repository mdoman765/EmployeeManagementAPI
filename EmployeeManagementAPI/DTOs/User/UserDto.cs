namespace EmployeeManagementAPI.DTOs.User;

public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int RoleId { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime AddedDate { get; set; }
    public DateTime? LastLoginDate { get; set; }
}

public class CreateUserDto
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int RoleId { get; set; }
}

public class UpdateUserDto
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public int? RoleId { get; set; }
    public bool? IsActive { get; set; }
}
