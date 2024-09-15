using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageSubForums;

public class SingleSubForumView
{
    private readonly ISubForumRepository subForumRepository;
    private readonly IModeratorRepository moderatorRepository;
    
    public SingleSubForumView(ISubForumRepository subForumRepository, IModeratorRepository moderatorRepository)
    {
        this.subForumRepository = subForumRepository;
        this.moderatorRepository = moderatorRepository;
        
    }

    public async Task DisplaySingleSubForum(int subForumId)
    {
        SubForum subForum = await subForumRepository.GetSingleAsync(subForumId);
        Console.WriteLine($"ID: {subForum.Id}, Title: {subForum.Title}, Description: {subForum.Description}, " +
                          $"Moderated by: {await moderatorRepository.GetModeratorsBySubForumIdAsync(subForum.Id)}");
    }
}