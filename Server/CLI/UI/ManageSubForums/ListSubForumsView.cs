using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageSubForums;

public class ListSubForumsView
{
    private readonly ISubForumRepository subForumRepository;
    private readonly IModeratorRepository moderatorRepository;
    private readonly IUserRepository userRepository;
    
    public ListSubForumsView(ISubForumRepository subForumRepository, IModeratorRepository moderatorRepository, IUserRepository userRepository)
    {
        this.moderatorRepository = moderatorRepository;
        this.subForumRepository = subForumRepository;
        this.userRepository = userRepository;
    }

    public async Task DisplayAllSubForums()
    {
        Console.WriteLine("Listing all sub-forums:");
        var subForums = subForumRepository.GetManyAsync(); 
            
        foreach (SubForum subForum in subForums)
        {
            var moderators = await moderatorRepository.GetModeratorsBySubForumIdAsync(subForum.Id);
            
            var usernames = new List<string>();
            foreach (var moderator in moderators)
            {
                var user = await userRepository.GetSingleAsync(moderator.UserId); 
                if (user != null)
                {
                    usernames.Add(user.Username);
                }
            }

            string moderatorsList = string.Join(", ", usernames);

            Console.WriteLine($"ID: {subForum.Id}, Title: {subForum.Title}, Description: {subForum.Description}, " +
                              $"Moderated by: {moderatorsList}");
        }
    }
}