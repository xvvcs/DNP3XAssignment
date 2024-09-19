using System.Text.Json;
using Entities;
using RepositoryContracts;
using InvalidOperationException = System.InvalidOperationException;

namespace FileRepositories;

public class SubForumFileRepository : ISubForumRepository
{
    private readonly string _filePath = "subforums.json";

    public SubForumFileRepository()
    {
        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, "[]");
        }
    }

    private async Task<List<SubForum>> LoadSubForumsAsync()
    {
        string subForumsAsJson = await File.ReadAllTextAsync(_filePath);
        return JsonSerializer.Deserialize<List<SubForum>>(subForumsAsJson);
    }

    private async Task SaveSubForumsAsync(List<SubForum> subForums)
    {
        string subForumsAsJson = JsonSerializer.Serialize(subForums);
        await File.WriteAllTextAsync(_filePath, subForumsAsJson);
    }

    public async Task<SubForum> AddAsync(SubForum subForum)
    {
        List<SubForum> subForums = await LoadSubForumsAsync();
        subForum.Id = subForums.Any() ? subForums.Max(sf => sf.Id) + 1 : 1;
        subForums.Add(subForum);
        await SaveSubForumsAsync(subForums);
        return subForum;
    }

    public async Task<SubForum> GetSingleAsync(int id)
    {
        List<SubForum> subForums = await LoadSubForumsAsync();
        SubForum? subForum = subForums.SingleOrDefault(sf => sf.Id == id);
        if (subForum is null)
        {
            throw new InvalidOperationException($"SubForum with ID '{id}' not found");
        }
        return subForum;
    }

    public async Task DeleteAsync(int id)
    {
        List<SubForum> subForums = await LoadSubForumsAsync();
        SubForum? subForumToRemove = subForums.SingleOrDefault(sf => sf.Id == id);
        if (subForumToRemove is null)
        {
            throw new InvalidOperationException($"SubForum with ID '{id}' not found");
        }
        subForums.Remove(subForumToRemove);
        await SaveSubForumsAsync(subForums);
    }

    public async Task UpdateAsync(SubForum subForum)
    {
        List<SubForum> subForums = await LoadSubForumsAsync();
        SubForum? existingSubForum = subForums.SingleOrDefault(sf => sf.Id == subForum.Id);
        if (existingSubForum is null)
        {
            throw new InvalidOperationException($"SubForum with ID '{subForum.Id}' not found");
        }
        subForums.Remove(existingSubForum);
        subForums.Add(subForum);
        await SaveSubForumsAsync(subForums);
    }

    public IQueryable<SubForum> GetMany()
    {
        string subForumsAsJson = File.ReadAllText(_filePath);
        List<SubForum> subForums = JsonSerializer.Deserialize<List<SubForum>>(subForumsAsJson);
        return subForums.AsQueryable();
    }
    
    public async Task<int> FindSubForumCreator(int subForumID)
    {
        List<SubForum> subForums = await LoadSubForumsAsync();
        SubForum? subForum = subForums.SingleOrDefault(sf => sf.Id == subForumID);
        if (subForum is null)
        {
            throw new InvalidOperationException($"SubForum with ID '{subForumID}' not found");
        }
        return subForum.UserId;
    }
   
}