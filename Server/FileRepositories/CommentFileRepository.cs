using System.Text.Json;
using Entities;
using RepositoryContracts;
using InvalidOperationException = System.InvalidOperationException;

namespace FileRepositories;

public class CommentFileRepository: ICommentRepository
{
    private readonly string _filePath = "comments.json";

    public CommentFileRepository()
    {
        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, "[]");
        }
    }

    public async Task<Comment> FindCommentById(int id)
    {
        List<Comment> comments = await LoadCommentsAsync();
        Comment? existingComment = comments.FirstOrDefault(c => c.Id == id);
        if (existingComment is null)
        {
            throw new InvalidOperationException("No comment found with id: " + id);
        }

        return existingComment;
    }

    private async Task<List<Comment>> LoadCommentsAsync()
    {
        string commentAsJson = await File.ReadAllTextAsync(_filePath);
        return JsonSerializer.Deserialize<List<Comment>>(commentAsJson);
    }

    private async Task SaveCommentsAsync(List<Comment> comments)
    {
        string commentAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(_filePath, commentAsJson);
    }

    public async Task<Comment> AddAsync(Comment comment)
    {
        List<Comment> comments = await LoadCommentsAsync();
        int maxID = comments.Count > 0 ? comments.Max(c => c.Id) : 1;
        comment.Id = maxID + 1;
        comments.Add(comment);
        await SaveCommentsAsync(comments);
        return comment;
    }

    public async Task UpdateAsync(Comment comment)
    {
        List<Comment> comments = await LoadCommentsAsync();
        Comment? existingComment = comments.SingleOrDefault(c => c.Id == comment.Id);
        if (existingComment is null)
        {
            throw new InvalidOperationException($"Comment with ID '{comment.Id}' not found");
        }

        comments.Remove(existingComment);
        comments.Add(comment);

        await SaveCommentsAsync(comments);
    }

    public async Task DeleteAsync(int id)
    {
        List<Comment> comments = await LoadCommentsAsync();
        Comment? commentToRemove = comments.SingleOrDefault(c => c.Id == id);
        if (commentToRemove is null)
        {
            throw new InvalidOperationException($"Comment with ID '{id}' not found");
        }
        comments.Remove(commentToRemove);
        await SaveCommentsAsync(comments);
    }

    public async Task<Comment> GetSingleAsync(int id)
    {
        List<Comment> comments = await LoadCommentsAsync();
        Comment? getComment = comments.SingleOrDefault(c => c.Id == id);
        if (getComment is null)
        {
            throw new InvalidOperationException($"Comment with ID '{id}' not found");
        }

        return getComment;
    }

    public async Task<List<Comment>> GetCommentsByPostIdAsync(int postId)
    {
        List<Comment> comments = await LoadCommentsAsync();
        var filteredComments = comments.Where(c => c.PostId == postId).ToList();
        return filteredComments;
    }

    public async Task LikeAsync(Comment comment)
    {
        List<Comment> comments = await LoadCommentsAsync();
        Comment? getComment = comments.SingleOrDefault(c => c.Id == comment.Id);
        if (getComment is null)
        {
            throw new InvalidOperationException($"Comment with ID '{comment.Id}' not found");
        }
        getComment.LikeCount++;
        await SaveCommentsAsync(comments);
    }

    public async Task DisLikeAsync(Comment comment)
    {
        List<Comment> comments = await LoadCommentsAsync();
        Comment? getComment = comments.SingleOrDefault(c => c.Id == comment.Id);
        if (getComment is null)
        {
            throw new InvalidOperationException($"Comment with ID '{comment.Id}' not found");
        }
        getComment.DislikeCount++;
        await SaveCommentsAsync(comments);
    }

    public IQueryable<Comment> GetMany()
    {
        string commentAsJson = File.ReadAllText(_filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentAsJson);
        return comments.AsQueryable();
        
    }
}