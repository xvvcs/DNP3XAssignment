using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class ListCommentsView
{
    private readonly ICommentRepository commentRepository;
    
    public ListCommentsView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public async Task DisplayCommentsByIdAsync(int postID)
    {
        var comments = await commentRepository.GetCommentsByPostIdAsync(postID);
    
        Console.WriteLine($"Listing Comments for post {postID}:");
        
        foreach (Comment comment in comments)
        {
            Console.WriteLine($"- {comment.Id}: {comment.Body}");
        }
    }

    public async Task DisplayCommentsAsync()
    {
        Console.WriteLine("Listing all comments:");
        foreach (Comment comment in commentRepository.GetMany())
        {
            Console.WriteLine($"- {comment.Id}: {comment.Body}");
        }
    }
}