using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class ModeratorFileRepository: IModeratorRepository
{
    private readonly string _filePath = "comments.json";
    
    public ModeratorFileRepository()
    {
        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, "[]");
        }
    }
    public async Task<Moderator> FindModerator(Moderator moderator)
    {
        List<Moderator> moderators = await LoadModeratorsAsync();
        Moderator? existingModerator = moderators.FirstOrDefault(m => m.Id == moderator.Id);
        if (existingModerator is null)
        {
            throw new InvalidOperationException($"No moderator found with id: {moderator.Id}");
        }

        return existingModerator;
    }

    public async Task<Moderator> FindModeratorById(int id)
    {
        List<Moderator> moderators = await LoadModeratorsAsync();
        Moderator? existingModerator = moderators.FirstOrDefault(m => m.Id == id);
        if (existingModerator is null)
        {
            throw new InvalidOperationException($"No moderator found with id: {id}");
        }

        return existingModerator;
    }
    
    private async Task<List<Moderator>> LoadModeratorsAsync()
    {
        string moderatorAsJson = await File.ReadAllTextAsync(_filePath);
        return JsonSerializer.Deserialize<List<Moderator>>(moderatorAsJson);
    }
    private async Task SaveModeratorsAsync(List<Moderator> moderators)
    {
        string commentAsJson = JsonSerializer.Serialize(moderators);
        await File.WriteAllTextAsync(_filePath, commentAsJson);
    }
    public async Task<Moderator> AddAsync(Moderator moderator)
    {
        List<Moderator> moderators = await LoadModeratorsAsync();
        int maxID = moderators.Count > 0 ? moderators.Max(m => m.Id) : 1;
        moderator.Id = maxID + 1;
        moderators.Add(moderator);
        await SaveModeratorsAsync(moderators);
        return moderator;
    }

    public async Task UpdateAsync(Moderator moderator)
    {
        List<Moderator> moderators = await LoadModeratorsAsync();
        Moderator? existingModerator = moderators.SingleOrDefault(m => m.Id == moderator.Id);
        if (existingModerator is null)
        {
            throw new InvalidOperationException($"Moderator with ID '{moderator.Id}' not found");
        }

        moderators.Remove(existingModerator);
        moderators.Add(moderator);

        await SaveModeratorsAsync(moderators);
    }

    public async Task DeleteAsync(int id)
    {
        List<Moderator> moderators = await LoadModeratorsAsync();
        Moderator? moderatorToRemove = moderators.SingleOrDefault(m => m.Id == id);
        if (moderatorToRemove is null)
        {
            throw new InvalidOperationException($"Moderator with ID '{id}' not found");
        }
        moderators.Remove(moderatorToRemove);
        await SaveModeratorsAsync(moderators);
    }

    public IQueryable<Moderator> GetManyAsync()
    {
        string moderatorsAsJson = File.ReadAllTextAsync(_filePath).Result;
        List<Moderator> moderators = JsonSerializer.Deserialize<List<Moderator>>(moderatorsAsJson)!;
        return moderators.AsQueryable();
    }

    public async Task<List<Moderator>> GetModeratorsBySubForumIdAsync(int subForumId)
    {
        List<Moderator> moderators = await LoadModeratorsAsync();
        var filteredModerators = moderators.Where(m => m.SubForumId == subForumId).ToList();
        return filteredModerators;
    }

    public async Task<Moderator> GetSingleAsync(int id)
    {
        List<Moderator> moderators = await LoadModeratorsAsync();
        Moderator? getModerator = moderators.SingleOrDefault(m => m.Id == id);
        if (getModerator is null)
        {
            throw new InvalidOperationException($"Moderator with ID '{id}' not found");
        }

        return getModerator;
    }
}