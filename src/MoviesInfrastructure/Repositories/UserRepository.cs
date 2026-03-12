using MoviesDomain.Entities;
using MoviesDomain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MoviesInfrastructure.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly MoviesDbContext _moviesDbContext;

    public UsersRepository(MoviesDbContext moviesDbContext)
    {
        _moviesDbContext = moviesDbContext;
    }

    public async Task<User?> GetUserAsync(string username)
    {
        return await _moviesDbContext.Users.SingleOrDefaultAsync(u => u.Name == username);
    }

    public async Task<bool> UserExistsAsync(string username)
    {
        return await _moviesDbContext.Users.AnyAsync(u => u.Name == username);
    }

    public async Task<bool> CreateUserAsync(User userToAdd)
    {
         _moviesDbContext.Users.Add(userToAdd);
         
         return await _moviesDbContext.SaveChangesAsync() > 0;

    }
}