using DTOs.Posts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController
{
    private readonly IUserRepository _userRepository;
    
    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    //POST https://localhost:7198/users
    [HttpPost]
    public async Task<IResult> AddUserAsync([FromBody] AddUserDTO request)
    {
        User user = new User()
        {
            Username = request.Username,
            Password = request.Password,
            Id = request.Id
        };
        await _userRepository.AddASync(user);
        return Results.Created($"users/{user.Id}", user);
    } 
    //GET https://localhost:7198/users/{id}
    [HttpGet("{id:int}")]
    public async Task<IResult> GetSingleUserAsync([FromRoute] int id)
    {
        try
        {
            User result = await _userRepository.GetSingleAsync(id);
            return Results.Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Results.NotFound(e.Message);
        }
    }
    //DELETE https://localhost:7198/users/{id}
    [HttpDelete("{id:int}")]
    public async Task<IResult> DeleteUserAsync(int id)
    {
        await _userRepository.DeleteAsync(id);
        return Results.NoContent();
    }
    //PUT https://localhost:7198/users/{id}
    [HttpPut("{id:int}")]
    public async Task<IResult> UpdateUserAsync([FromRoute] int id, [FromBody] ReplaceUserDTO request)
    {
        User user = new User()
        {
            Username = request.Username,
            Password = request.Password,
            Id = id
        };
        await _userRepository.UpdateAsync(user);
        return Results.Ok(user);
    }
    //GET https://localhost:7198/users
    [HttpGet]
    public async Task<IResult> GetAllUsersAsync([FromQuery] string? username, [FromQuery] int? userId)
    {
        List<User> users = _userRepository.GetMany().ToList();
        if (!string.IsNullOrWhiteSpace(username))
        {
            users = users.Where(u => u.Username.Contains(username, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        if (userId.HasValue)
        {
            users = users.Where(u => u.Id == userId).ToList();
        }

        return Results.Ok(users);

    }
    
    //GET https://localhost:7198/users/username
    [HttpGet("by-username")]
    public async Task<IResult> GetByUsernameAsync([FromQuery] string username)
    {
        try
        {
            User result = await _userRepository.GetByUsernameAsync(username);
            return Results.Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Results.NotFound(e.Message);
        }
    }
}
