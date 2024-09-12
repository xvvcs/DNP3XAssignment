using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class SingleCommentView
{
    private readonly ICommentRepository commentRepository;
    
    public SingleCommentView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public async Task DisplayComment(int commentID)
    {
        Comment comment = await commentRepository.GetSingleAsync(commentID);
        Console.WriteLine($"Comment ID: {comment.Id}, Post ID: {comment.PostId}, " +
                          $"User ID: {comment.UserId}, Content: {comment.Body}, " +
                          $"Dislikes: {comment.DislikeCount}, Likes: {comment.LikeCount}");
    }
}