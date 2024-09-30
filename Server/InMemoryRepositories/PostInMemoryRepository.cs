namespace InMemoryRepositories;
using Entities;
using RepositoryContracts;

public class PostInMemoryRepository : IPostRepository
{
    private List<Post> posts = new List<Post>();
    private bool post_liked = false;

    public PostInMemoryRepository()
    {
        _ = AddASync(new Post("Favorite Programming Language?", "What's everyone's favorite programming language and why?", 1)).Result; // id = 1
        _ = AddASync(new Post("Movie Recommendations", "Can anyone recommend a good movie to watch this weekend?", 2)).Result; // id = 2
        _ = AddASync(new Post("Travel Tips", "Any tips for traveling light on a two-week trip?", 3)).Result; // id = 3
        _ = AddASync(new Post("Healthy Eating", "Share your go-to healthy meals or snacks.", 4)).Result; // id = 4
        _ = AddASync(new Post("Exercise Routine", "What's your current workout routine? Need ideas!", 5)).Result; // id = 5
        _ = AddASync(new Post("Gaming", "What's the best game of 2024 so far?", 1)).Result; // id = 6
    }

    public Post FindPost(Post post)
    {
        Post? exisitingPost = posts.FirstOrDefault(p => p.Id == post.Id);
        if (exisitingPost is null)
        {
            throw new InvalidOperationException($"Post with ID '{post.Id}'not found.");
        }
        return exisitingPost;
    }
    public Task<Post> FindPostById(int id)
    {
        Post? exisitingPost = posts.FirstOrDefault(p => p.Id == id);
        if (exisitingPost is null)
        {
            throw new InvalidOperationException($"Post with ID '{id}'not found.");
        }
        return Task.FromResult(exisitingPost);
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
        return Task.FromResult(FindPostById(id).Result);
    }

    public Task DeleteAsync(int id)
    {
        posts.Remove(FindPostById(id).Result);
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
    public Task LikeAsync(Post post, int userId)
    {
        FindPost(post).Like.Add(post.UserId);
        return Task.CompletedTask;
    }
    

    public Task DisLikeAsync(Post post, int userId)
    {
        FindPost(post).Dislike.Add(userId);
        return Task.CompletedTask;
    }
}