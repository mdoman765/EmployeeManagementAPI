using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly AppDbContext _ctx;
    public DepartmentRepository(AppDbContext ctx) => _ctx = ctx;

    public async Task<IEnumerable<Department>> GetAllAsync()
        => await _ctx.Departments.Where(d => d.IsActive).ToListAsync();

    public async Task<Department?> GetByIdAsync(int id)
        => await _ctx.Departments.FirstOrDefaultAsync(d => d.Id == id && d.IsActive);

    public async Task<Department> CreateAsync(Department d)
    {
        _ctx.Departments.Add(d);
        await _ctx.SaveChangesAsync();
        return d;
    }

    public async Task<Department> UpdateAsync(Department d)
    {
        d.UpdatedAt = DateTime.UtcNow;
        _ctx.Departments.Update(d);
        await _ctx.SaveChangesAsync();
        return d;
    }

    public async Task<bool> SoftDeleteAsync(int id)
    {
        var d = await _ctx.Departments.FindAsync(id);
        if (d == null) return false;
        d.IsActive = false;
        d.UpdatedAt = DateTime.UtcNow;
        await _ctx.SaveChangesAsync();
        return true;
    }
}
