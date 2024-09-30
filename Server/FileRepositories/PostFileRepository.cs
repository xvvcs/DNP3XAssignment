using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class PostFileRepository : IPostRepository
{
    private readonly string filePath = "posts.json";
    
    public PostFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    private async Task<List<Post>> LoadPostsAsync()
    {
        string postAsJson = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<Post>>(postAsJson) ?? new List<Post>();
    }

    private async Task SavePostsAsync(List<Post> posts)
    {
        string postAsJson = JsonSerializer.Serialize(posts);
        await File.WriteAllTextAsync(filePath, postAsJson);
    }

    public async Task<Post> FindPostById(int id)
    {
        List<Post> posts = await LoadPostsAsync();
        Post? existingPost = posts.FirstOrDefault(p => p.Id == id);
        if (existingPost is null)
        {
            throw new InvalidOperationException($"Post with ID '{id}' not found.");
        }
        return existingPost;
    }

    public async Task<Post> AddASync(Post post)
    {
        List<Post> posts = await LoadPostsAsync();
        int maxID = posts.Count > 0 ? posts.Max(p => p.Id) : 1;
        post.Id = maxID + 1;
        posts.Add(post);
        await SavePostsAsync(posts);
        return post;
    }

    public async Task<Post> GetSingleAsync(int id)
    {
        return await FindPostById(id);
    }

    public async Task DeleteAsync(int id)
    {
        List<Post> posts = await LoadPostsAsync();
        Post? postToRemove = posts.FirstOrDefault(p => p.Id == id);
        if (postToRemove is null)
        {
            throw new InvalidOperationException($"Post with ID '{id}' not found.");
        }
        posts.Remove(postToRemove);
        await SavePostsAsync(posts);
    }

    public async Task UpdateAsync(Post post)
    {
        List<Post> posts = await LoadPostsAsync();
        Post? existingPost = posts.FirstOrDefault(p => p.Id == post.Id);
        if (existingPost is null)
        {
            throw new InvalidOperationException($"Post with ID '{post.Id}' not found.");
        }

        posts.Remove(existingPost);
        posts.Add(post);
        await SavePostsAsync(posts);
    }

    public IQueryable<Post> GetMany()
    {
        string postAsJson = File.ReadAllText(filePath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postAsJson) ?? new List<Post>();
        return posts.AsQueryable();
    }

    public async Task LikeAsync(Post post, int userId)
    {
        List<Post> posts = await LoadPostsAsync();
        Post? postToLike = posts.FirstOrDefault(p => p.Id == post.Id);
        if (postToLike is null)
        {
            throw new InvalidOperationException($"Post with ID '{post.Id}' not found.");
        }
        if (!postToLike.Like.Contains(userId))
        {
            postToLike.Like.Add(userId);
            postToLike.Dislike.Remove(userId);
            postToLike.updateLikeCount();
            await SavePostsAsync(posts);
        }
        else
        {
            throw new InvalidOperationException($"Post has already been liked by user with ID '{userId}'.");
        }
    }

    public async Task DisLikeAsync(Post post, int userId)
    {
        List<Post> posts = await LoadPostsAsync();
        Post? postToDislike = posts.FirstOrDefault(p => p.Id == post.Id);
        if (postToDislike is null)
        {
            throw new InvalidOperationException($"Post with ID '{post.Id}' not found.");
        }
        if (!postToDislike.Dislike.Contains(userId))
        {
            postToDislike.Dislike.Add(userId);
            postToDislike.Like.Remove(userId);
            postToDislike.updateLikeCount();
            await SavePostsAsync(posts);
        }
        else
        {
            throw new InvalidOperationException($"Post has already been disliked by user with ID '{userId}'.");
        }
    }
}