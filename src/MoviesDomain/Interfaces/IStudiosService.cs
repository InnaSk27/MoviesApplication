using MoviesDomain.Dtos;

namespace MoviesDomain.Interfaces;

public interface IStudiosService
{
    Task<IEnumerable<StudioDto>> GetAllStudiosAsync();
    // Task<StudioDto?> GetStudioByIdAsync(Guid studioId);
    Task<bool> CreateStudioAsync(StudioDto studio);
    
}