namespace EmployeeManagementAPI.DTOs.UpdateProfile
{
    public class UpdateProfileDto
    {
        // User table fields
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Password { get; set; }

        // Employee table fields
        public string Name { get; set; } = string.Empty;
        public string? Phone { get; set; }
    }
}
