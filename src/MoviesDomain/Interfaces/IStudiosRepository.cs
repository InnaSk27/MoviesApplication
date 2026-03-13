using MoviesDomain.Entities;

namespace MoviesDomain.Interfaces;

public interface IStudiosRepository
{
    Task<IEnumerable<Studio>> GetAllStudiosAsync();
    Task<bool> ValidateStudioAsync(string studioName);
    Task<bool> AddStudioAsync(Studio studio);
    Task<Studio?> GetStudioByIdAsync(Guid studioId);
    // Task<MovieDto?> AddMovieAsync(MovieDto movieId);
    
}