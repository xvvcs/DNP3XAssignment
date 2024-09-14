using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageModerators;

public class CreateModeratorView
{
    private readonly IModeratorRepository moderatorRepository;

    public CreateModeratorView(IModeratorRepository moderatorRepository)
    {
        this.moderatorRepository = moderatorRepository;
    }

    public async Task CreateModeratorAsync(int userID, int subForumID)
    {
        Moderator moderator = new Moderator(userID, subForumID);
        await moderatorRepository.AddAsync(moderator);
        Console.WriteLine($"User with ID {userID} has become moderator of sub-forum  with ID {subForumID}");
        
        // at some point we need to check if the user with given id even exist
    }
}