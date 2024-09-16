namespace InMemoryRepositories;
using RepositoryContracts;
using Entities;

public class ModeratorInMemoryRepository : IModeratorRepository
{
    private List<Moderator> moderators = new List<Moderator>();

    public ModeratorInMemoryRepository()
    {
        // Using the constructor with userId and subForumId
        _ = AddAsync(new Moderator(userId: 1, subForumId: 1)).Result;  // User: Kuba, SubForum: None of them (Programming languages)
        _ = AddAsync(new Moderator(userId: 2, subForumId: 2)).Result;  // User: Maciej, SubForum: Any horror movie recommendations
        _ = AddAsync(new Moderator(userId: 3, subForumId: 3)).Result;  // User: Arturs, SubForum: Comedies
        _ = AddAsync(new Moderator(userId: 4, subForumId: 4)).Result;  // User: user_1, SubForum: Travel tips Italy
        _ = AddAsync(new Moderator(userId: 5, subForumId: 5)).Result;  // User: user_2, SubForum: Gym app
        _ = AddAsync(new Moderator(userId: 1, subForumId: 6)).Result;  // User: Kuba, SubForum: COD Warzone 2.0 (Kuba moderates two subforums)
    }

    public Moderator FindModerator(Moderator moderator)
    {
        Moderator? existingModerator = moderators.FirstOrDefault(m => m.Id == moderator.Id);
        if (existingModerator is null)
        {
            throw new InvalidOperationException($"Moderator with ID '{moderator.Id}' not found.");
        }
        
        return existingModerator;
    }

    public Moderator FindModeratorById(int id)
    {
        Moderator? existingModerator = moderators.FirstOrDefault(m => m.Id == id);
        if (existingModerator is null)
        {
            throw new InvalidOperationException($"Moderator with ID '{id}' not found.");
        }
        
        return existingModerator;
    }

    public Task<Moderator> AddAsync(Moderator moderator)
    {
        moderator.Id = moderators.Any()
           ? moderators.Max(m => m.Id) + 1
            : 1;
        moderators.Add(moderator);
        return Task.FromResult(moderator);
    }

    public Task UpdateAsync(Moderator moderator)
    {
        moderators.Remove(FindModerator(moderator));
        moderators.Add(moderator);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        moderators.Remove(FindModeratorById(id));
        return Task.CompletedTask;
    }

    public IQueryable<Moderator> GetManyAsync()
    {
        return moderators.AsQueryable();
    }

    public Task<List<Moderator>> GetModeratorsBySubForumIdAsync(int subForumId)
    {
        var filteredModerators = moderators.Where(m => m.SubForumId == subForumId).ToList();
        return Task.FromResult(filteredModerators);
    }

    public Task<Moderator> GetSingleAsync(int id)
    {
        return Task.FromResult(FindModeratorById(id));
    }
}