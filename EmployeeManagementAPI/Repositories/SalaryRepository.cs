using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Repositories;

public class SalaryRepository : ISalaryRepository
{
    private readonly AppDbContext _ctx;
    public SalaryRepository(AppDbContext ctx) => _ctx = ctx;

    public async Task<IEnumerable<Salary>> GetAllAsync()
        => await _ctx.Salaries.Include(s => s.Employee)
            .OrderByDescending(s => s.CreatedAt).ToListAsync();

    public async Task<IEnumerable<Salary>> GetByEmployeeAsync(int employeeId)
        => await _ctx.Salaries.Include(s => s.Employee)
            .Where(s => s.EmployeeId == employeeId)
            .OrderByDescending(s => s.EffectiveFrom).ToListAsync();

    public async Task<Salary?> GetByIdAsync(int id)
        => await _ctx.Salaries.Include(s => s.Employee).FirstOrDefaultAsync(s => s.Id == id);

    public async Task<Salary> CreateAsync(Salary s)
    {
        // Mark previous salaries for this employee as not current
        var prev = await _ctx.Salaries.Where(x => x.EmployeeId == s.EmployeeId && x.IsCurrent).ToListAsync();
        foreach (var p in prev)
        {
            p.IsCurrent = false;
            p.EffectiveTo = s.EffectiveFrom.AddDays(-1);
            p.UpdatedAt = DateTime.UtcNow;
        }
        _ctx.Salaries.Add(s);
        await _ctx.SaveChangesAsync();
        return s;
    }
}
