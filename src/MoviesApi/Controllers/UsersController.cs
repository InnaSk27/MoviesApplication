using Microsoft.AspNetCore.Mvc;
using MoviesDomain.Dtos;
using MoviesDomain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;

namespace MoviesApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private IUsersService _userService;
    
    public UsersController(IUsersService usersService)
    {
        _userService = usersService;
    }

    [HttpPost("LoginWithDto")]
    public async Task<IActionResult> LoginWithDto([FromBody] UserDto request)
    {
        if(request != null)
        {
            var username = request.Name;
            if(await _userService.ValidateUserAsync(username, request.Password))
            {
                var user = await _userService.GetUserByNameAsync(username);
                var token = _userService.GenerateToken();
                return Ok(new {Token = token});
            }
            return Unauthorized("Invalid username or password");
        }
      
        return Ok();
    }

    [HttpPost("RegisterWithDto")]
    public async Task<IActionResult> RegisterWithDto([FromBody] UserDto request)
    {
        if(await _userService.RegisterUserAsync(request))
        {
            return Ok();
        }

        return BadRequest("User already exists");
    }


}