using System.Formats.Tar;
using System.Text.Json;
using Entities;
using RepositoryContracts;
using InvalidOperationException = System.InvalidOperationException;

namespace FileRepositories;

public class SubForumFileRepository : ISubForumRepository
{
    private readonly string _filePath = "subforums.json";
    private readonly string _postsFilePath = "posts.json";

    public SubForumFileRepository()
    {
        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, "[]");
        }
        if (!File.Exists(_postsFilePath))
        {
            File.WriteAllText(_postsFilePath, "[]");
        }
    }

    private async Task<List<SubForum>> LoadSubForumsAsync()
    {
        string subForumsAsJson = await File.ReadAllTextAsync(_filePath);
        return JsonSerializer.Deserialize<List<SubForum>>(subForumsAsJson);
    }
    private async Task<List<Post>> LoadPostsAsync()
    {
        string postsAsJson = await File.ReadAllTextAsync(_postsFilePath);
        return JsonSerializer.Deserialize<List<Post>>(postsAsJson);
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

    public async Task<SubForum> AddASync(SubForum subForum)
    {
        List<SubForum> subForums = await LoadSubForumsAsync();
        int maxID = subForums.Count > 0 ? subForums.Max(sf => sf.Id) : 1;
        subForum.Id = maxID + 1;
        
        subForums.Add(subForum);
        await SaveSubForumsAsync(subForums);
        return subForum;
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
    public async Task<IEnumerable<Post>> GetPostsBySubforumAsync(int subforumId)
    {
        List<SubForum> subForums = await LoadSubForumsAsync();
        SubForum subForum = subForums.SingleOrDefault(sf => sf.Id == subforumId);
        if (subForum == null)
        {
            throw new InvalidOperationException($"SubForum with ID '{subforumId}' not found.");
        }

        List<Post> posts = await LoadPostsAsync();
        var subforumPosts = posts.Where(p => subForum.PostIds.Contains(p.Id));
        return subforumPosts;
    }

    public async Task AddPostToSubforumAsync(int subforumId, int postId)
    {
        List<SubForum> subForums = await LoadSubForumsAsync();
        SubForum subForum = subForums.SingleOrDefault(sf => sf.Id == subforumId);
        if (subForum == null)
        {
            throw new InvalidOperationException($"SubForum with ID '{subforumId}' not found.");
        }

        subForum.AddPost(postId);
        await SaveSubForumsAsync(subForums);
    }

    public async Task DeletePostFromSubforumAsync(int subforumId, int postId)
    {
        List<SubForum> subForums = await LoadSubForumsAsync();
        SubForum subForum = subForums.SingleOrDefault(sf => sf.Id == subforumId);
        if (subForum == null)
        {
            throw new InvalidOperationException($"SubForum with ID '{subforumId}' not found.");
        }

        subForum.DeletePost(postId);
        await SaveSubForumsAsync(subForums);
    }
    
    public async Task<IEnumerable<SubForum>> GetSubForumsByUserIdAsync(int userId)
    {
        List<SubForum> subForums = await LoadSubForumsAsync();
        var filteredSubForums = subForums.Where(sf => sf.UserId == userId).ToList();
        return filteredSubForums;
    }
   
}