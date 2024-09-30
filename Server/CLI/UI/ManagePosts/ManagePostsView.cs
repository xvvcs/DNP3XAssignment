using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ManagePostsView
{
    private readonly IPostRepository postRepository;
    
    public ManagePostsView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task DeletePostAsync(int postId)
    {
        await postRepository.DeleteAsync(postId);
        Console.WriteLine($"Post {postId} deleted successfully.");

    }

    public async Task UpdatePostAsync(int postId, string body, string title)
    {
        Post existingPost = postRepository.FindPostById(postId).Result;
        Post post = new Post(title, body, existingPost.UserId, postId);
        await postRepository.UpdateAsync(post);
        Console.WriteLine($"Post {postId} updated successfully.");
    }

    public async Task LikePostAsync(int postId, int userId)
    {
        Post post = postRepository.FindPostById(postId).Result;
        await postRepository.LikeAsync(post, userId);
        Console.WriteLine($"{post.Title} liked.");
    }

    public async Task DislikePostAsync(int postId, int userId)
    {
        Post post = postRepository.FindPostById(postId).Result;
        await postRepository.DisLikeAsync(post, userId);
        Console.WriteLine($"{post.Title} disliked.");
    }
}