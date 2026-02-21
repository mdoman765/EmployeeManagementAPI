using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace EmployeeManagementAPI.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, Exception exception, CancellationToken ct)
    {
        _logger.LogError(exception, "Unhandled exception: {Message}", exception.Message);

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new { success = false, message = "An unexpected error occurred.", error = exception.Message };
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response), ct);
        return true;
    }
}
