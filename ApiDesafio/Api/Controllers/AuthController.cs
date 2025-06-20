using ApiDesafio.Domain.Entities;
using ApiDesafio.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;

namespace ApiDesafio.API.Controllers;



[ApiController]
[Route("api/[controller]")]
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
