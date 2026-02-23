using System.Security.Claims;
using EmployeeManagementAPI.DTOs.Attendance;
using EmployeeManagementAPI.Helpers;
using EmployeeManagementAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AttendanceController : ControllerBase
{
    private readonly IAttendanceService _svc;
    public AttendanceController(IAttendanceService svc) => _svc = svc;

    /// <summary>GET api/attendance - Get all attendance records</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(ApiResponse<IEnumerable<AttendanceDto>>.Ok(await _svc.GetAllAsync()));

    /// <summary>GET api/attendance/{id} - Get single attendance record</summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var r = await _svc.GetByIdAsync(id);
        return r == null
            ? NotFound(ApiResponse<string>.Fail("Attendance record not found"))
            : Ok(ApiResponse<AttendanceDto>.Ok(r));
    }

    /// <summary>GET api/attendance/employee/{employeeId} - Get attendance by employee</summary>
    [HttpGet("employee/{employeeId}")]
    public async Task<IActionResult> GetByEmployee(int employeeId)
        => Ok(ApiResponse<IEnumerable<AttendanceDto>>.Ok(await _svc.GetByEmployeeAsync(employeeId)));

    /// <summary>POST api/attendance - Record attendance (Admin only)</summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateAttendanceDto dto)
    {
        var uid = int.Parse(User.FindFirstValue("UserId")!);
        var r = await _svc.CreateAsync(dto, uid);
        return CreatedAtAction(nameof(GetById), new { id = r.Id },
            ApiResponse<AttendanceDto>.Ok(r, "Attendance recorded"));
    }

    /// <summary>PUT api/attendance/{id} - Update attendance (Admin only)</summary>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateAttendanceDto dto)
    {
        var uid = int.Parse(User.FindFirstValue("UserId")!);
        var r = await _svc.UpdateAsync(id, dto, uid);
        return r == null
            ? NotFound(ApiResponse<string>.Fail("Attendance record not found"))
            : Ok(ApiResponse<AttendanceDto>.Ok(r, "Attendance updated"));
    }

    /// <summary>DELETE api/attendance/{id} - Soft delete attendance (Admin only)</summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _svc.DeleteAsync(id);
        return ok
            ? Ok(ApiResponse<string>.Ok("Deleted", "Attendance deleted"))
            : NotFound(ApiResponse<string>.Fail("Attendance record not found"));
    }
}
