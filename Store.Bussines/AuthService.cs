using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Store.Bussines.DTOs;
using Store.Bussines.Interfaces;
using Store.Data.Entities;
using Store.Data.Repositories.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Store.Bussines;

public class AuthService : IAuthService
{
    private readonly IGenericRepository<Cliente> _clienteRepository;
    private readonly string _jwtSecret;

    public AuthService(IGenericRepository<Cliente> clienteRepository, IConfiguration configuration)
    {
        _clienteRepository = clienteRepository;
        _jwtSecret = configuration["JwtSettings:Secret"];
    }

    public async Task<AuthResponseDto> AuthenticateAsync(UserLoginDto loginDto)
    {
        var cliente = await _clienteRepository.GetAllAsync();
        var user = cliente.FirstOrDefault(u => u.Email == loginDto.Email);

        if (user == null || user.PasswordHash != loginDto.Password)
        {
            return new AuthResponseDto { Message = "Credenciales incorrectas" };
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSecret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim("ClienteId", user.ClienteId.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new AuthResponseDto { Token = tokenHandler.WriteToken(token), Message = "Login exitoso" };
    }
}
