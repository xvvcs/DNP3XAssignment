using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageModerators;

public class SingleModeratorView
{
    private readonly IModeratorRepository moderatorRepository;
    private readonly IUserRepository userRepository;

    public SingleModeratorView(IModeratorRepository moderatorRepository, IUserRepository userRepository)
    {
        this.moderatorRepository = moderatorRepository;
        this.userRepository = userRepository;
    }

    public async Task DisplayModerator(int moderatorId)
    {
        Moderator moderator = await moderatorRepository.GetSingleAsync(moderatorId);
        Console.WriteLine($"- ID: {moderator.Id}, Username: {userRepository.GetSingleAsync(moderator.UserId).Result.Username}, Sub-forum ID: {moderator.SubForumId}");
    }

}