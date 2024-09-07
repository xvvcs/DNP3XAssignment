namespace InMemoryRepositories;
using Entities;
using RepositoryContracts;

public class CommentInMemortyRepository : ICommentRepository
{
    private List<Comment> comments = new List<Comment>();
    public Task<Comment> AddAsync(Comment comment)
    {
        comment.Id = comments.Any()
            ? comments.Max(c => c.Id) + 1
             : 1;
        comments.Add(comment);
        
        return Task.FromResult(comment);
    }

    public Task UpdateAsync(Comment comment)
    {
        Comment? existingComment = comments.SingleOrDefault(p => p.Id == comment.Id);
        if (existingComment is null)
        {
            throw new InvalidOperationException($"Comment with ID '{comment.Id}' not found");
        }
        comments.Remove(existingComment);
        comments.Add(comment);
        
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Comment? commentToRemove = comments.SingleOrDefault(p => p.Id == id);
        if (commentToRemove is null)
        {
            throw new InvalidOperationException($"Comment with ID '{id}' not found");
        }
        comments.Remove(commentToRemove);
        
        return Task.CompletedTask;
    }

    public Task<Comment> GetSingleAsync(int id)
    {
        Comment? getComment = comments.SingleOrDefault(c => c.Id == id);
        if (getComment is null)
        {
            throw new InvalidOperationException($"Comment with ID '{id}' not found");
        }
        
        return Task.FromResult(getComment);
    }

    public IQueryable<Comment> GetManyAsync()
    {
        return comments.AsQueryable();
    }
}