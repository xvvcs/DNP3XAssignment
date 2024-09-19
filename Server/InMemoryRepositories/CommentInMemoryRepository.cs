namespace InMemoryRepositories;
using Entities;
using RepositoryContracts;

public class CommentInMemoryRepository : ICommentRepository
{
    private List<Comment> comments = new List<Comment>();

    public CommentInMemoryRepository()
    {
        // Post 1 
        _ = AddAsync(new Comment("Python all the way!", 1, 1)).Result;
        _ = AddAsync(new Comment("I love JavaScript, it's so versatile.", 1, 2)).Result;
        _ = AddAsync(new Comment("C# has to be my favorite, it's powerful.", 1, 3)).Result;
        _ = AddAsync(new Comment("Go is becoming my new favorite!", 1, 4)).Result;
        _ = AddAsync(new Comment("Ruby is just so elegant.", 1, 5)).Result;

        // Post 2
        _ = AddAsync(new Comment("You should check out 'Inception', it's a mind-bender.", 2, 1)).Result;
        _ = AddAsync(new Comment("'The Matrix' is always a classic!", 2, 3)).Result;
        _ = AddAsync(new Comment("'Interstellar' for sure, it's beautiful.", 2, 4)).Result;
        _ = AddAsync(new Comment("If you like thrillers, 'Gone Girl' is great.", 2, 5)).Result;

        // Post 3
        _ = AddAsync(new Comment("Roll your clothes, it saves so much space!", 3, 2)).Result;
        _ = AddAsync(new Comment("Bring a power bank for long flights!", 3, 1)).Result;
        _ = AddAsync(new Comment("I always pack a small first aid kit.", 3, 3)).Result;
        _ = AddAsync(new Comment("Don't forget to bring a good book.", 3, 5)).Result;

        // Post 4
        _ = AddAsync(new Comment("I love making smoothie bowls for breakfast.", 4, 2)).Result;
        _ = AddAsync(new Comment("Quinoa salads are my go-to lunch.", 4, 1)).Result;
        _ = AddAsync(new Comment("I snack on almonds and fruits throughout the day.", 4, 4)).Result;
        _ = AddAsync(new Comment("Meal prepping helps me stay healthy.", 4, 5)).Result;

        // Post 5
        _ = AddAsync(new Comment("I do strength training 3 times a week.", 5, 2)).Result;
        _ = AddAsync(new Comment("Running every morning has changed my life!", 5, 1)).Result;
        _ = AddAsync(new Comment("I love doing yoga for flexibility.", 5, 3)).Result;
        _ = AddAsync(new Comment("Try HIIT workouts, they are amazing for fat burn.", 5, 4)).Result;

        // Post 6
        _ = AddAsync(new Comment("'Baldur's Gate 3' is amazing, highly recommend!", 6, 1)).Result;
        _ = AddAsync(new Comment("'The Legend of Zelda: Tears of the Kingdom' is my favorite so far.", 6, 3)).Result;
        _ = AddAsync(new Comment("'Starfield' has been mind-blowing!", 6, 5)).Result;
        _ = AddAsync(new Comment("I'm still obsessed with 'Elden Ring'.", 6, 2)).Result;

    }

    public Task<Comment> FindCommentById(int id)
    {
        Comment? existingComment = comments.FirstOrDefault(c => c.Id == id);
        if (existingComment is null)
        {
            throw new InvalidOperationException("No comment found with id: " + id);
        }

        return Task.FromResult(existingComment);
    }
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

    public IQueryable<Comment> GetMany()
    {
        return comments.AsQueryable();
    }

    public Task LikeAsync(Comment comment)
    {
        Comment? getComment = comments.SingleOrDefault(c => c.Id == comment.Id);
        if (getComment is null)
        {
            throw new InvalidOperationException($"Comment with ID '{comment.Id}' not found");
        }
        getComment.LikeCount++;
        return Task.CompletedTask;
    }

    public Task DisLikeAsync(Comment comment)
    {
        Comment? getComment = comments.SingleOrDefault(c => c.Id == comment.Id);
        if (getComment is null)
        {
            throw new InvalidOperationException($"Comment with ID '{comment.Id}' not found");
        }
        getComment.DislikeCount++;
        return Task.CompletedTask;
    }
    
    public Task<List<Comment>> GetCommentsByPostIdAsync(int postId)
    {
        var filteredComments = comments.Where(c => c.PostId == postId).ToList();
        return Task.FromResult(filteredComments);
    }
}