namespace EmployeeManagementAPI.DTOs.Salary;

public class SalaryDto
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public decimal BasicSalary { get; set; }
    public decimal Bonus { get; set; }
    public decimal Deduction { get; set; }
    public decimal NetSalary => BasicSalary + Bonus - Deduction;
    public DateOnly EffectiveFrom { get; set; }
    public DateOnly? EffectiveTo { get; set; }
    public bool IsCurrent { get; set; }
}

public class CreateSalaryDto
{
    public int EmployeeId { get; set; }
    public decimal BasicSalary { get; set; }
    public decimal Bonus { get; set; }
    public decimal Deduction { get; set; }
    public DateOnly EffectiveFrom { get; set; }
}
