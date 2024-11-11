using DTOs.Posts;
using DTOs.SubForum;

namespace Blazor.Services;

public interface ISubforumService
{
    public Task<SubforumDTO> AddSubforumAsync(SubforumDTO post);
    public Task<SubforumDTO> GetPostAsync(int id);
    public Task<SubforumDTO> GetSubforumAsync(int id);
    public Task<SubforumDTO> UpdateSubforumAsync(SubforumDTO post);
    public Task DeleteSubforumAsync(int id);
    public Task<IEnumerable<SubforumDTO>> GetsubforumAsync();
    public Task<IEnumerable<PostDTO>> GetPostsAsync(int moderatorId);
}