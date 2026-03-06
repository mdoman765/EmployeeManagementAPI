using EmployeeManagementAPI.DTOs.UpdateProfile;
using EmployeeManagementAPI.DTOs.User;
using EmployeeManagementAPI.Helpers;
using EmployeeManagementAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmployeeManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]                          // ✅ Any authenticated user — no Admin role required
    public class ProfileController : ControllerBase
    {
        private readonly IUpdateProfileService _svc;

        public ProfileController(IUpdateProfileService svc) => _svc = svc;

        private int CurrentUserId =>
            int.Parse(User.FindFirstValue("UserId")!);  // extracted from JWT token

        /// <summary>GET api/profile — Get own profile</summary>
        [HttpGet]
        public async Task<IActionResult> GetMyProfile()
        {
            var result = await _svc.GetProfileAsync(CurrentUserId);
            return Ok(ApiResponse<UserDto>.Ok(result));
        }

        /// <summary>PUT api/profile — Update own profile</summary>
        [HttpPut]
        public async Task<IActionResult> UpdateMyProfile([FromBody] UpdateProfileDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<string>.Fail("Invalid request data"));

            var (ok, error, data) = await _svc.UpdateProfileAsync(CurrentUserId, dto);

            return ok
                ? Ok(ApiResponse<UserDto>.Ok(data!, "Profile updated successfully"))
                : BadRequest(ApiResponse<string>.Fail(error!));
        }
    }

}
