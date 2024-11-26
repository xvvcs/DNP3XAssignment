using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories;

public class EfcPostRepository : IPostRepository
{
    private readonly AppContext _context;
    
    public EfcPostRepository(AppContext context)
    {
        _context = context;
    }

    public async Task<Post> AddASync(Post post)
    {
        EntityEntry<Post> entityEntry = await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task UpdateAsync(Post post)
    {
        if(!await _context.Posts.AnyAsync(p => p.Id == post.Id))
        {
            throw new InvalidOperationException("Post not found");
        }
        _context.Posts.Update(post);
        await _context.SaveChangesAsync();
    }

    public IQueryable<Post> GetMany()
    {
        return _context.Posts.AsQueryable();
    }

    public async Task<Post> FindPostByIdAsync(int id)
    {
        return await _context.Posts.FindAsync(id);
    }

    public async Task<Post> GetSingleAsync(int id)
    {
        return await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task DeleteAsync(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post == null)
        {
            throw new InvalidOperationException("Post not found");
        }
        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
    }

    public async Task LikeAsync(Post post, int userId)
    {
        var existingPost = await _context.Posts.FindAsync(post.Id);
        if (existingPost == null)
        {
            throw new InvalidOperationException("Post not found");
        }
        existingPost.Like.Add(userId);
        existingPost.updateLikeCount();
        _context.Posts.Update(existingPost);
        await _context.SaveChangesAsync();
    }

    public async Task DisLikeAsync(Post post, int userId)
    {
        var existingPost = await _context.Posts.FindAsync(post.Id);
        if (existingPost == null)
        {
            throw new InvalidOperationException("Post not found");
        }
        existingPost.Dislike.Add(userId);
        existingPost.updateLikeCount();
        _context.Posts.Update(existingPost);
        await _context.SaveChangesAsync();
    }
}