using MoviesDomain.Interfaces;
using MoviesDomain.Dtos;
using MoviesDomain.Entities;

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

    public async Task<bool> CreateStudioAsync(StudioDto studioDto)
    {
        if(!await _studiosRepository.ValidateStudioAsync(studioDto.Name ?? ""))
        {
            return await _studiosRepository.AddStudioAsync(MapToStudioEntity(studioDto));
        }
        return false;
    }

    private StudioDto MapToStudioDto(Studio entityStudio)
    {
        var moviesInStudio = entityStudio?.Movies?.Select(x => _moviesService.MapToMovieDto(x));
        return new StudioDto()
        {
            Name = entityStudio?.Name,
            Movies = moviesInStudio?.ToList()
        };
    }

    private Studio MapToStudioEntity(StudioDto studioDto)
    {
        var moviesToAdd = new List<Movie>();

        if(studioDto.Movies != null && studioDto.Movies.Any())
        {
            moviesToAdd = studioDto.Movies.Select(x => _moviesService.MapToMovieEntity(x)).ToList();
        }
        
        return new Studio
        {
            Id = Guid.Empty != studioDto.Id ? studioDto.Id : Guid.NewGuid(),
            Name = studioDto.Name,
            Movies = moviesToAdd
        };
    }
}