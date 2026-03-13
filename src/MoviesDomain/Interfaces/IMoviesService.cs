using MoviesDomain.Dtos;
using MoviesDomain.Entities;

namespace MoviesDomain.Interfaces;

public interface IMoviesService
{
    Task<IEnumerable<MovieDto>> GetAllMoviesAsync();
    Task<MovieDto?> GetMovieByIdAsync(Guid movieId);
    Task<bool> AddMovieAsync(MovieDto movieDto);
    MovieDto MapToMovieDto(Movie entityMovie);
    Movie MapToMovieEntity(MovieDto entityMovie);
    
}