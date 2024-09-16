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
        Post existingPost = postRepository.FindPostById(postId);
        Post post = new Post(title, body, existingPost.UserId, existingPost.LikeCount, existingPost.DislikeCount, postId);
        await postRepository.UpdateAsync(post);
        Console.WriteLine($"Post {postId} updated successfully.");
    }

    public async Task LikePostAsync(int postId)
    {
        Post post = postRepository.FindPostById(postId);
        await postRepository.LikeAsync(post);
        Console.WriteLine($"{post.Title} liked.");
    }

    public async Task DislikePostAsync(int postId)
    {
        Post post = postRepository.FindPostById(postId);
        await postRepository.DisLikeAsync(post);
        Console.WriteLine($"{post.Title} disliked.");
    }
}