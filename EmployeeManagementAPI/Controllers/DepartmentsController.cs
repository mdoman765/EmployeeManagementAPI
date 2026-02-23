using System.Security.Claims;
using EmployeeManagementAPI.DTOs.Department;
using EmployeeManagementAPI.Helpers;
using EmployeeManagementAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DepartmentsController : ControllerBase
{
    private readonly IDepartmentService _svc;
    public DepartmentsController(IDepartmentService svc) => _svc = svc;

    /// <summary>GET api/departments - Get all active departments</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(ApiResponse<IEnumerable<DepartmentDto>>.Ok(await _svc.GetAllAsync()));

    /// <summary>GET api/departments/{id} - Get department by id</summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var r = await _svc.GetByIdAsync(id);
        return r == null
            ? NotFound(ApiResponse<string>.Fail("Department not found"))
            : Ok(ApiResponse<DepartmentDto>.Ok(r));
    }

    /// <summary>POST api/departments - Create department (Admin only)</summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateDepartmentDto dto)
    {
        var uid = int.Parse(User.FindFirstValue("UserId")!);
        var r = await _svc.CreateAsync(dto, uid);
        return CreatedAtAction(nameof(GetById), new { id = r.Id },
            ApiResponse<DepartmentDto>.Ok(r, "Department created"));
    }

    /// <summary>PUT api/departments/{id} - Update department (Admin only)</summary>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateDepartmentDto dto)
    {
        var r = await _svc.UpdateAsync(id, dto);
        return r == null
            ? NotFound(ApiResponse<string>.Fail("Department not found"))
            : Ok(ApiResponse<DepartmentDto>.Ok(r, "Department updated"));
    }

    /// <summary>DELETE api/departments/{id} - Soft delete department (Admin only)</summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _svc.DeleteAsync(id);
        return ok
            ? Ok(ApiResponse<string>.Ok("Deleted", "Department deleted"))
            : NotFound(ApiResponse<string>.Fail("Department not found"));
    }
}
