using MoviesDomain.Entities;

namespace MoviesDomain.Interfaces;

public interface IUsersRepository
{
    Task<User?> GetUserAsync(string username);
    Task<bool> UserExistsAsync(string username);
    Task<bool> CreateUserAsync(User userToAdd);
}