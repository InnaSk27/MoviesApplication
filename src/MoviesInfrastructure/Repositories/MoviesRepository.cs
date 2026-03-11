using Microsoft.EntityFrameworkCore;
using MoviesDomain.Entities;
using MoviesDomain.Interfaces;

namespace MoviesInfrastructure.Repository;

public class MoviesRepository: IMoviesRepository
{
    private MoviesDbContext _moviesDbContext;

    public MoviesRepository(MoviesDbContext moviesDbContext)
    {
        _moviesDbContext = moviesDbContext;
    }

    public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
    {
        return await _moviesDbContext.Movies.ToListAsync();
    }
    public async Task<Movie?> GetMovieByIdAsync(Guid movieId)
    {
        return await _moviesDbContext.Movies.FindAsync(movieId);
    }
    public async Task<bool> AddMovieAsync(Movie movie)
    {
        try
        {
            _moviesDbContext.Movies.Add(movie);
            return await _moviesDbContext.SaveChangesAsync() > 0;
        }
        catch (Exception)
        {
            return false;
        }
    }
}