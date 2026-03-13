using MoviesDomain.Entities;

namespace MoviesDomain.Interfaces;

public interface IStudiosRepository
{
    Task<IEnumerable<Studio>> GetAllStudiosAsync();
    // Task<MovieDto?> GetMovieByIdAsync(Guid movieId);
    // Task<MovieDto?> AddMovieAsync(MovieDto movieId);
    
}