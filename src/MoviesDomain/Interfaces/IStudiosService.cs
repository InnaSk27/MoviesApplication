using MoviesDomain.Dtos;

namespace MoviesDomain.Interfaces;

public interface IStudiosService
{
    Task<IEnumerable<StudioDto>> GetAllStudiosAsync();
    // Task<MovieDto?> GetMovieByIdAsync(Guid movieId);
    // Task<MovieDto?> AddMovieAsync(MovieDto movieId);
    
}