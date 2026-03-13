using MoviesDomain.Interfaces;
using MoviesDomain.Dtos;
using MoviesDomain.Entities;
using MoviesDomain.Dtos.Enums;

namespace MoviesApplication.Services;

public class MoviesService: IMoviesService
{
    public IMoviesRepository _moviesRepository;
    public IStudiosRepository _studiosRepository;

    public MoviesService(IMoviesRepository moviesRepository, IStudiosRepository studiosRepository)
    {
        _moviesRepository = moviesRepository;
        _studiosRepository = studiosRepository;
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
    
    public async Task<bool> AddMovieAsync(MovieDto movieDto)
    {
        var dbMovie = MapToMovieEntity(movieDto);
        
        if(movieDto.StudioId != Guid.Empty)
        {
            dbMovie.Studio = await _studiosRepository.GetStudioByIdAsync(movieDto.StudioId);
        }

        return await _moviesRepository.AddMovieAsync(dbMovie);
    }

    public MovieDto MapToMovieDto(Movie entityMovie)
    {
        return new MovieDto()
        {
            Genre = (MoviesGenre)entityMovie.Genre,
            Id = entityMovie.Id,
            Name = entityMovie.Name,
            TicketPrice = entityMovie.TicketPrice
        };
    }

    public Movie MapToMovieEntity(MovieDto dtoMovie)
    {
        return new Movie()
        {
            Id = Guid.Empty != dtoMovie.Id ? dtoMovie.Id : Guid.NewGuid(),
            Genre = (int)dtoMovie.Genre,
            Name = dtoMovie.Name,
            TicketPrice = dtoMovie.TicketPrice
        };
    }
}