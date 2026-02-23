using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _ctx;
    public UserRepository(AppDbContext ctx) => _ctx = ctx;

    public async Task<IEnumerable<User>> GetAllAsync()
        => await _ctx.Users.Include(u => u.Role).ToListAsync();

    public async Task<User?> GetByIdAsync(int id)
        => await _ctx.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);

    public async Task<User> CreateAsync(User u)
    {
        _ctx.Users.Add(u);
        await _ctx.SaveChangesAsync();
        return u;
    }

    public async Task<User> UpdateAsync(User u)
    {
        u.UpdatedDate = DateTime.UtcNow;
        _ctx.Users.Update(u);
        await _ctx.SaveChangesAsync();
        return u;
    }

    public async Task<bool> SoftDeleteAsync(int id)
    {
        var u = await _ctx.Users.FindAsync(id);
        if (u == null) return false;
        u.IsActive = false;
        u.UpdatedDate = DateTime.UtcNow;
        await _ctx.SaveChangesAsync();
        return true;
    }
}
