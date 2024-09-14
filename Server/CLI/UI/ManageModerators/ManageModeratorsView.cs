using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageModerators;

public class ManageModeratorsView
{
    private readonly IModeratorRepository moderatorRepository;

    public ManageModeratorsView(IModeratorRepository moderatorRepository)
    {
        this.moderatorRepository = moderatorRepository;
    }

    public async Task DeleteModerator(int moderatorId)
    {
        await moderatorRepository.DeleteAsync(moderatorId);
        Console.WriteLine($"Moderator with ID {moderatorId} has been deleted");
    }

    public async Task UpdateModerator(int userId, int subForumId)
    {
        Moderator moderator = new Moderator(userId, subForumId);
        await moderatorRepository.UpdateAsync(moderator);
        Console.WriteLine($"Moderator with ID {moderator.Id} has been updated");
    }
}