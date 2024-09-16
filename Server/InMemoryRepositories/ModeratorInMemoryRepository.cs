namespace InMemoryRepositories;
using RepositoryContracts;
using Entities;

public class ModeratorInMemoryRepository : IModeratorRepository
{
    private List<Moderator> moderators = new List<Moderator>();

    public ModeratorInMemoryRepository()
    {
        _ = AddAsync(new Moderator(1, 1)).Result; // UserId = 1 moderating SubForumId = 1 
        _ = AddAsync(new Moderator(2, 2)).Result; // UserId = 2 moderating SubForumId = 2 
        _ = AddAsync(new Moderator(3, 3)).Result; // UserId = 3 moderating SubForumId = 3 
        _ = AddAsync(new Moderator(4, 4)).Result; // UserId = 4 moderating SubForumId = 4 
        _ = AddAsync(new Moderator(5, 5)).Result; // UserId = 5 moderating SubForumId = 5 
        _ = AddAsync(new Moderator(3, 6)).Result; // UserId = 3 moderating SubForumId = 6 
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