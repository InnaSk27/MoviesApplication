

using Microsoft.AspNetCore.Mvc;
using MoviesDomain.Dtos;
using MoviesDomain.Interfaces;

namespace MoviesApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private IMoviesService _moviesService;

    public MoviesController(IMoviesService moviesService)
    {
        _moviesService = moviesService;   
    }

    [HttpGet(Name = "GetAllMovies")]
    public async Task<IEnumerable<MovieDto>> Get()
    {
        return await _moviesService.GetAllMoviesAsync();
    }
    
    [HttpGet("{movieId}")]
    public async Task<ActionResult<MovieDto>> Get(Guid movieId)
    {
        var movie = await _moviesService.GetMovieByIdAsync(movieId);
        if(movie == null)
        {
            return NotFound($"Record with id {movieId} was not found.");
        }

        return Ok(movie);
    }

    [HttpPost]
    public async Task<ActionResult<MovieDto>> AddMovie(MovieDto movieToAdd)
    {
        var movie = await _moviesService.AddMovieAsync(movieToAdd);

        if(movie == null)
        {
            return NotFound();
        }

        return Ok(movie);
    }

}
