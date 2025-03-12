using Microsoft.AspNetCore.Mvc;
using Store.Bussines.DTOs;
using Store.Bussines.Interfaces;

namespace Store.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
    {
        var response = await _authService.AuthenticateAsync(loginDto);
        if (string.IsNullOrEmpty(response.Token))
            return Unauthorized(response.Message);

        return Ok(response);
    }
}
