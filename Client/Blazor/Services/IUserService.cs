using DTOs.Posts;

namespace Blazor.Services;

public interface IUserService
{
    public Task<AddUserDTO> AddUserAsync(AddUserDTO user);
    public Task<UserDTO> GetUserAsync(int id);
    public Task<UserDTO> GetUserAsync(string username);
    public Task<UserDTO> UpdateUserAsync(UserDTO user);
    public Task DeleteUserAsync(int id);
    public Task<IEnumerable<UserDTO>> GetUsersAsync();
    public Task<IEnumerable<PostDTO>> GetPostsAsync(int userId);
}