namespace InMemoryRepositories;
using Entities;
using RepositoryContracts;

public class PostInMemoryRepository : IPostRepository
{
    private List<Post> posts = new List<Post>();

    public Post FindPost(Post post)
    {
        Post? exisitingPost = posts.FirstOrDefault(p => p.Id == post.Id);
        if (exisitingPost is null)
        {
            throw new InvalidOperationException($"Post with ID '{post.Id}'not found.");
        }
        return exisitingPost;
    }
    public Post FindPostById(int id)
    {
        Post? exisitingPost = posts.FirstOrDefault(p => p.Id == id);
        if (exisitingPost is null)
        {
            throw new InvalidOperationException($"Post with ID '{id}'not found.");
        }
        return exisitingPost;
    }
    public Task<Post> AddASync(Post post)
    {
       post.Id = posts.Any()
           ? posts.Max(p => p.Id) + 1
            : 1;
       posts.Add(post);
       return Task.FromResult(post);
    }

    public Task<Post> GetSingleAsync(int id)
    {
        return Task.FromResult(FindPostById(id));
    }

    public Task DeleteAsync(int id)
    {
        posts.Remove(FindPostById(id));
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Post post)
    {
        posts.Remove(FindPost(post));
        posts.Add(post);

        return Task.CompletedTask;
    }

    public IQueryable<Post> GetMany()
    {
        return posts.AsQueryable();
    }
    public Task LikeAsync(Post post)
    {
        FindPost(post).LikeCount++;
        return Task.CompletedTask;
    }

    public Task DislikeAsync(Post post)
    {
        FindPost(post).DislikeCount++;
        return Task.CompletedTask;
    }
}