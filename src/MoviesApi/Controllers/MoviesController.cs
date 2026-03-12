

using Microsoft.AspNetCore.Mvc;
using MoviesDomain.Dtos;
using MoviesDomain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

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
    [Authorize]
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

    /// <summary>
    /// Creates a Movie.
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     {
    ///        "name": "Die Hard",
    ///        "genre": "Action",
    ///        "ticketPrice": 20
    ///     }
    /// </remarks>
    [HttpPost]
    // [Authorize]
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
