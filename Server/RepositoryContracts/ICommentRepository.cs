using Entities;

namespace RepositoryContracts;

public interface ICommentRepository
{
    Task<Comment> FindCommentById(int id);
    Task<Comment> AddAsync(Comment comment);
    Task UpdateAsync(Comment comment);
    Task DeleteAsync(int id);
    Task<Comment> GetSingleAsync(int id);
    IQueryable<Comment> GetMany();
    Task LikeAsync (Comment comment, int userId);
    Task DisLikeAsync (Comment comment, int userId);
    Task<List<Comment>> GetCommentsByPostIdAsync(int postId);
}