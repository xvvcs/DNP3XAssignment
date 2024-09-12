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

    public async Task UpdateComment(int commentID, string newContent, int userID, int likes, int dislikes, int postID)
    {
        Comment comment = new Comment(newContent, postID, userID, likes, dislikes, postID);    
        await commentRepository.UpdateAsync(comment);
        Console.WriteLine($"Comment with ID {commentID} has been updated.");
    }
    
}