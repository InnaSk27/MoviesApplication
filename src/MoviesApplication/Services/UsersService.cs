using MoviesDomain.Entities;
using MoviesDomain.Interfaces;
using Microsoft.AspNetCore.Identity;
using MoviesDomain.Dtos;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using MoviesDomain.JWT;

namespace MoviesApplication.Services;

public class UsersService : IUsersService
{
    public readonly IUsersRepository _usersRepository;
    public JwtSettings _jwtSettings;

    public UsersService(IUsersRepository usersRepository, JwtSettings jwtSettings)
    {
        _usersRepository = usersRepository;
        _jwtSettings = jwtSettings;
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
        if (await _usersRepository.UserExistsAsync(userDto.Name ?? ""))
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

    public string GenerateToken()
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, "testuser"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey ?? ""));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiryMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}