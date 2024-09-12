using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class CreateCommentsView
{
    private readonly ICommentRepository commentRepository;
    
    public CreateCommentsView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public async Task CreateCommentAsync(string body, int userId, int postID)
    {
        Comment comment = new Comment(body, userId, postID);
        await commentRepository.AddAsync(comment);
        Console.WriteLine($"Comment created successfully on post: {postID} by {userId}.");
    }
}