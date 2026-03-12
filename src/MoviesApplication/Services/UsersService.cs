using System.Security.Permissions;
using MoviesDomain.Entities;
using MoviesDomain.Interfaces;
using Microsoft.AspNetCore.Identity;
using MoviesInfrastructure;
using MoviesDomain.Dtos;

namespace MoviesApplication.Services;

public class UsersService : IUsersService
{
    public readonly IUsersRepository _usersRepository;

    public UsersService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    } 

    public async Task<bool> ValidateUserAsync(string username, string password)
    {
        var user = await _usersRepository.GetUserAsync(username);
        if(user == null)
        {
            return false;
        }

        var passwordHasher = new PasswordHasher<User>();

        var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

        return result == PasswordVerificationResult.Success;
    }

    public async Task<bool> RegisterUserAsync(UserDto userDto)
    {
        if (await _usersRepository.UserExistsAsync(userDto.Name))
        {
            return false;
        }
        
        var passwordHasher = new PasswordHasher<User?>();

        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = userDto.Name,
            PasswordHash = passwordHasher.HashPassword(null, userDto.Password)
        };

        return await _usersRepository.CreateUserAsync(user);
    }
}