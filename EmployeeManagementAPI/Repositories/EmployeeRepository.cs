using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _context;
    public EmployeeRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Employee>> GetAllAsync()
        => await _context.Employees
            .Include(e => e.Department)
            .Where(e => e.IsActive)
            .ToListAsync();

    public async Task<Employee?> GetByIdAsync(int id)
        => await _context.Employees
            .Include(e => e.Department)
            .FirstOrDefaultAsync(e => e.Id == id && e.IsActive);

    public async Task<Employee> CreateAsync(Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        return employee;
    }

    public async Task<Employee> UpdateAsync(Employee employee)
    {
        employee.UpdatedDate = DateTime.UtcNow;
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
        return employee;
    }

    public async Task<bool> SoftDeleteAsync(int id)
    {
        var emp = await _context.Employees.FindAsync(id);
        if (emp == null) return false;
        emp.IsActive = false;
        emp.UpdatedDate = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }
}
