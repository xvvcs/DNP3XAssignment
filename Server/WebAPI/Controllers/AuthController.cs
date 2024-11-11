using DTOs;
using DTOs.Posts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public AuthController(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDTO requestDto)
    {
        if (requestDto == null)
        {
            return BadRequest("Invalid login requestDto.");
        }

        var user = await _userRepository.GetByUsernameAsync(requestDto.Username);
        if (user == null || user.Password != requestDto.Password)
        {
            return Unauthorized("Invalid username or password.");
        }

        UserDTO dto = new UserDTO
        {
            Id = user.Id,
            Username = user.Username
        };

        return Ok(dto);
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] AddUserDTO request)
    {
        if (request == null)
        {
            return BadRequest("Invalid register request.");
        }

        User user = new User
        {
            Username = request.Username,
            Password = request.Password,
        };

        await _userRepository.AddASync(user);

        UserDTO dto = new UserDTO
        {
            Id = user.Id,
            Username = user.Username
        };

        return Created($"users/{user.Id}", dto);
    }
}