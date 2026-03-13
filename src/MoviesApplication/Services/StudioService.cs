using MoviesDomain.Interfaces;
using MoviesDomain.Dtos;
using MoviesDomain.Entities;

namespace MoviesApplication.Services;

public class StudioService: IStudiosService
{
    public IStudiosRepository _studiosRepository;
    public IMoviesRepository _moviesRepository;

    public StudioService(IStudiosRepository studiosRepository, IMoviesRepository moviesRepository)
    {
        _studiosRepository = studiosRepository;
        _moviesRepository = moviesRepository;
    }
    public async Task<IEnumerable<StudioDto>> GetAllStudiosAsync()
    {
        var dbStudio = await _studiosRepository.GetAllStudiosAsync();
        return dbStudio.Select(m => MapToStudioDto(m)); 
    }

    public async Task<StudioDto?> GetStudioByIdAsync(Guid studioId)
    {
        var studioDto = new StudioDto();
        var dbStudio = await _studiosRepository.GetStudioByIdAsync(studioId);

        if(dbStudio != null)
        {
            studioDto.Name = dbStudio.Name;
        }

        return studioDto;
    }

    public async Task<bool> AddStudioAsync(StudioDto studioDto)
    {
        if(!await _studiosRepository.ValidateStudioAsync(studioDto.Name ?? ""))
        {
            var dbStudio = MapToStudioEntity(studioDto);
            
            if (studioDto.MovieIds.Any())
            {
                dbStudio.Movies = new List<Movie>();
                foreach(var movieId in studioDto.MovieIds)
                {
                    var dbMovie = await _moviesRepository.GetMovieByIdAsync(movieId);
                    if(dbMovie != null)
                    {
                        dbStudio.Movies.Add(dbMovie);
                    }
                }
            }

            return await _studiosRepository.AddStudioAsync(dbStudio);
        }
        return false;
    }

    private StudioDto MapToStudioDto(Studio entityStudio)
    {
        var moviesInStudio = entityStudio?.Movies?.Select(x => x.Id);
        return new StudioDto()
        {
            Name = entityStudio?.Name,
            MovieIds = moviesInStudio?.ToList()
        };
    }

    private Studio MapToStudioEntity(StudioDto studioDto)
    {
        return new Studio
        {
            Id = Guid.Empty != studioDto.Id ? studioDto.Id : Guid.NewGuid(),
            Name = studioDto.Name
        };
    }
}