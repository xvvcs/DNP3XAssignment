using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class SinglePostView
{
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;
    
    public SinglePostView(IPostRepository postRepository, ICommentRepository commentRepository)
    {
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
    }
    public async Task DisplayPostAsync(int postId)
    {
        Post? post = await postRepository.GetSingleAsync(postId);
        Console.WriteLine($"ID: {post.Id}, Title: {post.Title}, Content: {post.Body}, Like Count: {post.LikeCount}, Dislike Count: {post.DislikeCount}");
        
        var comments = await commentRepository.GetCommentsByPostIdAsync(postId);
    
        if (comments.Count == 0)
        {
            Console.WriteLine("No comments for this post.");
            return;
        }
        
        Console.WriteLine("Comments:");
        foreach (Comment comment in comments)
        {
            Console.WriteLine($"- {comment.Id}: {comment.Body} (Likes: {comment.LikeCount}, Dislikes: {comment.DislikeCount})");
        }
    }
}