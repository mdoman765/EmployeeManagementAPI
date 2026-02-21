namespace EmployeeManagementAPI.Models;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public int DepartmentId { get; set; }
    public int? DesignationId { get; set; }
    public string? AccountNumber { get; set; }
    public DateTime AddedDate { get; set; } = DateTime.UtcNow;
    public int? AddedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public bool IsActive { get; set; } = true;
    public Department Department { get; set; } = null!;
    public ICollection<Salary> Salaries { get; set; } = new List<Salary>();
    public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
}
