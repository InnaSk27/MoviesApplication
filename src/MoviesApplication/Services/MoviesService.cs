using MoviesDomain.Interfaces;
using MoviesDomain.Dtos;
using MoviesDomain.Entities;
using MoviesDomain.Dtos.Enums;

namespace MoviesApplication.Services;

public class MoviesService: IMoviesService
{
    public IMoviesRepository _moviesRepository;

    public MoviesService(IMoviesRepository moviesRepository)
    {
        _moviesRepository = moviesRepository;
    }
    public async Task<IEnumerable<MovieDto>> GetAllMoviesAsync()
    {
        var dbMovies = await _moviesRepository.GetAllMoviesAsync();
        return dbMovies.Select(m => MapToMovieDto(m)); 
    }

    public async Task<MovieDto?> GetMovieByIdAsync(Guid movieId)
    {
        var dbMovie = await _moviesRepository.GetMovieByIdAsync(movieId);
        if(dbMovie != null)
        {
            return MapToMovieDto(dbMovie);
        }

        return null;
    }
    
    public async Task<MovieDto?> AddMovieAsync(MovieDto movieDto)
    {
        var dbMovie = MapToMovieEntity(movieDto);
        if(await _moviesRepository.AddMovieAsync(dbMovie))
        {
            return await GetMovieByIdAsync(dbMovie.Id);
        }

        return null;
    }

    private static MovieDto MapToMovieDto(Movie entityMovie)
    {
        return new MovieDto()
        {
            Genre = (MoviesGenre)entityMovie.Genre,
            Id = entityMovie.Id,
            Name = entityMovie.Name,
            TicketPrice = entityMovie.TicketPrice
        };
    }

    private static Movie MapToMovieEntity(MovieDto dtoMovie)
    {
        return new Movie()
        {
            Id = Guid.NewGuid(),
            Genre = (int)dtoMovie.Genre,
            Name = dtoMovie.Name,
            TicketPrice = dtoMovie.TicketPrice
        };
    }
}