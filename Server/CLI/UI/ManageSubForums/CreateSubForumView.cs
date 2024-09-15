using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageSubForums;

public class CreateSubForumView
{
    private readonly ISubForumRepository subForumRepository;
    
    public CreateSubForumView(ISubForumRepository subForumRepository)
    {
        this.subForumRepository = subForumRepository;
    }
    
    public async Task createSubForumAsync(string subForumName, string subforumDesc, int userID)
    {
        SubForum subforum = new SubForum(subForumName, subforumDesc, userID);
        await subForumRepository.AddAsync(subforum);
        Console.WriteLine($"Subforum '{subForumName}' was created successfully.");
    }
}