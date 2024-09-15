using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageSubForums;

public class ManageSubForumsView
{
    private readonly ISubForumRepository subForumRepository;
    
    public ManageSubForumsView(ISubForumRepository subForumRepository)
    {
        this.subForumRepository = subForumRepository;
    }

    public async Task DeleteSubForumAsync(int subForumId)
    {
        await subForumRepository.DeleteAsync(subForumId);
        Console.WriteLine($"Subforum {subForumId} deleted successfully.");
    }

    public async Task UpdateSubForumAsync(int subForumId, string subforumName, string subforumDesc)
    {
        SubForum updatedSubForum = new SubForum(subforumName, subforumDesc, 
            subForumRepository.FindSubForumCreator(subForumId).Result);
        await subForumRepository.UpdateAsync(updatedSubForum);
        Console.WriteLine($"Subforum {subForumId} updated successfully.");
    }
}