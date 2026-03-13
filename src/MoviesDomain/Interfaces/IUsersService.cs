using MoviesDomain.Dtos;

namespace MoviesDomain.Interfaces;

public interface IUsersService
{
    Task<bool> ValidateUserAsync(string username, string password);
    Task<bool> RegisterUserAsync(UserDto userDto);
    Task<UserDto> GetUserByNameAsync(string username);
    string GenerateToken();
}