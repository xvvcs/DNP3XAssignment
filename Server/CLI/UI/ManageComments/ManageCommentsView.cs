using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class ManageCommentsView
{
    private readonly ICommentRepository commentRepository;
    
    public ManageCommentsView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public async Task DeleteComment(int commentID)
    {
        await commentRepository.DeleteAsync(commentID);
        Console.WriteLine($"Comment with ID {commentID} has been deleted.");
    }

    public async Task UpdateComment(int commentID, string newContent)
    {
        Comment existingComment = commentRepository.FindCommentById(commentID).Result;
        Comment comment = new Comment(newContent, existingComment.PostId, existingComment.UserId, commentID);    
        await commentRepository.UpdateAsync(comment);
        Console.WriteLine($"Comment with ID {commentID} has been updated.");
    }
    
}