using DTOs.Posts;
using DTOs.SubForum;

namespace Blazor.Services;

public interface ISubforumService
{
    public Task<SubforumDTO> AddSubforumAsync(AddSubForumDTO subforum);
    public Task<SubforumDTO> GetSubforumAsync(int id);
    public Task<SubforumDTO> UpdateSubforumAsync(SubforumDTO subforum);
    public Task DeleteSubforumAsync(int id);
    public Task<IEnumerable<SubforumDTO>> GetSubforumsAsync();
    public Task<IEnumerable<PostDTO>> GetPostsBySubforumAsync(int subforumId);
    public Task AddPostsToSubforumAsync(int subforumId, int postId);
    public Task DeletePostsFromSubforumAsync(int subforumId, int postId);
    public Task<IEnumerable<SubforumDTO>> GetSubforumsByUserIdAsync(int userId);
}