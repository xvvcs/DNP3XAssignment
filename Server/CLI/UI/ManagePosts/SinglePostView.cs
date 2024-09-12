using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class SinglePostView
{
    private readonly IPostRepository postRepository;
    
    public SinglePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }
    public async Task DisplayPostAsync(int postId)
    {
        Post? post = await postRepository.GetSingleAsync(postId);
        Console.WriteLine($"ID: {post.Id}, Title: {post.Title}, Content: {post.Body}, Like Count: {post.LikeCount}, Dislike Count: {post.DislikeCount}");
        
    }
}