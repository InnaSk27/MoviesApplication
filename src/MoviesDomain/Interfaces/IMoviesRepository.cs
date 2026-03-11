using MoviesDomain.Entities;

namespace MoviesDomain.Interfaces;

public interface IMoviesRepository
{
    Task<IEnumerable<Movie>> GetAllMoviesAsync();
    Task<Movie?> GetMovieByIdAsync(Guid movieId);
    Task<bool> AddMovieAsync(Movie movie);
}