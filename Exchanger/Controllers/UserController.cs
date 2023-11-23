using Exchanger.Models;
using Exchanger.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exchanger.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService) 
    {
        _userService = userService;
    }

    [HttpGet("GetUsers")]
    public async Task<List<User>> GetUsers()
    {
        return await _userService.GetUsers();
    }

    /// <summary>
    /// User create
    /// </summary>
    /// <param name="name">User name</param>
    [HttpPost("Create/{name}")]
    public async Task<IActionResult> Create(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return BadRequest("Value parameter is required");
        }

        var user = new User(name);
        await _userService.Create(user);

        return Ok();
    }

    
}
