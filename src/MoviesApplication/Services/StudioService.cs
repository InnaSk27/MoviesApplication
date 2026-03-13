using MoviesDomain.Interfaces;
using MoviesDomain.Dtos;
using MoviesDomain.Entities;
using MoviesDomain.Dtos.Enums;
using Org.BouncyCastle.Math.EC.Rfc7748;

namespace MoviesApplication.Services;

public class StudioService: IStudiosService
{
    public IStudiosRepository _studiosRepository;
    public IMoviesService _moviesService;

    public StudioService(IStudiosRepository studiosRepository, IMoviesService moviesService)
    {
        _studiosRepository = studiosRepository;
        _moviesService = moviesService;
    }
    public async Task<IEnumerable<StudioDto>> GetAllStudiosAsync()
    {
        var dbStudio = await _studiosRepository.GetAllStudiosAsync();
        return dbStudio.Select(m => MapToStudioDto(m)); 
    }
    private StudioDto MapToStudioDto(Studio entityStudio)
    {
        var moviesInStudio = entityStudio?.Movies?.Select(x => _moviesService.MapToMovieDto(x));
        return new StudioDto()
        {
            Name = entityStudio.Name,
            Movies = moviesInStudio.ToList()
        };
    }

    // private static Movie MapToMovieEntity(MovieDto dtoMovie)
    // {
    //     return new Movie()
    //     {
    //         Id = Guid.Empty != dtoMovie.Id ? dtoMovie.Id : Guid.NewGuid(),
    //         Genre = (int)dtoMovie.Genre,
    //         Name = dtoMovie.Name,
    //         TicketPrice = dtoMovie.TicketPrice
    //     };
    // }
}