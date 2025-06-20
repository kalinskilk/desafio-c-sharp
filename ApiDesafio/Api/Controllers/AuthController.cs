using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ApiDesafio.API.Controllers;


[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly JwtService _jwtService;

    public AuthController(IConfiguration config)
    {
        var secretKey = config["Jwt:Secret"];
        if (string.IsNullOrEmpty(secretKey))
            throw new InvalidOperationException("JWT Secret key is missing from configuration.");

        _jwtService = new JwtService(secretKey);
    }

    [HttpPost("token")]
    [AllowAnonymous] // permite acesso sem autenticação
    public IActionResult GenerateToken()
    {
        var token = _jwtService.GenerateToken();
        return Ok(new { token });
    }
}
