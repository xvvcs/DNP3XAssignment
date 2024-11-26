using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories;

public class EfcCommentRepository : ICommentRepository
{
    private readonly AppContext _context;

    public EfcCommentRepository(AppContext context)
    {
        _context = context;
    }

    public async Task<Comment> FindCommentByIdAsync(int id)
    {
        return await _context.Comments.FindAsync(id);
    }

    public async Task<Comment> AddAsync(Comment comment)
    {
        EntityEntry<Comment> entityEntry = await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task UpdateAsync(Comment comment)
    {
        if (!await _context.Comments.AnyAsync(c => c.Id == comment.Id))
        {
            throw new InvalidOperationException("Comment not found");
        }
        _context.Comments.Update(comment);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var comment = await _context.Comments.FindAsync(id);
        if (comment == null)
        {
            throw new InvalidOperationException("Comment not found");
        }
        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
    }

    public async Task<Comment> GetSingleAsync(int id)
    {
        return await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
    }

    public IQueryable<Comment> GetMany()
    {
        return _context.Comments.AsQueryable();
    }

    public async Task LikeAsync(Comment comment, int userId)
    {
        var existingComment = await _context.Comments.FindAsync(comment.Id);
        if (existingComment == null)
        {
            throw new InvalidOperationException("Comment not found");
        }
        existingComment.Like.Add(userId);
        existingComment.updateLikeCount();
        _context.Comments.Update(existingComment);
        await _context.SaveChangesAsync();
    }

    public async Task DisLikeAsync(Comment comment, int userId)
    {
        var existingComment = await _context.Comments.FindAsync(comment.Id);
        if (existingComment == null)
        {
            throw new InvalidOperationException("Comment not found");
        }
        existingComment.Dislike.Add(userId);
        existingComment.updateLikeCount();
        _context.Comments.Update(existingComment);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Comment>> GetCommentsByPostIdAsync(int postId)
    {
        return await _context.Comments.Where(c => c.PostId == postId).ToListAsync();
    }
}