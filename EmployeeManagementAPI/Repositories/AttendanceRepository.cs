using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Repositories;

public class AttendanceRepository : IAttendanceRepository
{
    private readonly AppDbContext _ctx;
    public AttendanceRepository(AppDbContext ctx) => _ctx = ctx;

    public async Task<IEnumerable<Attendance>> GetAllAsync()
        => await _ctx.Attendances.Include(a => a.Employee)
            .Where(a => !a.IsDeleted).OrderByDescending(a => a.CheckInTime).ToListAsync();

    public async Task<IEnumerable<Attendance>> GetByEmployeeAsync(int employeeId)
        => await _ctx.Attendances.Include(a => a.Employee)
            .Where(a => a.EmployeeId == employeeId && !a.IsDeleted)
            .OrderByDescending(a => a.CheckInTime).ToListAsync();

    public async Task<Attendance?> GetByIdAsync(int id)
        => await _ctx.Attendances.Include(a => a.Employee)
            .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);

    public async Task<Attendance> CreateAsync(Attendance a)
    {
        _ctx.Attendances.Add(a);
        await _ctx.SaveChangesAsync();
        return a;
    }

    public async Task<Attendance> UpdateAsync(Attendance a)
    {
        a.UpdatedAt = DateTime.UtcNow;
        _ctx.Attendances.Update(a);
        await _ctx.SaveChangesAsync();
        return a;
    }

    public async Task<bool> SoftDeleteAsync(int id)
    {
        var a = await _ctx.Attendances.FindAsync(id);
        if (a == null) return false;
        a.IsDeleted = true;
        a.UpdatedAt = DateTime.UtcNow;
        await _ctx.SaveChangesAsync();
        return true;
    }
}
