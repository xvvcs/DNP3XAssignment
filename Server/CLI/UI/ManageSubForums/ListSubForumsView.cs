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
        this.userRepository = userRepository;
        this.moderatorRepository = moderatorRepository;
        this.subForumRepository = subForumRepository;
    }

    public async Task DisplayAllSubForums()
    {
        Console.WriteLine("Listing all sub-forums:");
        var subForums = subForumRepository.GetMany();

        foreach (SubForum subForum in subForums)
        {
            var moderators = await moderatorRepository.GetModeratorsBySubForumIdAsync(subForum.Id);
        
            var subUsernames = new List<string>();
            foreach (var moderator in moderators)
            {
                var user = await userRepository.GetSingleAsync(moderator.UserId);
                if (user != null)
                {
                    subUsernames.Add(user.Username);
                }
            }

            if (subUsernames.Any())
            {
                Console.WriteLine($"ID: {subForum.Id}, Title: {subForum.Title}, Description: {subForum.Description}");
                Console.WriteLine("Moderators:");
                for (int i = 0; i < subUsernames.Count; i++)
                {
                    Console.WriteLine($"  {i + 1}. {subUsernames[i]}");
                }
            }
            else
            {
                Console.WriteLine($"ID: {subForum.Id}, Title: {subForum.Title}, Description: {subForum.Description}, Moderated by: No moderators assigned.");
            }
        }
    }

}