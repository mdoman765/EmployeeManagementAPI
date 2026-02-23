using System.Security.Claims;
using EmployeeManagementAPI.DTOs.Salary;
using EmployeeManagementAPI.Helpers;
using EmployeeManagementAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SalariesController : ControllerBase
{
    private readonly ISalaryService _svc;
    public SalariesController(ISalaryService svc) => _svc = svc;

    /// <summary>GET api/salaries - Get all salary records</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(ApiResponse<IEnumerable<SalaryDto>>.Ok(await _svc.GetAllAsync()));

    /// <summary>GET api/salaries/employee/{employeeId} - Get salaries for a specific employee</summary>
    [HttpGet("employee/{employeeId}")]
    public async Task<IActionResult> GetByEmployee(int employeeId)
        => Ok(ApiResponse<IEnumerable<SalaryDto>>.Ok(await _svc.GetByEmployeeAsync(employeeId)));

    /// <summary>POST api/salaries - Add salary record (Admin only). Automatically closes previous salary.</summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateSalaryDto dto)
    {
        var uid = int.Parse(User.FindFirstValue("UserId")!);
        var r = await _svc.CreateAsync(dto, uid);
        return Ok(ApiResponse<SalaryDto>.Ok(r, "Salary record created"));
    }
}
