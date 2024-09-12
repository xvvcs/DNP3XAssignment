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

    public async Task UpdatePostAsync(int postId, string body, string title, int userID, int dislikes, int likes )
    {
        Post post = new Post(title, body, userID, likes, dislikes, postId);
        await postRepository.UpdateAsync(post);
        Console.WriteLine($"Post {postId} updated successfully.");
    }
}