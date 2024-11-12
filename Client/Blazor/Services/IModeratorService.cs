using DTOs.Moderators;
using DTOs.Posts;
using DTOs.SubForum;

namespace Blazor.Services;

public interface IModeratorService
{
    public Task<AddModeratorDTO> AddModeratorAsync(AddModeratorDTO moderator);
    public Task<ModeratorDTO> GetModeratorAsync(int id);
    public Task<ModeratorDTO> GetModeratorAsync(string username);
    public Task<ModeratorDTO> UpdateModeratorAsync(ModeratorDTO user);
    public Task DeleteModeratorAsync(int id);
    public Task<IEnumerable<ModeratorDTO>> GetModeratorAsync();
    // Gets all subforums managed by a moderator
    public Task<IEnumerable<SubforumDTO>> GetSubforumsAsync(int moderatorId);

    // Assigns a moderator to multiple subforums
    public Task AssignModeratorToSubforumsAsync(int moderatorId, List<int> subforumIds);

    // Removes a moderator from specific subforums
    public Task RemoveModeratorFromSubforumsAsync(int moderatorId, List<int> subforumIds);
}