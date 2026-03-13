using MoviesDomain.Dtos;
using MoviesDomain.Entities;

namespace MoviesDomain.Interfaces;

public interface IMoviesService
{
    Task<IEnumerable<MovieDto>> GetAllMoviesAsync();
    Task<MovieDto?> GetMovieByIdAsync(Guid movieId);
    Task<MovieDto?> AddMovieAsync(MovieDto movieId);
    MovieDto MapToMovieDto(Movie entityMovie);
    
}