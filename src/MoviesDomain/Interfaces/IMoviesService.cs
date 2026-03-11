using MoviesDomain.Dtos;

namespace MoviesDomain.Interfaces;

public interface IMoviesService
{
    Task<IEnumerable<MovieDto>> GetAllMoviesAsync();
    Task<MovieDto?> GetMovieByIdAsync(Guid movieId);
    Task<MovieDto?> AddMovieAsync(MovieDto movieId);
    
}