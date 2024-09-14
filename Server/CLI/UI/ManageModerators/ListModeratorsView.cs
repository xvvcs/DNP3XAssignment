using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageModerators;

public class ListModeratorsView
{
    private readonly IModeratorRepository moderatorRepository;
    private readonly IUserRepository userRepository;
    private readonly ISubForumRepository subForumRepository;

    public ListModeratorsView(IModeratorRepository moderatorRepository, IUserRepository userRepository, ISubForumRepository subForumRepository)
    {
        this.moderatorRepository = moderatorRepository;
        this.userRepository = userRepository;
        this.subForumRepository = subForumRepository;
    }

    public async Task DisplayModeratorsAsync()
    {
        Console.WriteLine("Listing all moderators");
        foreach (Moderator moderator in moderatorRepository.GetManyAsync())
        {
            Console.WriteLine($"- ID: {moderator.Id}, Username: {userRepository.GetSingleAsync(moderator.UserId).Result.Username} moderates " +
                              $"sub-forum {subForumRepository.GetSingleAsync(moderator.SubForumId).Result.Title} ");
                // can be improved in situations when one user moderates multiple sub-forums
        }
    }

    public async Task DisplayModeratorsBySubForumIdAsync(int subForumId)
    {
        var filteredModerators = await moderatorRepository.GetModeratorsBySubForumIdAsync(subForumId);
        Console.WriteLine($"Listing all moderators of sub-forum: {subForumRepository.GetSingleAsync(subForumId).Result.Title}");
        foreach (Moderator moderator in filteredModerators)
        {
            Console.WriteLine($"- ID: {moderator.Id}, Username: {userRepository.GetSingleAsync(moderator.UserId).Result.Username}");
        }
    }
}