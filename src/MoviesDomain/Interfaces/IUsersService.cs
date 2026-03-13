using MoviesDomain.Dtos;

namespace MoviesDomain.Interfaces;

public interface IUsersService
{
    Task<bool> ValidateUserAsync(string username, string password);
    Task<bool> RegisterUserAsync(UserDto userDto);
    string GenerateToken();
}