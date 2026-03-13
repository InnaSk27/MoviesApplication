using Microsoft.EntityFrameworkCore;
using MoviesDomain.Entities;
using MoviesDomain.Interfaces;

namespace MoviesInfrastructure.Repositories;

public class StudiosRepository: IStudiosRepository
{
    private MoviesDbContext _moviesDbContext;

    public StudiosRepository(MoviesDbContext moviesDbContext)
    {
        _moviesDbContext = moviesDbContext;
    }

    public async Task<IEnumerable<Studio>> GetAllStudiosAsync()
    {
        return await _moviesDbContext.Studio.ToListAsync();
    }
    public async Task<Studio?> GetStudioByIdAsync(Guid studioId)
    {
        return await _moviesDbContext.Studio.FindAsync(studioId);
    }
    public async Task<bool> ValidateStudioAsync(string studioName)
    {
        return await _moviesDbContext.Studio.AnyAsync(st => st.Name == studioName);
    }
    
    public async Task<bool> AddStudioAsync(Studio studio)
    {
        try
        {
            _moviesDbContext.Studio.Add(studio);
            return await _moviesDbContext.SaveChangesAsync() > 0;
        }
        catch (Exception)
        {
            return false;
        }
    }
}