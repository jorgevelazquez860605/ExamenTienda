using Store.Bussines.DTOs;

namespace Store.Bussines.Interfaces;
public interface IAuthService
{
    Task<AuthResponseDto> AuthenticateAsync(UserLoginDto loginDto);
}
