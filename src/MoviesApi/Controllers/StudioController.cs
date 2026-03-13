

using Microsoft.AspNetCore.Mvc;
using MoviesDomain.Dtos;
using MoviesDomain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace MoviesApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudioController : ControllerBase
{
    private IStudiosService _studiosService;

    public StudioController(IStudiosService studiosService)
    {
        _studiosService = studiosService;   
    }

    [HttpGet(Name = "GetAllStudios")]
    [Authorize]
    public async Task<IEnumerable<StudioDto>> Get()
    {
        return await _studiosService.GetAllStudiosAsync();
    }

    /// <summary>
    /// Creates a Studio.
    /// </summary>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddStudio(StudioDto studioToAdd)
    {
        if(!await _studiosService.AddStudioAsync(studioToAdd))
        {
            return NotFound();
        }

        return Ok();
    }

}
