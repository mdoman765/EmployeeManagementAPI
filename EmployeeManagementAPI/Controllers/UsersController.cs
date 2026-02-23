using System.Security.Claims;
using EmployeeManagementAPI.DTOs.User;
using EmployeeManagementAPI.Helpers;
using EmployeeManagementAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class UsersController : ControllerBase
{
    private readonly IUserService _svc;
    public UsersController(IUserService svc) => _svc = svc;

    /// <summary>GET api/users - Get all users (Admin only)</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(ApiResponse<IEnumerable<UserDto>>.Ok(await _svc.GetAllAsync()));

    /// <summary>GET api/users/{id} - Get user by id (Admin only)</summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var r = await _svc.GetByIdAsync(id);
        return r == null
            ? NotFound(ApiResponse<string>.Fail("User not found"))
            : Ok(ApiResponse<UserDto>.Ok(r));
    }

    /// <summary>POST api/users - Create new user (Admin only)</summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
    {
        var uid = int.Parse(User.FindFirstValue("UserId")!);
        var r = await _svc.CreateAsync(dto, uid);
        return CreatedAtAction(nameof(GetById), new { id = r.Id },
            ApiResponse<UserDto>.Ok(r, "User created"));
    }

    /// <summary>PUT api/users/{id} - Update user (Admin only). Can toggle IsActive status.</summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto dto)
    {
        var r = await _svc.UpdateAsync(id, dto);
        return r == null
            ? NotFound(ApiResponse<string>.Fail("User not found"))
            : Ok(ApiResponse<UserDto>.Ok(r, "User updated"));
    }

    /// <summary>DELETE api/users/{id} - Soft delete user (Admin only)</summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _svc.DeleteAsync(id);
        return ok
            ? Ok(ApiResponse<string>.Ok("Deleted", "User deleted"))
            : NotFound(ApiResponse<string>.Fail("User not found"));
    }
}
