namespace EmployeeManagementAPI.DTOs.Department;

public class DepartmentDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}

public class CreateDepartmentDto
{
    public string Name { get; set; } = string.Empty;
}
