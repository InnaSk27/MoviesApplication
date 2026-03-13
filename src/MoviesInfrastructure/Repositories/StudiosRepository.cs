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
    // public async Task<Movie?> GetMovieByIdAsync(Guid movieId)
    // {
    //     return await _moviesDbContext.Movies.FindAsync(movieId);
    // }
    // public async Task<bool> AddMovieAsync(Movie movie)
    // {
    //     try
    //     {
    //         _moviesDbContext.Movies.Add(movie);
    //         return await _moviesDbContext.SaveChangesAsync() > 0;
    //     }
    //     catch (Exception)
    //     {
    //         return false;
    //     }
    // }
}